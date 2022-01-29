using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StaticObjects.ObstacleDataTools
{
    public abstract class BaseData : ScriptableObject
    {
        [SerializeField] protected List<RoadElement> listOfData;
        
        public virtual RoadElement GetRandomObject()
        {
            var randomIndex = GetRandomIndexOfList();
            return listOfData[randomIndex];
        }

        public virtual RoadElement GetObject(int index)
        {
            return listOfData[index];
        }
        
        public List<RoadElement> GetAllObjects()
        {
            return listOfData;
        }

        public int GetLenghtOfList()
        {
            return listOfData.Count;
        }

        private int GetRandomIndexOfList()
        {
            return Random.Range(0, listOfData.Count);
        }
    }
}