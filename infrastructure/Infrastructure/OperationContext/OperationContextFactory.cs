using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Force.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.OperationContext
{
    public class OperationContextFactory<T, TContext>
        where T : class
        where TContext : OperationContextBase<T>
    {
        public Func<T, TContext> BuildFunc(IServiceProvider sp)
        {
            return x => Build(sp, x);
        }

        public TContext Build(IServiceProvider sp, T request)
        {
            if (request == null)
            {
                return null;
            }

            var ctor = Cache.ConstructorInfo;

            var prs = ctor.GetParameters();
            var d = new Dictionary<ParameterInfo, object>();
            var dbContext = sp.GetService<DbContext>();
            var dbContextCache = GetDbContextCache(dbContext);
            var requestPropsCache = RequestPropsCache.GetInstance(request);

            foreach (var p in prs)
            {
                d[p] = p.ParameterType == request.GetType()
                    ? request
                    : dbContextCache != null
                      && dbContextCache.Contains(p.ParameterType)
                      && requestPropsCache.Contains(p.ParameterType)
                        ? requestPropsCache.GetPropInstance(p.ParameterType, request, dbContext)
                        : sp.GetService(p.ParameterType);
            }

            var ctr = Type<TContext>.GetActivator(ctor);
            var context = ctr.Invoke(d.Values.ToArray());

            return context;
        }

        private static ConstructorInfo GetConstructorInfo()
        {
            var ctype = typeof(TContext);
            var ctor = ctype
                .GetConstructors()
                .OrderByDescending(x => x.GetGenericArguments().Length)
                .First();

            return ctor;
        }

        private TContext CreateInstance(params object[] args)
        {
            return Type<TContext>.CreateInstance();
        }

        #region RequestPropsCache

        private class RequestPropsCache
        {
            private static RequestPropsCache _instance;
            private static readonly object _locker = new object();
            private readonly Dictionary<string, PropertyInfo> _requestProps;

            private RequestPropsCache(T request)
            {
                _requestProps = GetRequestProps(request);
            }

            public static RequestPropsCache GetInstance(T request)
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (_locker)
                {
                    _instance ??= new RequestPropsCache(request);
                }

                return _instance;
            }

            public object GetPropInstance(Type type, T request, DbContext dbContext)
            {
                return dbContext?.Find(type, GetValue(type, request));
            }

            public bool Contains(MemberInfo member)
            {
                return _requestProps.ContainsKey(member.Name + "Id")
                       || _requestProps.Count == 1 && _requestProps.ContainsKey("Id");
            }

            private object GetValue(MemberInfo member, T request)
            {
                return _requestProps.FirstOrDefault(x => x.Key == member.Name + "Id"
                                                         || _requestProps.Count == 1 && x.Key == "Id")
                    .Value.GetValue(request);
            }

            private static Dictionary<string, PropertyInfo> GetRequestProps(T request)
            {
                var props = request.GetType().GetProperties();

                return props.Where(propertyInfo => propertyInfo.Name[^2..] == "Id")
                    .ToDictionary(propertyInfo => propertyInfo.Name);
            }
        }

        #endregion

        private static class Cache
        {
            public static readonly ConstructorInfo ConstructorInfo =
                typeof(TContext)
                    .GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length)
                    .First();
        }

        #region DbContextCache

        private static DbContextCache GetDbContextCache(DbContext dbContext)
        {
            return dbContext == null ? null : DbContextCache.GetInstance((dynamic) dbContext);
        }

        [UsedImplicitly]
        private class DbContextCache<TDbContext>
        {
            public static readonly DbContextCache Instance = new DbContextCache(typeof(TDbContext));
        }

        private class DbContextCache
        {
            private readonly Type _type;

            private Dictionary<Type, PropertyInfo> _contextTypes;

            public DbContextCache(Type type)
            {
                _type = type;
                _contextTypes = GetContextTypes();
            }

            public static DbContextCache GetInstance<TDbContext>(TDbContext dbContext)
                where TDbContext : DbContext
            {
                return DbContextCache<TDbContext>.Instance;
            }

            public bool Contains(Type type)
            {
                return _contextTypes.Keys.Contains(type);
            }

            private Dictionary<Type, PropertyInfo> GetContextTypes()
            {
                if (_contextTypes != null && _contextTypes.Any())
                {
                    return _contextTypes;
                }

                var dbTypes = _type
                    .GetProperties()
                    .Where(x => x.PropertyType.IsGenericType
                                && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    .Select(x => x.PropertyType.GetGenericArguments()[0])
                    .ToList();

                _contextTypes = Type<TContext>
                    .PublicProperties
                    .Where(x => dbTypes.Contains(x.Value.PropertyType))
                    .Select(x => x.Value)
                    .ToDictionary(x => x.PropertyType);

                return _contextTypes;
            }
        }

        #endregion
    }
}