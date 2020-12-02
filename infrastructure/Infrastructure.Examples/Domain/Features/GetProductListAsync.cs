using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;

namespace Infrastructure.Examples.Domain.Features
{
    public class GetProductListAsync: IQuery<Task<IEnumerable<ProductListItem>>>
    {
        public string Name { get; set; }
    }
}