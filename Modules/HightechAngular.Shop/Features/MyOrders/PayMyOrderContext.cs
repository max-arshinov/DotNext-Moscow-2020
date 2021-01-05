using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class PayMyOrderContext : 
        IntEntityOperationContextBase<PayMyOrder, Order>, 
        ICommand<Task<CommandResult<OrderStatus>>>
    {
        public PayMyOrderContext(PayMyOrder request, IUnitOfWork unitOfWork) : base(request, unitOfWork) { }
    }
}