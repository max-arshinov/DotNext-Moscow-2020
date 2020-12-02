using Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes;
using Infrastructure.SwaggerSchema.Filters.HandleCustomAttributes.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SwaggerSchema
{
    public static class StartupExtensions
    {
        public static void AddBaseAttributesHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, BoolInlineEditingAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, CustomHandlerAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, DataTypeAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, DisableFormControlIfTargetPropertyIsNotNullAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, DisplayAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, EnumFlagAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, HiddenAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, MediaDataAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, TextHighlighterAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, DefaultAttributeHandler>();
            serviceCollection.AddTransient<AbstractCustomAttributeHandler, HtmlEditorAttributeHandler>();
        }
    }
}