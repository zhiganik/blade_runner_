using System;
using System.Collections.Generic;
using System.Linq;
using ChunkSystem.ChunkElements;
using Enums;
using PoolFactory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ChunkSystem
{
    public class ChunkSystemHandler : MonoBehaviour, IChunkSystemService
    {
        [Space(5)]
        [SerializeField] private ChunkPlatform[] chunkPrefabs;
        [SerializeField] private List<ChunkPlatform> currentSpawnedChunks;
        
        private DiChunkFactory _diChunkFactory;
        private Pool<ChunkPlatform>[] _platformPool;

        private event Action ONPlatformWasDeleted;
        
        public event Action OnPlatformWasDeleted
        {
            add => ONPlatformWasDeleted += value;
            remove => ONPlatformWasDeleted -= value;
        }
        
        [Inject]
        private void Construct( DiChunkFactory placeholderFactory)
        {
            _diChunkFactory = placeholderFactory;
        }
        
        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            _platformPool = new Pool<ChunkPlatform>[chunkPrefabs.Length];

            for (var index = 0; index < chunkPrefabs.Length; index++)
            {
                var chunk = chunkPrefabs[index];
                
                _platformPool[index] = new Pool<ChunkPlatform>(new 
                    DiPoolFactory<ChunkPlatform>(chunk.gameObject, _diChunkFactory), chunk.GetChunkCountInPool);
            }
        }

        public ChunkPlatform GetFirstPlatform()
        {
            return currentSpawnedChunks.First();
        }
        
        public Vector3 GetFirstSpawnedChunkEndPosition()
        {
            return currentSpawnedChunks[0].
                GetChunkNode(ChunkType.EndChunk).
                nodeTransform.position;
        }

        public ChunkPlatform CreateChunk()
        {
            if (currentSpawnedChunks.Count == 0)
            {
                return CreateStartChunk();
            }
            
            var platform = AllocateChunk();
            
            return platform;
        }

        private ChunkPlatform CreateStartChunk()
        {
            var startPlatform = AllocateChunk();
            return startPlatform;
        }

        private ChunkPlatform AllocateChunk()
        {
            var randomPool = GetRandomPool();
            var poolID = Array.IndexOf(_platformPool, randomPool);
            var platform = randomPool.Allocate();

            CalculatePositionForChunk(platform);
            InitializeNewChunk(platform, poolID);
            return platform;
        }

        public void DeleteChunk()
        {
            var platform = currentSpawnedChunks[0];
            var poolID = platform.PoolID;
            
            _platformPool[poolID].Release(platform);
            currentSpawnedChunks.Remove(currentSpawnedChunks[0]);
            ONPlatformWasDeleted?.Invoke();
        }

        private void InitializeNewChunk(ChunkPlatform targetPlatform, int poolID)
        {
            targetPlatform.PoolID = poolID;
            currentSpawnedChunks.Add(targetPlatform);
        }

        private void CalculatePositionForChunk(ChunkPlatform platform)
        {
            if (currentSpawnedChunks.Count == 0)
            {
                platform.transform.position = Vector3.zero;
                return;
            }
            
            platform.transform.position = currentSpawnedChunks[currentSpawnedChunks.Count - 1]
                .GetChunkNode(ChunkType.EndChunk).nodeTransform.position - platform
                .GetChunkNode(ChunkType.StartChunk).nodeTransform.localPosition;
        }
        
        private Pool<ChunkPlatform> GetRandomPool()
        {
            var pool = _platformPool[Random.Range(0, _platformPool.Length)];
            return pool;
        }
    }
}