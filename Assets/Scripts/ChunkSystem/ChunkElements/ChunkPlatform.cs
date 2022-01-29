using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using PoolFactory;
using StaticObstacles.ObstacleDataTools;
using UnityEngine;
using Zenject;

namespace ChunkSystem.ChunkElements
{
    public class DiChunkFactory : PlaceholderFactory<ChunkPlatform>{}
    
    public class ChunkPlatform : MonoBehaviour, IPoolObject
    {
        [SerializeField] private int chunkCountInPool;
        [SerializeField] private PlatformType platformType;
        [SerializeField] private ChunkNode[] chunkNodes;
        [SerializeField] private BoxCollider groundCollider;

        private event Action OnPlatformReset;
        
        public event Action PlatformReset
        {
            add => OnPlatformReset += value;
            remove => OnPlatformReset -= value;
        }
        
        public int PoolID { get; set; }
        public BoxCollider CurrentGroundCollider => groundCollider;

        public int GetChunkCountInPool => chunkCountInPool;

        public ChunkNode GetChunkNode(ChunkType chunkType)
        {
            var targetChunk = chunkNodes
                .Where(chunk => chunk.chunkType == chunkType)
                .Select(chunk => chunk)
                .FirstOrDefault();
            return targetChunk;
        }
        
        public void OnReset()
        {
            gameObject.SetActive(false);
            OnPlatformReset?.Invoke();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}