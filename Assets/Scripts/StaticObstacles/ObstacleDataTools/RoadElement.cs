using PoolFactory;
using UnityEngine;

namespace StaticObstacles.ObstacleDataTools
{
    public class RoadElement : MonoBehaviour, IPoolObject
    {
        public int PoolID { get; set; }
        
        public void OnReset()
        {
            gameObject.SetActive(false);
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}