namespace PoolFactory
{
    public interface IPoolObject
    {
        public int PoolID { get; set; }

        public void OnReset();
    }
    
}