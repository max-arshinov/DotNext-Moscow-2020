using Force.Cqrs;
using MediatR;

namespace Infrastructure.Tests.Mocks
{
    public class CommandMock: ICommand, IRequest<Unit>
    {
        
    }
}