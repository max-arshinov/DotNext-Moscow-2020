using System;

namespace Infrastructure.Ddd.State
{
    public interface IHasStatus<out T>
        where T : Enum
    {
        T Status { get; }
    }
}