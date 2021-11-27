using ChunkSystem;
using InputSystem;
using PoolFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BladeRunner
{
    public class ObstacleSystem : MonoBehaviour
    {
        [Header("Chunk Tools")]
        [SerializeField] private int startChunkCount;
        [Space(5)]
        [SerializeField] private Obstacle[] obstaclePrefabs;
        [SerializeField] private List<Obstacle> currentSpawnedObstacles;

        private Dictionary<ObstacleType, Pool<Obstacle>> poolDict = new Dictionary<ObstacleType, Pool<Obstacle>>();
        private CameraTest _player;
        private IChunkSystemService _chunkSystemHandler;
        //private 

        [Inject]
        private void Construct(CameraTest runnerControl, IChunkSystemService chunkSystemHandler)
        {
            _player = runnerControl;
            _chunkSystemHandler = chunkSystemHandler;
        }

        private void Awake()
        {
            InitializePool();
        }

        private void Start()
        {
            StartCoroutine(SpawnRandomObstacles());
        }
        private void InitializePool()
        {
            for (var index = 0; index < obstaclePrefabs.Length; index++)
            {
                var chunk = obstaclePrefabs[index];
                poolDict[chunk.Type] =
                    new Pool<Obstacle>(new PrefabPoolFactory<Obstacle>(chunk.gameObject), chunk.StartCountInPool);;
            }
        }

        private Obstacle SpawnObstackle(ObstacleType type, Vector3 spawnPosition, Vector3 force = default)
        {
            Obstacle obstacle = poolDict[type].Allocate();
            obstacle.Spawn(spawnPosition, _player.transform.position, force, _chunkSystemHandler.GetFirstPlatform().CurrentGroundCollider);
            return obstacle;
        }
        
        private IEnumerator SpawnRandomObstacles()
        {
            while (true)
            {
                ObstacleType type;
                int rand = Random.Range(-2, 2);
                Obstacle obstacle;
                if (rand >= 0)
                {
                    type = ObstacleType.TestCube;
                    obstacle = SpawnObstackle(type, _player.transform.position + new Vector3(-10f, 2f, 0f));
                }
                else
                {
                    type = ObstacleType.TestSphere;
                    obstacle = SpawnObstackle(type, _player.transform.position + new Vector3(-25f, 2f, 0f), _player.transform.position);
                }

                yield return new WaitForSeconds(2f);
                poolDict[type].Release(obstacle);
            }
        } 
    }
}
