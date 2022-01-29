using System;
using System.Linq;
using Enums;
using PoolFactory;
using UnityEngine;
using Zenject;

namespace ChunkSystem.ChunkElements
{
    public class DiChunkFactory : PlaceholderFactory<ChunkPlatform>{}
    
    public class ChunkPlatform : MonoBehaviour, IPoolObject
    {
        [SerializeField] private int chunkCountInPool;
        [SerializeField] private ChunkNode[] chunkNodes;
        [SerializeField] private BoxCollider groundCollider;

        private event Action<ChunkPlatform> OnPlatformReset;
        
        public event Action<ChunkPlatform> PlatformReset
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
            OnPlatformReset?.Invoke(this);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}