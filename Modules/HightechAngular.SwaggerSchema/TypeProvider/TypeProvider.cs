using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HightechAngular.SwaggerSchema.TypeProvider
{
    public class TypeProvider : ITypeProvider
    {
        public Type GetType(string type)
        {
            Assemblies.TryGetValue(type, out var t);
            return t;
        }
        
        private static readonly IDictionary<string, Type> Assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => !x.IsDynamic && x.FullName.StartsWith("HightechAngular"))
            .SelectMany(x => x.GetExportedTypes()
                .Where(y => y.IsClass && !y.IsAbstract && y.IsPublic))
            .ToDictionary(x => x.Name, x => x);
        
        public IDictionary<string, Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return Assemblies;
        }
    }
}