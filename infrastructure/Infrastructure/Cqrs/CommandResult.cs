using System;
using System.Threading.Tasks;
using Force.Ccc;
using Infrastructure.Workflow;

namespace Infrastructure.Cqrs
{
    public class CommandResult<T>: Result<T, FailureInfo>
    {
        public CommandResult(T success) : base(success)
        {
        }

        public CommandResult(FailureInfo failure) : base(failure)
        {
        }
        
        public static implicit operator CommandResult<T>(Exception e) => 
            new CommandResult<T>(new ExceptionFailureInfo(e));

        public static implicit operator CommandResult<T>(FailureInfo failure) => 
            new CommandResult<T>(failure);

        public static implicit operator CommandResult<T>(T success) => 
            new CommandResult<T>(success);

        public CommandResult<TMap> Map<TMap>(Func<T, TMap> map) =>
            Match(
                x => new CommandResult<TMap>(map(x)), 
                x => new CommandResult<TMap>(x));
    }

    public static class HandlerResultExtensions
    {
        public static async Task<CommandResult<TOut>> MapAsync<TIn, TOut>(
            this Task<CommandResult<TIn>> resultTask,
            Func<TIn, TOut> map) =>
            (await resultTask).Map(map);
    }
}