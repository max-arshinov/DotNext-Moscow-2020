using HightechAngular.Shop;
using Infrastructure.SwaggerSchema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HightechAngular.Web
{
    public static class StartupExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Hightech Swagger API",
                    Version = "v1"
                });
                c.DocumentFilter<SchemaDocumentFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header, Description = "JWT Token",
                    Name = "Authorization", Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}},
                        new string[] { }
                    }
                });
            });
        }
    }
}