using System;

namespace Infrastructure.Ddd.State
{
    public abstract class IntHasStateBase<TStatus, TState> :
        HasStateBase<int, TStatus, TState>
        where TStatus : Enum { }
}