namespace Assets.PoolFactory
{
    public class PoolFactory<T> : IPoolFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}