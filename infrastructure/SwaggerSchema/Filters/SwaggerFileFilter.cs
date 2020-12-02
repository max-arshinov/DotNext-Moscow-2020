using System.Linq;
using Infrastructure.SwaggerSchema.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.SwaggerSchema.Filters
{
    public class SwaggerFileFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requestAttribute = context.MethodInfo.GetCustomAttributes(typeof(FileStreamContentTypeAttribute), false)
                .Cast<FileStreamContentTypeAttribute>()
                .FirstOrDefault();

            if (requestAttribute == null) return;

            var content = requestAttribute.ContentTypes
                .ToDictionary(contentType => contentType, contentType => 
                    new OpenApiMediaType {Schema = new OpenApiSchema {Type = "string", Format = "binary"}});

            operation.Responses.Add("200", new OpenApiResponse
            {
                Content = content
            });
        }
    }
}