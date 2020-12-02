using System.Linq;
using Infrastructure.Ddd;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class ProductListFilter: IFilter<ProductListItem, GetProductList>
    {
        public IQueryable<ProductListItem> Filter(IQueryable<ProductListItem> queryable, GetProductList predicate) =>
            queryable.Where(x => x.Name == predicate.Name + "/Very Complex Logic");
    }
}