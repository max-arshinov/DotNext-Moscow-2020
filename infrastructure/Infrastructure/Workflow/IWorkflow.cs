using System;
using Force.Ccc;

namespace Infrastructure.Workflow
{
    public interface IWorkflow<in T, TResult>
    {
        Result<TResult, FailureInfo> Process(T request, IServiceProvider sp);
    }
}