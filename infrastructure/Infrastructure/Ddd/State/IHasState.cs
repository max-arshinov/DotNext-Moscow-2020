namespace Infrastructure.Ddd.State
{
    public interface IHasState<out T>
    {
        T State { get; }
    }
}