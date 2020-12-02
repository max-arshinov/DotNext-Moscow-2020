using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class DataTypeAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "dataType";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(DataTypeAttribute)) is DataTypeAttribute dataTypeAttribute)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiString(dataTypeAttribute.GetDataTypeName()));
            }
        }
    }
}