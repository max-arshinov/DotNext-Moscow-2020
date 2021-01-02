using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.SwaggerSchema.TypeProvider
{
    public class DefaultTypeProvider : ITypeProvider
    {
        private readonly IDictionary<string, Type> _assemblies;

        public DefaultTypeProvider(Func<string, bool> predicate)
        {
            var predicate1 = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(x => x.GetExportedTypes()
                    .Where(y => y.IsClass && !y.IsAbstract && y.IsPublic && predicate1(x.FullName)))
                .ToDictionary(x => x.Name, x => x);
        }

        public Type GetType(string type)
        {
            _assemblies.TryGetValue(type, out var t);
            return t;
        }

        public IDictionary<string, Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return _assemblies;
        }
    }
}