using System.Collections.Generic;
using Force.Cqrs;

namespace Infrastructure.Examples.Domain.Features
{
    public class GetProductList: IQuery<IEnumerable<ProductListItem>>
    {
        public string Name { get; set; }
    }
}