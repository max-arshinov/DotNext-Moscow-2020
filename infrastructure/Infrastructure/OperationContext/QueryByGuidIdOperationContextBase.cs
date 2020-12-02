using System;
using Force.Ddd;

namespace Infrastructure.OperationContext
{
    public class QueryByGuidIdOperationContextBase<TQuery, TRequest> : QueryByIdOperationContextBase<Guid, TQuery, TRequest>
        where TRequest : class, IHasId<Guid>
    {
        public QueryByGuidIdOperationContextBase(TRequest request) : base(request)
        {
        }
    }
}