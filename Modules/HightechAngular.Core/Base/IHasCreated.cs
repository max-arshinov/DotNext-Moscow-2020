using System;

namespace HightechAngular.Orders.Base
{
    public interface IHasCreatedDateString
    {
        DateTime Created { get; }
        
        string CreatedString { get; }
    }
}