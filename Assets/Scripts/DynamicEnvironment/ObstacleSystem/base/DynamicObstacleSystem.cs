using System.Collections;
using System.Collections.Generic;
using Assets.AudioSystem.AudioService;
using Assets.ChunkSystem;
using Assets.PoolFactory;
using Assets.TestFolder;
using UnityEngine;
using Zenject;

namespace Assets.DynamicEnvironment.ObstacleSystem.@base
{
    public class DynamicObstacleSystem : MonoBehaviour, IDynamicObstacleSystemService, IAudioReceiver
    {
        [SerializeField] private DynamicObstacle[] obstaclePrefabs;

        private Dictionary<DynamicObstacleType, Pool<DynamicObstacle>> poolDict = new Dictionary<DynamicObstacleType, Pool<DynamicObstacle>>();
        private CameraTest _player;
        private IChunkSystemService _chunkSystemHandler;
        private IAudioSystemService _audioSystemService;

        [Inject]
        private void Construct(CameraTest runnerControl, IChunkSystemService chunkSystemHandler, IAudioSystemService audioSystemService)
        {
            _player = runnerControl;
            _chunkSystemHandler = chunkSystemHandler;
            _audioSystemService = audioSystemService;
        }

        private void Awake()
        {
            InitializePool();
        }

        private void Start()
        {
            _audioSystemService.AddAudioReceiver(this);
        }

        private void InitializePool()
        {
            for (var index = 0; index < obstaclePrefabs.Length; index++)
            {
                var chunk = obstaclePrefabs[index];
                poolDict[chunk.Type] =
                    new Pool<DynamicObstacle>(new PrefabPoolFactory<DynamicObstacle>(chunk.gameObject), chunk.StartCountInPool);
            }
        }

        public DynamicObstacle SpawnObstacle(DynamicObstacleType type, Vector3 spawnPosition)
        {
            DynamicObstacle obstacle = poolDict[type].Allocate();
            obstacle.Spawn(spawnPosition, _player.transform.position, _chunkSystemHandler.GetFirstPlatform().CurrentGroundCollider);
            return obstacle;
        }

        public void DestroyObstacle(DynamicObstacle obstacle)
        {
            obstacle.Reset();
            poolDict[obstacle.Type].Release(obstacle);
        }

        public void ReceiveAudioData(float[] channels)
        {
            for (int i = 0; i < obstaclePrefabs.Length; i++)
            {
                if (obstaclePrefabs[i].rythmSpawnSettings.AllowSpawn(channels))
                {
                    DynamicObstacle obstacle = SpawnObstacle(obstaclePrefabs[i].Type,
                                  _player.transform.position + new Vector3(0f, 2f, 25f));

                    StartCoroutine(DestroyRoutine(obstacle));
                }
            }
        }
        private IEnumerator DestroyRoutine(DynamicObstacle obstacle)
        {
            yield return new WaitForSeconds(1.5f);
            DestroyObstacle(obstacle);
        }
    }
}
