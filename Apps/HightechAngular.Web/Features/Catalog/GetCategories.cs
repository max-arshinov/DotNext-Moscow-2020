using System.Collections.Generic;
using Force.Cqrs;

namespace HightechAngular.Web.Features.Catalog
{
    public class GetCategories : IQuery<IEnumerable<CategoryListItem>>
    {
    }
}