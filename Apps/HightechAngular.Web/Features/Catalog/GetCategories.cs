using Force.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Catalog
{
    public class GetCategories : IQuery<IEnumerable<CategoryListItem>>
    {
    }
}
