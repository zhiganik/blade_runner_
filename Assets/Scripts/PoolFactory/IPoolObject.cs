namespace Assets.PoolFactory
{
    public interface IPoolObject
    {
        public int PoolID { get; set; }

        public void Reset();
    }
    
}