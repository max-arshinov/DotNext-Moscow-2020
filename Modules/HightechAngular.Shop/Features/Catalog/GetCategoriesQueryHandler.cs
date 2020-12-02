using System.Linq;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs.Read;

namespace HightechAngular.Shop.Features.Catalog
{
    public class GetCategoriesQueryHandler: GetIntEnumerableQueryHandlerBase<GetCategories, Category, CategoryListItem>
    {
        public GetCategoriesQueryHandler(IQueryable<Category> queryable) : base(queryable)
        {
        }
    }
}