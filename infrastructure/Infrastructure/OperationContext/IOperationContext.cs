namespace Infrastructure.OperationContext
{
    // assembly-scanning optimization
    internal interface IOperationContext<out T>
    {
        T Request { get; }
    }
}