using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;
using JetBrains.Annotations;

namespace HightechAngular.Shop.Features.Catalog
{
    [UsedImplicitly]
    public class GetCategoriesQueryHandler : GetIntEnumerableQueryHandlerBase<GetCategories, Category, CategoryListItem>
    {
        public GetCategoriesQueryHandler(IQueryable<Category> queryable) : base(queryable) { }
    }
}