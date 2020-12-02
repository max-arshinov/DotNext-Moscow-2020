using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.SwaggerSchema.TypeProvider
{
    public class DefaultTypeProvider : ITypeProvider
    {
        private readonly Func<string, bool> _predicate;

        public DefaultTypeProvider(Func<string, bool> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            Assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(x => x.GetExportedTypes()
                    .Where(y => y.IsClass && !y.IsAbstract && y.IsPublic && _predicate(x.FullName)))
                .ToDictionary(x => x.Name, x => x);
        }
        
        public Type GetType(string type)
        {
            Assemblies.TryGetValue(type, out var t);
            return t;
        }

        private readonly IDictionary<string, Type> Assemblies;
        
        public IDictionary<string, Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return Assemblies;
        }
    }
}