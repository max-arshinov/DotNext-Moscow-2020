using System;
using System.ComponentModel.DataAnnotations;
using Force.Ccc;
using Force.Ddd;
using JetBrains.Annotations;

namespace Infrastructure.OperationContext
{
    [PublicAPI]
    public abstract class EntityOperationContextBase<TKey, TRequest, TEntity> : 
        ByIdOperationContextBase<TKey, TRequest>
        where TRequest : class, IHasId<TKey>
        where TEntity : class, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        protected EntityOperationContextBase(TRequest request, IUnitOfWork unitOfWork) : base(request)
        {
            Entity = unitOfWork.Find<TEntity>(request.Id);
        }
        
        [Required]
        public TEntity Entity { get; }
    }
}