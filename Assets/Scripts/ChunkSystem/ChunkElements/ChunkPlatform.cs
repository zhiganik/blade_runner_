using System;
using System.Linq;
using Enums;
using PoolFactory;
using UnityEngine;

namespace ChunkSystem
{
    public class ChunkPlatform : MonoBehaviour, IPoolObject
    {
        [SerializeField] private int chunkCountInPool;
        [SerializeField] private PlatformType platformType;
        [SerializeField] private ChunkNode[] chunkNodes;

        [SerializeField] private BoxCollider groundCollider;
        
        public event Action onPlatformReset;

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
        
        public void Reset()
        {
            gameObject.SetActive(false);
            onPlatformReset?.Invoke();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}