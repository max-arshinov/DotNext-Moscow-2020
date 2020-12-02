using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HightechAngular.Orders.Entities;
using Infrastructure.SwaggerSchema.Dropdowns;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using Microsoft.AspNetCore.Http;

namespace HightechAngular.Shop.Features.Catalog
{
    public class ProductsDropdownProvider : IDropdownProvider<ProductListItem>
    {
        private readonly IServiceProvider _serviceProvider;
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly int _currentCategoryId;

        public ProductsDropdownProvider(IServiceProvider serviceProvider, 
            IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _currentCategoryId = GetCurrentCategory();
        }

        public Task<Dropdowns> GetDropdownOptionsAsync()
        {
            return _serviceProvider.DropdownsFor<ProductListItem>()
                .With(x => x.Name)
                .As<Product, string>(q => q
                    .Where(x => x.Category.Id == _currentCategoryId)
                    .ToDropdownOption(x => x.Name, x => x.Name))
                .With(x => x.Price)
                .As<Product, double>(q => q
                    .Where(x => x.Category.Id == _currentCategoryId)
                    .Select(x => x.Price)
                    .ToDropdownOption(x => x.ToString(CultureInfo.InvariantCulture), x => x));
        }

        private int GetCurrentCategory()
        {
            _httpContextAccessor.HttpContext.Request.Query.TryGetValue("categoryId", out var categoryStr);
            return int.TryParse(categoryStr, out int category)? category : 1;
        }
    }
}