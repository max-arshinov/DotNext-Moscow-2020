using System;
using Force.Ddd;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.AspNetCore
{
    public class IdRequestBase<T>: HasIdBase<T>
        where T : IEquatable<T>
    {
        [FromRoute]
        public override T Id
        {
            get => base.Id; 
            set => base.Id = value;
        }
    }
}