namespace Infrastructure.OperationContext
{
    public abstract class OperationContextBase<T> : IOperationContext<T>
        where T : class
    {
        protected OperationContextBase(T request)
        {
            Request = request;
        }

        public T Request { get; }
    }
}