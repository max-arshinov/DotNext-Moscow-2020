using Force.Cqrs;
using Infrastructure.AspNetCore;

namespace Infrastructure.Examples.Domain.Features
{
    public class DeleteProduct: IdRequestBase<int>, ICommand
    {
    }
}