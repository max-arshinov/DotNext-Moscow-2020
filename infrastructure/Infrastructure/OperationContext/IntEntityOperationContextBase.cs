using Force.Ccc;
using Force.Ddd;
using JetBrains.Annotations;

namespace Infrastructure.OperationContext
{
    [PublicAPI]
    public abstract class IntEntityOperationContextBase<TRequest, TEntity> : 
        EntityOperationContextBase<int, TRequest, TEntity> 
        where TRequest : class, IHasId<int> 
        where TEntity : class, IHasId<int> 
    {
        protected IntEntityOperationContextBase(TRequest request, IUnitOfWork unitOfWork) : 
            base(request, unitOfWork) { }
    }
}