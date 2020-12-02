using Force.Ddd;

namespace Infrastructure.OperationContext
{
    public class QueryByLongIdOperationContextBase<TQuery, TRequest> : QueryByIdOperationContextBase<long, TQuery, TRequest>
        where TRequest : class, IHasId<long>
    {
        public QueryByLongIdOperationContextBase(TRequest request) : base(request)
        {
        }
    }
}