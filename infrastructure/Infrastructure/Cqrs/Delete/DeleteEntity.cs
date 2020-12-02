using System;
using Force.Ddd;

namespace Infrastructure.Cqrs.Delete
{
    public class DeleteEntity<TKey>: HasIdBase<TKey> 
        where TKey : IEquatable<TKey>
    {
    }
}