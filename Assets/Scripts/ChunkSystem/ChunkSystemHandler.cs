using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using InputSystem;
using ObserverSystem;
using PoolFactory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ChunkSystem
{
    public class ChunkSystemHandler : MonoBehaviour, IChunkSystemService, INotifyObserver<ChunkCreationType>
    {
        [Header("Chunk Tools")]
        [SerializeField] private int startChunkCount;
        [Space(5)]
        [SerializeField] private ChunkPlatform[] chunkPrefabs;
        [SerializeField] private List<ChunkPlatform> currentSpawnedChunks;
        
        private Pool<ChunkPlatform>[] _platformPool;
        private CameraTest _player;
        private readonly List<INotifyListener<ChunkCreationType>> _notifyListeners = 
            new List<INotifyListener<ChunkCreationType>>();
        
        private event Action OnSpawnNewPlatform;

        private const float PositionTolerance = 10f;
        
        [Inject]
        private void Construct(CameraTest runnerControl)
        {
            _player = runnerControl;
        }
        
        private void Awake()
        {
            InitializeSubscribersAndComponents();
            InitializePool();
            SpawnChunks();
        }

        private void InitializeSubscribersAndComponents()
        {
            OnSpawnNewPlatform += () => CreateChunk(1);
        }

        public ChunkPlatform GetFirstPlatform()
        {
            return currentSpawnedChunks.First();
        }

        private void InitializePool()
        {
            _platformPool = new Pool<ChunkPlatform>[chunkPrefabs.Length];

            for (var index = 0; index < chunkPrefabs.Length; index++)
            {
                var chunk = chunkPrefabs[index];
                
                _platformPool[index] = new Pool<ChunkPlatform>(new 
                    PrefabPoolFactory<ChunkPlatform>(chunk.gameObject), chunk.GetChunkCountInPool);
            }
        }

        private void Update()
        {
            //currentSpawnedChunks[0];
            if (_player.transform.position.z > GetFirstSpawnedChunkEndPosition().z + PositionTolerance)
            {
                NotifySubscribers(ChunkCreationType.RemoveChunk);
                DeleteChunk();
            }
        }

        public void CreateChunk(int platformCount)
        {
            for (var i = 0; i < platformCount; i++)
            {
                var randomPool = GetRandomPool();
                var poolID = Array.IndexOf(_platformPool, randomPool);
                var platform = randomPool.Allocate();
                
                CalculatePositionForChunk(platform);
                InitializeNewChunk(platform, poolID);
            }
            
            NotifySubscribers(ChunkCreationType.AddChunk);
        }

        public void DeleteChunk()
        {
            var platform = currentSpawnedChunks[0];
            var poolID = platform.PoolID;
            
            _platformPool[poolID].Release(platform);
            UnSubscribeOnPlatformResetEvent(currentSpawnedChunks[0]);
            currentSpawnedChunks.Remove(currentSpawnedChunks[0]);
        }
        
        private void SpawnChunks()
        {
            CreateStartChunk();
            CreateChunk(startChunkCount);
        }

        private void CreateStartChunk()
        {
            if (currentSpawnedChunks.Count != 0) return;
            
            var randomPool = GetRandomPool();
            var poolID = Array.IndexOf(_platformPool, randomPool);
            var startPlatform = randomPool.Allocate();

            startPlatform.transform.position = Vector3.zero;
            InitializeNewChunk(startPlatform, poolID);
        }

        private void InitializeNewChunk(ChunkPlatform targetPlatform, int poolID)
        {
            targetPlatform.PoolID = poolID;
            SubscribeOnPlatformResetEvent(targetPlatform);
            currentSpawnedChunks.Add(targetPlatform);
            targetPlatform.Activate();
        }

        private void CalculatePositionForChunk(ChunkPlatform platform)
        {
            platform.transform.position = currentSpawnedChunks[currentSpawnedChunks.Count - 1]
                .GetChunkNode(ChunkType.EndChunk).nodeTransform.position - platform
                .GetChunkNode(ChunkType.StartChunk).nodeTransform.localPosition;
        }

        private Vector3 GetFirstSpawnedChunkEndPosition()
        {
            return currentSpawnedChunks[0].
                GetChunkNode(ChunkType.EndChunk).
                nodeTransform.position;
        }

        private Pool<ChunkPlatform> GetRandomPool()
        {
            var pool = _platformPool[Random.Range(0, _platformPool.Length)];
            return pool;
        }

        private void SubscribeOnPlatformResetEvent(ChunkPlatform targetPlatform)
        {
            targetPlatform.onPlatformReset += OnSpawnNewPlatform;
        }
        
        private void UnSubscribeOnPlatformResetEvent(ChunkPlatform targetPlatform)
        {
            targetPlatform.onPlatformReset -= OnSpawnNewPlatform;
        }

        private void NotifySubscribers(ChunkCreationType creationType)
        {
            if(_notifyListeners.Count == 0) return;
            
            foreach (var listener in _notifyListeners)
            {
                listener.ReceiveNotification(creationType);
            }
        }

        public void AddListener(INotifyListener<ChunkCreationType> notifier)
        {
            _notifyListeners.Add(notifier);
        }

        public void RemoveListener(INotifyListener<ChunkCreationType> notifier)
        {
            _notifyListeners.Remove(notifier);
        }
    }
}