using System;
using System.Linq;
using AudioSystem.AudioService;
using Enums;
using InputSystem;
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

        [SerializeField] private ChunkData _chunkData;
        
        public event Action onPlatformReset;

        public int PoolID { get; set; }
        public BoxCollider CurrentGroundCollider => groundCollider;
        public ChunkData ChunkData => _chunkData;

        public int GetChunkCountInPool => chunkCountInPool;

        public void InitializeChunkData(AudioSystemHandler audioSystemHandler, CameraTest cameraTest)
        {
            var chunkData = new ChunkData();
            chunkData.AudioSystemHandler = audioSystemHandler;
            chunkData.CameraTest = cameraTest;
            _chunkData = chunkData;
        }
        
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


    [Serializable]
    public class ChunkData
    {
        public AudioSystemHandler AudioSystemHandler;
        public CameraTest CameraTest;
    }
}