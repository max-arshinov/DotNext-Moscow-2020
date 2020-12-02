using Force.Ccc;

namespace Infrastructure.Workflow
{
    internal interface IHasUnitOfWork
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}