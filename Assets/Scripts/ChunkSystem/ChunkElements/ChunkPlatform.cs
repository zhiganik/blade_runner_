using System;
using System.Linq;
using AudioSystem.AudioService;
using Enums;
using InputSystem;
using PoolFactory;
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

        public IAudioSystemService AudioSystemHandler { get; private set; }
        public CameraTest CameraTest { get; private set; }
        
        public event Action onPlatformReset;

        public int PoolID { get; set; }
        public BoxCollider CurrentGroundCollider => groundCollider;

        public int GetChunkCountInPool => chunkCountInPool;

        [Inject]
        private void Construct(IAudioSystemService audioSystemHandler, CameraTest cameraTest)
        {
            AudioSystemHandler = audioSystemHandler;
            CameraTest = cameraTest;
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
}