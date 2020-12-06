using System;

namespace Infrastructure.Ddd.Domain.State
{
    public abstract class SingleStateBase<TEntity, TStatus> :
        StateBase<TEntity, TStatus>
        where TEntity : class, IHasStatus<TStatus>
        where TStatus : Enum
    {
        protected SingleStateBase(TEntity entity) : base(entity)
        {
        }

        public abstract TStatus EligibleStatus { get; }

        public override bool IsStatusEligible(TStatus status)
        {
            return status?.Equals(EligibleStatus) == true;
        }
    }
}