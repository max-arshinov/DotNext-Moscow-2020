using System.Collections.Generic;
using Force.Cqrs;

namespace HightechAngular.Shop.Features.Catalog
{
    public class GetCategories: IQuery<IEnumerable<CategoryListItem>>
    {
    }
}