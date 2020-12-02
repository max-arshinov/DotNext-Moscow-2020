using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    public class ParallelDropdownBuilderConfiguration<T>: IDropdownBuilder 
        where T : class
    {
        private readonly ParallelDropdownsBuilder<T> _builder;
        private readonly IServiceProvider _serviceProvider;
        Func<Task<Dropdown>> _map;
        private readonly string _name;
        private Func<Func<Task<Dropdown>>, Task<Dropdown>> _profiler;

        internal ParallelDropdownBuilderConfiguration(ParallelDropdownsBuilder<T> builder, 
            IServiceProvider serviceProvider, 
            string name)
        {
            _builder = builder;
            _serviceProvider = serviceProvider;
            _name = name;
        }

        public ParallelDropdownBuilderConfiguration<T> As<TEntity, TValue>(
            bool condition,
            Func<IQueryable<TEntity>, IQueryable<DropdownOption<TValue>>> map,
            IEnumerable<DropdownOption<TValue>> falseValue
            )
            where TEntity : class
        {
            if (condition)
            {
                return As(map);
            }

            _map = () => Task.FromResult(new Dropdown(falseValue, _name));
            return this;
        }
        
        public ParallelDropdownBuilderConfiguration<T> As<TEntity, TValue>(
            Func<IQueryable<TEntity>, IEnumerable<DropdownOption<TValue>>> map
        )
            where TEntity : class
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var query = _serviceProvider.GetService<IQueryable<TEntity>>();
            
            _map = () => Task.FromResult(new Dropdown(map(query), _name));
            return this;
        }
        
        public ParallelDropdownBuilderConfiguration<T> As<TEntity, TValue>(
            Func<IQueryable<TEntity>, IQueryable<DropdownOption<TValue>>> map) 
            where TEntity : class
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            async Task<Dropdown> Func()
            {
                var mainData = _serviceProvider.InScopeAsync<IQueryable<TEntity>, List<DropdownOption<TValue>>>(
                    (queryable, sp) =>
                    {
                        var q = map(queryable).Distinct();
                        return ProtectedToListAsync(q);
                });

                await Task.WhenAll(mainData);
                var options = mainData.Result;

                return new Dropdown(options, _name);
            }

            _map = Func;
            return this;
        }

        public ParallelDropdownBuilderConfiguration<T> As<TEntity, TValue>(
            Func<IQueryable<TEntity>, IServiceProvider, IQueryable<DropdownOption<TValue>>> map) 
            where TEntity : class
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            async Task<Dropdown> Func()
            {
                var mainData = _serviceProvider.InScopeAsync<IQueryable<TEntity>, List<DropdownOption<TValue>>>(
                    (queryable, sp) =>
                    {
                        var q = map(queryable, sp).Distinct();
                        return ProtectedToListAsync(q);
                    });

                await Task.WhenAll(mainData);
                var options = mainData.Result;

                return new Dropdown(options, _name);
            }

            _map = Func;
            return this;
        }

        public static implicit operator Task<Dropdowns>(ParallelDropdownBuilderConfiguration<T> builder) =>
            builder._builder;

        public ParallelDropdownBuilderConfiguration<T> With<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return _builder.With(expression);
        }

        public ParallelDropdownBuilderConfiguration<T> Prepend(DropdownOption option)
        {
            _builder.Prepend(option);
            return this;
        }
        
        private static Task<List<TValue>> ProtectedToListAsync<TValue>(IQueryable<TValue> q)
        {
            // Some people will call AsQueryable no matter what :(
            return q.ToListAsync();
        }

        Task<Dropdown> IDropdownBuilder.BuildAsync()
        {
            if (_map == null)
            {
                throw new InvalidOperationException("Call \"As\" method first");
            }
            
            return _profiler == null
                ? _map()
                : _profiler(_map);
        }

        public ParallelDropdownBuilderConfiguration<T> Profile(
            Func<Func<Task<Dropdown>>, Task<Dropdown>> profiler)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            return this;
        }
    }
}