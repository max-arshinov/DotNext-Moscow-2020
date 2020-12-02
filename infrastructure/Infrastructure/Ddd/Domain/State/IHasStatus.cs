using System;

namespace Infrastructure.Ddd.Domain.State
{
    public interface IHasStatus<out T> 
        where T : Enum
    {
        T Status { get; }
    }
}