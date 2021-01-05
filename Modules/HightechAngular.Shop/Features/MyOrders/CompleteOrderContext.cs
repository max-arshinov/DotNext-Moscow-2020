using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.Cqrs;
using Infrastructure.OperationContext;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class CompleteOrderContext :
        IntEntityOperationContextBase<CompleteOrder, Order>,
        ICommand<Task<CommandResult<OrderStatus>>>
    {
        public CompleteOrderContext(CompleteOrder request, IUnitOfWork unitOfWork) : base(request, unitOfWork) { }
    }
}