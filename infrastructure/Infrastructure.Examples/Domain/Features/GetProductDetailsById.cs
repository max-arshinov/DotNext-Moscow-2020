using Force.Cqrs;
using Infrastructure.AspNetCore;

namespace Infrastructure.Examples.Domain.Features
{
    public class GetProductDetailsById: IdRequestBase<int>, IQuery<ProductDetails>
    {
    }
}