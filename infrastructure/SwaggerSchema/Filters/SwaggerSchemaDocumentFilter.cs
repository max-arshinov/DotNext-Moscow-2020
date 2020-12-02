using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes;
using Infrastructure.SwaggerSchema.TypeProvider;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.SwaggerSchema.Filters
{
    public class SwaggerSchemaDocumentFilter : IDocumentFilter
    {
        private readonly IEnumerable<AbstractCustomAttributeHandler> _abstractCustomAttributeHandlers;
        private readonly IDictionary<string, Type> _types;

        public SwaggerSchemaDocumentFilter(ITypeProvider typeProvider,
            IEnumerable<AbstractCustomAttributeHandler> abstractCustomAttributeHandlers)
        {
            _abstractCustomAttributeHandlers = abstractCustomAttributeHandlers;
            _types = typeProvider.GetTypes(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            AddAdditionalAttributesToSchemas(context.SchemaRepository.Schemas);
        }

        private void AddAdditionalAttributesToSchemas(Dictionary<string, OpenApiSchema> schemas)
        {
            foreach (var schema in schemas)
            {
                if (!_types.TryGetValue(schema.Key, out var type))
                {
                    continue;
                }

                var properties = type.GetProperties();

                if (!properties.Any())
                {
                    continue;
                }

                foreach (var abstractCustomAttributeHandler in _abstractCustomAttributeHandlers)
                {
                    abstractCustomAttributeHandler.InitPropertiesDictionary();
                }

                foreach (var property in properties)
                {
                    foreach (var abstractCustomAttributeHandler in _abstractCustomAttributeHandlers)
                    {
                        abstractCustomAttributeHandler.FillProperties(property);
                    }
                }

                foreach (var schemaProperty in schema.Value.Properties)
                {
                    foreach (var abstractCustomAttributeHandler in _abstractCustomAttributeHandlers)
                    {
                        abstractCustomAttributeHandler.HandleSchemaProperty(schemaProperty);
                    }
                }
            }
        }
    }
}