using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Force.Cqrs;
using Infrastructure.Examples.Data;
using Infrastructure.Tests.Mocks;
using Infrastructure.Workflow;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests
{
    //[SimpleJob(RunStrategy.Monitoring, launchCount: 10, warmupCount: 3, targetCount: 100)]
    public class WorkflowBenchmark
    {
        private IServiceProvider _sp;
        
        public WorkflowBenchmark()
        {
            var ob = new DbContextOptionsBuilder<ExampleDbContext>();
            ob.UseInMemoryDatabase("Infrastructure");

            _sp = Services.BuildServiceProvider(
                sp => new ExampleDbContext(),
                typeof(CommandMock).Assembly);
        }

        [Benchmark]
        public async Task MediatR()
        {
            var handler = _sp.GetService<IMediator>();
            await handler.Send(new CommandMock(), CancellationToken.None);
        }

        [Benchmark]
        public void ProcessManually()
        {
            var handler = _sp.GetService<ICommandHandler<CommandMock>>();
            handler.Handle(new CommandMock());
        }

        [Benchmark]
        public void ProcessNoIoc()
        {
            var uh = new CommandMockHandler();
            uh.Handle(new CommandMock());
        }

        [Benchmark]
        public void Process()
        {
            var command = new CommandMock();
            // dynamic workflow = _sp.GetService(typeof(IWorkflow<,>).MakeGenericType(command.GetType(), typeof(object)));

            // var workflow = _sp.GetService<IWorkflow<CommandMock, object>>();
            // var result = workflow.Process(command, _sp);

            var wf = new HandlerWorkflow<CommandMock, object>();
            wf.Process(command, _sp);
        }
    }
}