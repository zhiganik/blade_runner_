using System;
using System.Collections.Generic;
using ChunkSystem.ChunkElements;
using PoolFactory;
using StaticObstacles.ObstacleDataTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StaticObstacles
{
    public class StaticGenerationService : MonoBehaviour, IStaticGenerationService
    {
        [SerializeField] private ObstacleData obstacleData;
        [SerializeField] private EnvironmentData environmentData;
        [SerializeField] private PlaneData planeData;
        
        private readonly List<RoadElement> _currentSpawnedDataObjects = new List<RoadElement>();
        private ChunkPlatform _currentChunk;
        
        private Pool<RoadElement>[] _obstaclePool;
        private Pool<RoadElement>[] _environmentPool;
        private Pool<RoadElement>[] _planePool;
        
        private const int CountOfObjectsInPool = 3;
        private const string BaseScopeForPool = "StaticGenerationPools";
        
        private void Awake()
        {
            var poolParent = new GameObject(BaseScopeForPool);
            
            CreatePool(ref _obstaclePool, obstacleData, poolParent);
            CreatePool(ref _environmentPool, environmentData, poolParent);
            CreatePool(ref _planePool, planeData, poolParent);
        }

        private void CreatePool(ref Pool<RoadElement>[] targetPool, BaseData data, GameObject parent)
        {
            var length = data.GetLenghtOfList();
            
            if(length.Equals(0)) return;
            
            targetPool = new Pool<RoadElement>[length];

            for (var index = 0; index < length; index++)
            {
                var dataObject = data.GetObject(index);
                
                targetPool[index] = new Pool<RoadElement>(new 
                    PrefabPoolFactory<RoadElement>(dataObject.gameObject, parent), CountOfObjectsInPool);
            }
        }
        
        public void ProceedChunk(ChunkPlatform platform)
        {
            _currentChunk = platform;
            GenerateEnvironment();
            //GenerateObstacle();
            GeneratePlane();
            _currentChunk.Activate();
        }

        private void GenerateEnvironment()
        {
            var dataObject = GetDataObject(_environmentPool);
            PlaceToLocalZero(dataObject);
            dataObject.Activate();
        }
        
        private void GenerateObstacle()
        {
            var dataObject = GetDataObject(_obstaclePool);
            PlaceToLocalZero(dataObject);
            dataObject.Activate();
        }
        
        private void GeneratePlane()
        {
            var dataObject = GetDataObject(_planePool);
            PlaceToLocalZero(dataObject);
            dataObject.Activate();
        }
        
        private RoadElement GetDataObject(Pool<RoadElement>[] targetPool)
        {
            var randomPool = GetRandomPool(targetPool);
            var poolID = Array.IndexOf(targetPool, randomPool);
            var dataObject = randomPool.Allocate();
            
            InitializeNewChunk(dataObject, poolID);
            return dataObject;
        }
        
        private void InitializeNewChunk(RoadElement targetRoadElement, int poolID)
        {
            targetRoadElement.PoolID = poolID;
            _currentSpawnedDataObjects.Add(targetRoadElement);
        }
        
        private Pool<RoadElement> GetRandomPool(Pool<RoadElement>[] targetPool)
        {
            var pool = targetPool[Random.Range(0, targetPool.Length)];
            return pool;
        }

        private void PlaceToLocalZero(RoadElement roadElement)
        {
            roadElement.transform.position = _currentChunk.transform.localPosition;
        }
    }
}