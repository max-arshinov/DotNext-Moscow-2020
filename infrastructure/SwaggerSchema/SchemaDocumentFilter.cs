using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Infrastructure.SwaggerSchema.TypeProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.SwaggerSchema
{
    public class SchemaDocumentFilter: IDocumentFilter
    {
        private IDictionary<string, Type> _types;

        public SchemaDocumentFilter(ITypeProvider typeProvider)
        {
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

                var propertiesDisplayAttributeDictionary = GetPropertiesDisplayAttributeDictionary(properties);
                var propertiesHiddenInputAttributeDictionary = GetPropertiesHiddenInputAttributeDictionary(properties);
                var propertiesEnumFlagsAttributeDictionary = GetPropertiesEnumFlagsAttributeDictionary(properties);

                foreach (var schemaProperty in schema.Value.Properties)
                {
                    var schemaPropertyName = schemaProperty.Key.ToLower();

                    if (propertiesDisplayAttributeDictionary.ContainsKey(schemaPropertyName))
                    {
                        schemaProperty.Value.Extensions.Add(
                            "title",
                            propertiesDisplayAttributeDictionary[schemaPropertyName]);
                    }

                    if (propertiesHiddenInputAttributeDictionary.ContainsKey(schemaPropertyName))
                    {
                        schemaProperty.Value.Extensions.Add(
                            "isHidden",
                            propertiesHiddenInputAttributeDictionary[schemaPropertyName]);
                    }

                    if (propertiesEnumFlagsAttributeDictionary.ContainsKey(schemaPropertyName))
                    {
                        schemaProperty.Value.Extensions.Add(
                            "isMultiSelect",
                            propertiesEnumFlagsAttributeDictionary[schemaPropertyName]);
                    }
                }
            }
        }

        private Dictionary<string, OpenApiString> GetPropertiesDisplayAttributeDictionary(PropertyInfo[] properties)
        {
            var propertiesDisplayAttributeDictionary = new Dictionary<string, OpenApiString>();

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
                {
                    propertiesDisplayAttributeDictionary.Add(
                        property.Name.ToLower(),
                        new OpenApiString(displayAttribute.GetName()));
                }
            }

            return propertiesDisplayAttributeDictionary;
        }

        private Dictionary<string, OpenApiBoolean> GetPropertiesHiddenInputAttributeDictionary(PropertyInfo[] properties)
        {
            var propertiesHiddenInputAttributeDictionary = new Dictionary<string, OpenApiBoolean>();

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute(typeof(HiddenInputAttribute)) is HiddenInputAttribute hiddenInputAttribute)
                {
                    propertiesHiddenInputAttributeDictionary.Add(
                        property.Name.ToLower(),
                        new OpenApiBoolean(hiddenInputAttribute.DisplayValue));
                }
            }

            return propertiesHiddenInputAttributeDictionary;
        }

        private Dictionary<string, OpenApiBoolean> GetPropertiesEnumFlagsAttributeDictionary(PropertyInfo[] properties)
        {
            return properties.Where(x => x.PropertyType.IsEnum)
                .Where(x => x.PropertyType.GetCustomAttribute(typeof(FlagsAttribute)) != null)
                .ToDictionary(property => property.Name.ToLower(), property => new OpenApiBoolean(true));
        }
    }
}