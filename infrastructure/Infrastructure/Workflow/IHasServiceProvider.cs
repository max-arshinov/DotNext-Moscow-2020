using System;

namespace Infrastructure.Workflow
{
    internal interface IHasServiceProvider
    { 
        IServiceProvider ServiceProvider { get; set; }
    }
}