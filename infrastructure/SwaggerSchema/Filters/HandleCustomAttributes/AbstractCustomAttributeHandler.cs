using System.Collections.Generic;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes
{
    public abstract class AbstractCustomAttributeHandler
    {
        protected Dictionary<string, IOpenApiAny> Properties { get; set; }
        
        protected abstract string Key { get; set; }

        public abstract void FillProperties(PropertyInfo property);

        public void HandleSchemaProperty(KeyValuePair<string, OpenApiSchema> schemaProperty)
        {
            var schemaPropertyName = schemaProperty.Key.ToLower();
            if (Properties.ContainsKey(schemaPropertyName))
            {
                schemaProperty.Value.Extensions.Add(Key, Properties[schemaPropertyName]);
            }
        }

        public void InitPropertiesDictionary()
        {
            Properties = new Dictionary<string, IOpenApiAny>();
        }
    }
}