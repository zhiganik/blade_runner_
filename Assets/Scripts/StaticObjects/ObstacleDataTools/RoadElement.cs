using Enums.StaticObjects;
using PoolFactory;
using UnityEngine;

namespace StaticObjects.ObstacleDataTools
{
    public class RoadElement : MonoBehaviour, IPoolObject
    {
        [SerializeField] private StaticObjectType objectType;
        
        public int PoolID { get; set; }

        public StaticObjectType ObjectType => objectType;

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