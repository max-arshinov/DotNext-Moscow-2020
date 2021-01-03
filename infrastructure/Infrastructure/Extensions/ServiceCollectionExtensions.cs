using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd.DomainEvents;
using Force.Reflection;
using Infrastructure.Cqrs;
using Infrastructure.Ddd;
using Infrastructure.OperationContext;
using Infrastructure.Validation;
using Infrastructure.Workflow;
using Infrastructure.Workflow.Steps;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly HashSet<Type> TargetInterfaces = new()
        {
            typeof(IHandler<>),
            typeof(IHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>),
            typeof(IDomainEventHandler<>),
            typeof(IFilter<,>),
            typeof(ISorter<,>),
            typeof(IQueryHandler<,>),
            typeof(IValidator<>),
            typeof(IAsyncValidator<>),
            typeof(IAsyncOperationContextFactory<,>)
        };

        public static IServiceCollection AddOpenGenericTypeDefinition(this IServiceCollection sc, Type type)
        {
            if (!type.IsInterface || !type.IsGenericType)
            {
                throw new ArgumentException($"Type \"{type}\" is not a generic interface type");
            }

            TargetInterfaces.Add(type);
            return sc;
        }

        public static void AddModule(this IServiceCollection services, Assembly assembly)
        {
            AddInfrastructure(services);

            var impls = new Dictionary<Type, Type>();
            var ctxs = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsTypeDefinition || type.IsAbstract || type.IsGenericType)
                {
                    continue;
                }

                var interfaces = type
                    .GetInterfaces()
                    .Where(x => x.IsGenericType)
                    .ToArray();

                foreach (var inf in interfaces)
                {
                    var gtd = inf.GetGenericTypeDefinition();

                    if (TargetInterfaces.Contains(gtd))
                    {
                        if (gtd == typeof(IHandler<,>))
                        {
                            if (!interfaces.Contains(typeof(ICommandHandler<,>))
                                && !interfaces.Contains(typeof(IQueryHandler<,>)))
                            {
                                impls[inf] = type;
                            }
                        }
                        else if (gtd == typeof(IHandler<>))
                        {
                            if (interfaces.Contains(typeof(IDomainEventHandler<>)))
                            {
                                impls[inf] = type;
                            }
                        }
                        else
                        {
                            impls[inf] = type;
                        }
                    }

                    if (typeof(IOperationContext<>) == gtd)
                    {
                        ctxs.Add(type);
                    }
                }
            }

            foreach (var kv in impls)
            {
                services.AddScoped(kv.Key, kv.Value);
            }

            foreach (var ctx in ctxs)
            {
                var inf = ctx
                    .GetInterfaces()
                    .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IOperationContext<>));

                var requestType = inf.GenericTypeArguments[0];
                var funcType = typeof(Func<,>).MakeGenericType(requestType, ctx);

                var factoryType = typeof(OperationContextFactory<,>).MakeGenericType(requestType, ctx);

                services.AddScoped(factoryType);

                services.AddScoped(funcType, sp => ((dynamic) sp.GetService(factoryType)).BuildFunc(sp));
            }
        }

        private static void AddInfrastructure(IServiceCollection services)
        {
            services.TryAddScoped<IHandler<IEnumerable<IDomainEvent>>, DomainEventDispatcher>();
            services.TryAddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.TryAddScoped(typeof(IWorkflow<,>), typeof(HandlerWorkflowFactory<,>));
            services.TryAddScoped(typeof(IAsyncWorkflow<,>), typeof(HandlerAsyncWorkflowFactory<,>));
            services.TryAddScoped(typeof(IValidator<>), typeof(DataAnnotationValidator<>));
            services.TryAddScoped(typeof(IAsyncValidator<>), typeof(DataAnnotationValidator<>));
        }

        #region Handler workflow

        public static HandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflow<TRequest, TResponse>(
            this IServiceProvider sp)
        {
            var validator = sp.GetService<IValidator<TRequest>>();
            var uow = sp.GetService<IUnitOfWork>();

            return validator == null
                ? new HandlerWorkflow<TRequest, TResponse>(new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow))
                : new HandlerWorkflow<TRequest, TResponse>(
                    new ValidateWorkflowStep<TRequest, TResponse>(validator),
                    new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow));
        }

        public static AsyncHandlerWorkflow<TRequest, TResponse> CreateDefaultWorkflowAsync<TRequest, TResponse>(
            this IServiceProvider sp)
        {
            var validatorAsync = sp.GetService<IAsyncValidator<TRequest>>();
            var uow = sp.GetService<IUnitOfWork>();

            return validatorAsync == null
                ? new AsyncHandlerWorkflow<TRequest, TResponse>(new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow))
                : new AsyncHandlerWorkflow<TRequest, TResponse>(
                    new UnitOfWorkWorkflowStep<TRequest, TResponse>(uow),
                    new ValidateWorkflowAsyncStep<TRequest, TResponse>(validatorAsync));
        }

        #endregion

        #region AddDbContextAndQueryables

        public static void AddDbContextAndQueryables<T>(
            this IServiceCollection sc)
            where T : DbContext
        {
            sc.AddDbContextAndQueryables(sp => sp.GetService<T>());
        }

        public static void AddDbContextAndQueryables<T>(
            this IServiceCollection sc,
            Func<IServiceProvider, T> dbContextFactory)
            where T : DbContext
        {
            sc.AddScoped<DbContext>(dbContextFactory);
            var dbSets = Type<T>
                .PublicProperties
                .Values
                .Where(x => x.PropertyType.IsGenericType
                            && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dbSet in dbSets)
            {
                var entityType = dbSet.PropertyType.GetGenericArguments()[0];
                var qt = typeof(IQueryable<>).MakeGenericType(entityType);
                sc.AddScoped(
                    qt,
                    typeof(QueryableFactory<>).MakeGenericType(entityType));
            }
        }

        #endregion

        #region AddModulesWithDbContext

        [PublicAPI]
        public static IMvcCoreBuilder AddModulesWithDbContext<TDbContext>(
            this IMvcCoreBuilder builder,
            params Assembly[] assemblies)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContextAndQueryables<TDbContext>();
            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assemblies);

            return builder;
        }

        [PublicAPI]
        public static IMvcCoreBuilder AddModulesWithDbContext<TDbContext>(
            this IMvcCoreBuilder builder,
            Func<IServiceProvider, TDbContext> dbContextFactory,
            params Assembly[] assemblies)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContextAndQueryables(dbContextFactory);
            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assemblies);

            return builder;
        }

        [PublicAPI]
        public static IMvcBuilder AddModulesWithDbContext<TDbContext>(
            this IMvcBuilder builder,
            Func<IServiceProvider, TDbContext> dbContextFactory,
            params Assembly[] assemblies)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContextAndQueryables(dbContextFactory);
            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assemblies);

            return builder;
        }

        [PublicAPI]
        public static IMvcBuilder AddModulesWithDbContext<TDbContext>(
            this IMvcBuilder builder,
            params Action<IServiceCollection>[] initializers)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContextAndQueryables<TDbContext>();

            AddModuleWithApplicationParts(builder.Services, builder.PartManager,
                initializers
                    .Select(x => x.Method.DeclaringType?.Assembly)
                    .Where(x => x != null));

            foreach (var initializer in initializers)
            {
                initializer(builder.Services);
            }

            return builder;
        }

        [PublicAPI]
        public static IMvcBuilder AddModulesWithDbContext<TDbContext>(
            this IMvcBuilder builder,
            params Assembly[] assemblies)
            where TDbContext : DbContext
        {
            builder.Services.AddDbContextAndQueryables<TDbContext>();

            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assemblies);

            return builder;
        }

        private static void AddModuleWithApplicationParts(IServiceCollection serviceCollection,
            ApplicationPartManager applicationPartManager, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                AddModuleWithApplicationParts(serviceCollection, applicationPartManager, assembly);
            }
        }

        #endregion

        #region AddModules

        [PublicAPI]
        public static IMvcCoreBuilder AddModule(
            this IMvcCoreBuilder builder,
            Assembly assembly)
        {
            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assembly);
            return builder;
        }

        [PublicAPI]
        public static IMvcBuilder AddModule(
            this IMvcBuilder builder,
            Assembly assembly)
        {
            AddModuleWithApplicationParts(builder.Services, builder.PartManager, assembly);
            return builder;
        }

        private static void AddModuleWithApplicationParts(IServiceCollection services, ApplicationPartManager manager,
            Assembly assembly)
        {
            AddModule(services, assembly);

            // add application parts of given assembly into the part manager
            foreach (var applicationPart in ApplicationPartFactory.GetApplicationPartFactory(assembly)
                .GetApplicationParts(assembly))
            {
                manager.ApplicationParts.Add(applicationPart);
            }
        }

        #endregion
    }
}