using System.Collections.Generic;
using System.Linq;

namespace PoolFactory
{
    public class Pool<T> where T : IPoolObject
    {
        private readonly List<T> _poolObjects = new List<T>();
        private readonly HashSet<T> _unavailableObjects = new HashSet<T>();
        private readonly IPoolFactory<T> _poolPoolFactory;

        private const int DefaultPoolSize = 5;

        public Pool(IPoolFactory<T> factory, int poolSize = DefaultPoolSize)
        {
            _poolPoolFactory = factory;

            for(var i = 0; i < poolSize; i++)
            {
                Create();
            }
        }
        
        private T Create() 
        {
            var pool = _poolPoolFactory.Create();
            _poolObjects.Add(pool);
            return pool;
        }
        
        //Get from pool
        public T Allocate()
        {
            foreach (var poolObject in _poolObjects.Where(poolObject => !_unavailableObjects.Contains(poolObject)))
            {
                _unavailableObjects.Add(poolObject);
                return poolObject;
            }
            
            var newMembers = Create();
            _unavailableObjects.Add(newMembers);
            return newMembers;
        }
        
        //Return to pool
        public void Release(T poolObject)
        {
            poolObject.OnReset();
            _unavailableObjects.Remove(poolObject);
        }
    }
}