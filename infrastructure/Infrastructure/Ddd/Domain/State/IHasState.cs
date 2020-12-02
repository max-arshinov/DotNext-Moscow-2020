namespace Infrastructure.Ddd.Domain.State
{
    public interface IHasState<out T>
    {
        T State { get; }
    }
}