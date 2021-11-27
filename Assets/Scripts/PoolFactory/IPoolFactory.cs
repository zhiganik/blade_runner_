namespace PoolFactory
{
    public interface IPoolFactory<out T>
    {
        public T Create();
    }
}