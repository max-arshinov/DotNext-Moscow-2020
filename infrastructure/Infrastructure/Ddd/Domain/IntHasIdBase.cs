using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Ddd.Domain
{
    public abstract class IntHasIdBase: HasIdBase<int>
    {}
    
    public abstract class HasIdBase<TKey>
        where TKey: IEquatable<TKey>
    {
        [Key, Required, HiddenInput]
        public virtual TKey Id { get; set; }
    }
}