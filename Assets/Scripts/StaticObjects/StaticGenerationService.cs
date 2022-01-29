using System;
using System.Collections.Generic;
using ChunkSystem.ChunkElements;
using Enums.StaticObjects;
using PoolFactory;
using StaticObjects.ObstacleDataTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StaticObjects
{
    public class StaticGenerationService : MonoBehaviour, IStaticGenerationService
    {
        [Header("Tools"), Space]
        [SerializeField] private bool useCombination;

        [Header("Data"), Space]
        [SerializeField] private ObstacleData obstacleData;
        [SerializeField] private EnvironmentData environmentData;
        [SerializeField] private PlaneData planeData;

        private readonly Dictionary<ChunkPlatform, List<RoadElement>> _currentRoadElements = 
            new Dictionary<ChunkPlatform, List<RoadElement>>();
        private ChunkPlatform _currentChunk;
        private RoadElement _currentRoadElement;

        private Pool<RoadElement>[] _obstaclePool;
        private Pool<RoadElement>[] _environmentPool;
        private Pool<RoadElement>[] _planePool;
        
        private const int CountOfObjectsInPool = 5;
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
        
        public void ProceedChunkGeneration(ChunkPlatform platform)
        {
            _currentChunk = platform;
            _currentChunk.PlatformReset += DeactivateRoadElements;
            
            var environment = GetRoadElement(StaticObjectType.Environment);
            var obstacle = GetRoadElement(StaticObjectType.Obstacle);
            var plane = GetRoadElement(StaticObjectType.Plane);

            var roadElementsList = new List<RoadElement> {environment, obstacle, plane};
            _currentRoadElements.Add(platform, roadElementsList);
            _currentRoadElement = null;
        }
        
        private void DeactivateRoadElements(ChunkPlatform platform)
        {
            if (_currentRoadElements.TryGetValue(platform, out var list))
            {
                foreach (var roadElement in list)
                {
                    if(roadElement == null) continue;
                    
                    var poolID = roadElement.PoolID;

                    switch (roadElement.ObjectType)
                    {
                        case StaticObjectType.Obstacle:
                            _obstaclePool[poolID].Release(roadElement);
                            break;
                        case StaticObjectType.Plane:
                            _planePool[poolID].Release(roadElement);
                            break;
                        case StaticObjectType.Environment:
                            _environmentPool[poolID].Release(roadElement);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                _currentRoadElements.Remove(platform);
                platform.PlatformReset -= DeactivateRoadElements;
            }
        }

        private RoadElement GetRoadElement(StaticObjectType objectType)
        {
            switch (objectType)
            {
                case StaticObjectType.Obstacle:
                    _currentRoadElement = GetDataObject(_obstaclePool);
                    break;
                case StaticObjectType.Plane:
                    _currentRoadElement = GetDataObject(_planePool);
                    break;
                case StaticObjectType.Environment:
                    _currentRoadElement = GetDataObject(_environmentPool);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(objectType), objectType, null);
            }

            if (_currentRoadElement == null) return null;
            
            PlaceToLocalZero(_currentRoadElement);
            _currentRoadElement.Activate();
            return _currentRoadElement;
        }

        private RoadElement GetDataObject(Pool<RoadElement>[] targetPool)
        {
            if (targetPool == null) return null;
            
            var randomPool = GetRandomPool(targetPool);
            var poolID = Array.IndexOf(targetPool, randomPool);
            var roadElement = randomPool.Allocate();
            
            roadElement.PoolID = poolID;
            return roadElement;
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