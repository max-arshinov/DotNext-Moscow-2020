using System.Reflection;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Any;

namespace Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers
{
    public class DisableFormControlIfTargetPropertyIsNotNullAttributeHandler : AbstractCustomAttributeHandler
    {
        protected override string Key { get; set; } = "disableFormControl";

        public override void FillProperties(PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(DisableFormControlIfTargetPropertyIsNotNullAttribute)) is
                DisableFormControlIfTargetPropertyIsNotNullAttribute disableFormControlIfTargetPropertyIsNotNull)
            {
                Properties.Add(
                    property.Name.ToLower(),
                    new OpenApiString(disableFormControlIfTargetPropertyIsNotNull.TargetPropertyName));
            }
        }
    }
}