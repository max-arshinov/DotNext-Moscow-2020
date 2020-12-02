using System;
using System.Threading.Tasks;
using Infrastructure.SwaggerSchema.Dropdowns.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SwaggerSchema.Dropdowns
{
    public static class Extensions
    {
        public static ParallelDropdownsBuilder<T> DropdownsFor<T>(
            this IServiceProvider sp) 
            where T : class =>
            Dropdowns.Create<T>(sp);
        
        public static async Task<TResult> InScopeAsync<TService, TResult>(this IServiceProvider serviceProvider,
            Func<TService, IServiceProvider, Task<TResult>> func)
        {
            using var scope = serviceProvider.CreateScope();
            return await func(scope.ServiceProvider.GetService<TService>(), scope.ServiceProvider);
        }
    }
}