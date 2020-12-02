using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    public class ParallelDropdownsBuilder<T> : DropdownsBuilder<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;
        private MemberParams _memberName;

        private ConcurrentDictionary<string, Dropdown> _options = 
            new ConcurrentDictionary<string, Dropdown>();
        
        private Dictionary<string, IDropdownBuilder> _optionBuilders =
            new Dictionary<string, IDropdownBuilder>(); 
        
        internal ParallelDropdownsBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ParallelDropdownsBuilder<T> Prepend(DropdownOption option)
        {
            _options.TryAdd(_memberName.Key, new Dropdown(new [] {option}, _memberName.Name));
            return this;
        }

        public ParallelDropdownBuilderConfiguration<T> With<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            var c = new ParallelDropdownBuilderConfiguration<T>(this, _serviceProvider, memberName.Name);
            _optionBuilders[memberName.Key] = c;
            return c;
        }
        
        public ParallelDropdownsBuilder<T> With<TProperty>(Expression<Func<T, TProperty>> expression, 
            Func<IQueryable<T>, IQueryable<DropdownOption>> options)
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new DbContextDropdownBuilder<T>(_serviceProvider, options, memberName.Name);
            return this;
        }
        
        public ParallelDropdownsBuilder<T> With<TProperty>(Expression<Func<T, TProperty>> expression, 
            Func<IQueryable<T>, IServiceProvider, IQueryable<DropdownOption<TProperty>>> options)
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new ServiceProviderDropdownBuilder<T>(_serviceProvider, options, memberName.Name);
            return this;
        }
        
        public ParallelDropdownsBuilder<T> WithProperty<TProperty>(Expression<Func<T, TProperty>> expression, 
            Func<IQueryable<TProperty>, IQueryable<DropdownOption>> options)
            where TProperty : class
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new DbContextDropdownBuilder<TProperty>(_serviceProvider, options, memberName.Name);
            return this;
        }
        
        public ParallelDropdownsBuilder<T> WithProperty<TProperty>(Expression<Func<T, TProperty>> expression, 
            Func<IQueryable<TProperty>, IServiceProvider, IQueryable<DropdownOption<TProperty>>> options) 
            where TProperty : class
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new ServiceProviderDropdownBuilder<TProperty>(_serviceProvider, options, memberName.Name);
            return this;
        }
        
        public ParallelDropdownsBuilder<T> With<TProperty, TSource>(Expression<Func<T, TProperty>> expression, 
            Func<IQueryable<TSource>, IQueryable<DropdownOption>> options) 
            where TSource : class
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new DbContextDropdownBuilder<TSource>(_serviceProvider, options, memberName.Name);
            return this;
        }

        public ParallelDropdownsBuilder<T> WithEnum<TProperty>(Expression<Func<T, TProperty>> expression,
        Func<IEnumerable<DropdownOption>, IEnumerable<DropdownOption>> filter = null)
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new EnumDropdownBuilder<TProperty>(filter, memberName.Name);
            return this;
        }
        
        public ParallelDropdownsBuilder<T> WithBool<TProperty>(Expression<Func<T, TProperty>> expression, 
            (string trueName, string falseName)? naming = null)
        {
            var memberName = GetMemberParams(expression);
            _memberName = memberName;
            _optionBuilders[memberName.Key] = new BoolDropdownBuilder(_memberName.Name, naming);
            return this;
        }

        public override async Task<Dropdowns> BuildAsync()
        {
            await Task.WhenAll(_optionBuilders
                .Select(async x =>
                    {
                        try
                        {
                            var val = await x.Value.BuildAsync();
                            if (_options.ContainsKey(x.Key))
                            {
                                val.Prepend(_options[x.Key].Options.First());
                                _options[x.Key] = val;
                            }
                            else
                            {
                                _options.TryAdd(x.Key, val);
                            }
                        }
                        catch (Exception e)
                        {
                            var exc = new Exception($"Problem with dropdown column \"{x.Key}\". {e.Message}", e);
                            var logger = _serviceProvider.GetService<ILogger>();
                            logger?.LogError(exc, exc.Message);
                            throw exc;
                        }
                    }));

            var keys = _optionBuilders.Keys.ToArray();
            foreach (var kv in _options)
            {
                Options[kv.Key] = _options[kv.Key];
                Options[kv.Key].Order = Array.IndexOf(keys, kv.Key);
            }

            if (Options != null)
                foreach (var option in Options)
                {
                    if (option.Value.Order == 0)
                        option.Value.Selected = true;
                }
            return new Dropdowns(Options
                .OrderBy(option => option.Value.Order)
                .ToDictionary(option => option.Key, o => o.Value));
        }
    }
}