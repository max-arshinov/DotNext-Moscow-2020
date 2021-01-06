using System.Threading.Tasks;
using Force.Cqrs;
using HightechAngular.Admin.Features.OrderManagement;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ResolveDisputedOrderCommandHandler: ICommandHandler<ResolveDisputedOrder, Task>
    {
        public Task Handle(ResolveDisputedOrder input)
        {
            return Task.CompletedTask;
        }
    }
}