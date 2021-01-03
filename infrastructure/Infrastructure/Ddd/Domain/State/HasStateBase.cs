using System;

namespace Infrastructure.Ddd.Domain.State
{
    public abstract class HasStateBase<TKey, TStatus, TState> :
        EntityBase<TKey>,
        IHasStatus<TStatus>,
        IHasState<TState>
        where TStatus : Enum
        where TKey : IEquatable<TKey>
    {
        private TState _state;
        private TStatus _status;

        public TState State => _state ??= GetState(Status);

        public TStatus Status
        {
            get => _status;
            protected set
            {
                _status = value;
                _state = GetState(_status);
            }
        }

        public abstract TState GetState(TStatus status);

        public void With<T>(Action<T> action)
            where T : class, TState
        {
            if (State is T state)
            {
                action(state);
            }
        }

        public TResult With<T, TResult>(Func<T, TResult> func, TResult ifFalse = default)
            where T : class, TState
        {
            if (State is T state)
            {
                return func(state);
            }

            return ifFalse;
        }

        public TCurrentState As<TCurrentState>()
            where TCurrentState : TState
        {
            return State is TCurrentState currentState
                ? currentState
                : default;
        }

        protected T To<T>(TStatus status)
            where T : TState
        {
            Status = status;
            return (T) State;
        }

        public static explicit operator TState(HasStateBase<TKey, TStatus, TState> hasStatus)
        {
            return hasStatus.State;
        }
    }
}