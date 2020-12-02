using System;
using System.Collections.Generic;
using System.Reflection;

namespace HightechAngular.SwaggerSchema.TypeProvider
{
    public interface ITypeProvider
    {
        Type GetType(string type);
        
        IDictionary<string, Type> GetTypes(IEnumerable<Assembly> assemblies);
    }
}