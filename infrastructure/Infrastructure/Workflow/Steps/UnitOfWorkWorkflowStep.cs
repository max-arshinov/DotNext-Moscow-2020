using System;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;

namespace Infrastructure.Workflow.Steps
{
    public class UnitOfWorkWorkflowStep<TRequest, TReturn> :
        IWorkflowStep<TRequest, TReturn>,
        IAsyncWorkflowStep<TRequest, TReturn>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkWorkflowStep(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TReturn, FailureInfo>> ProcessAsync(TRequest request,
            Func<TRequest, Task<Result<TReturn, FailureInfo>>> next)
        {
            var res = await next(request);
            if (!res.IsFaulted)
            {
                // https://habr.com/ru/post/283522/
                Dispatch((dynamic) request);
            }

            return res;
        }

        public Result<TReturn, FailureInfo> Process(TRequest request, Func<TRequest, Result<TReturn, FailureInfo>> next)
        {
            var res = next(request);
            if (!res.IsFaulted)
            {
                Dispatch((dynamic) request);
            }

            return res;
        }

        private void Dispatch<T>(ICommand<T> command)
        {
            _unitOfWork.Commit();
        }

        private void Dispatch(ICommand command)
        {
            _unitOfWork.Commit();
        }

        private void Dispatch(object command) { }
    }
}