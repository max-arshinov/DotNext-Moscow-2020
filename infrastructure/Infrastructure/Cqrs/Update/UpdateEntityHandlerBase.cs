using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd;
using Infrastructure.Ddd;
using Infrastructure.Workflow;

namespace Infrastructure.Cqrs.Update
{
    public abstract class UpdateEntityHandlerBase<TKey, TEntity, TCommand> :
        IHasUnitOfWork,
        ICommandHandler<TCommand>,
        IValidator<TCommand>
        where TEntity : class, IHasId<TKey>
        where TCommand : ICommand, IHasId<TKey>
        where TKey : IEquatable<TKey>
    {
        private IUnitOfWork _uow;

        IUnitOfWork IHasUnitOfWork.UnitOfWork
        {
            get => _uow;
            set => _uow = value;
        }

        protected abstract void UpdateEntity(TEntity entity, TCommand command);

        public void Handle(TCommand input)
        {
            var entity = _uow.Find<TEntity>(input.Id);
            UpdateEntity(entity, input);
            _uow.Commit();
        }

        public IEnumerable<ValidationResult> Validate(TCommand obj)
        {
            yield return ValidationResult.Success;
        }
    }
}