using AudioSystem.AudioVisualizer;
using ChunkSystem;
using InputSystem;
using PoolFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BladeRunner
{
    public class ObstacleSystem : MonoBehaviour, IObstacleSystemService
    {
        [Header("Chunk Tools")]
        [SerializeField] private int startChunkCount;
        [Space(5)]
        [SerializeField] private Obstacle[] obstaclePrefabs;

        private Dictionary<ObstacleType, Pool<Obstacle>> poolDict = new Dictionary<ObstacleType, Pool<Obstacle>>();
        private CameraTest _player;
        private IChunkSystemService _chunkSystemHandler;

        float prevMom;

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
            prevMom = Time.time;
            //StartCoroutine(SpawnRandomObstacles());
            StartCoroutine(KickBeatRoutine(ObstacleType.TestCube));
            StartCoroutine(SnareBeatRoutine(ObstacleType.TestSphere));
            StartCoroutine(HatBeatRoutine(ObstacleType.TestCylinder));
        }

        private void InitializePool()
        {
            for (var index = 0; index < obstaclePrefabs.Length; index++)
            {
                var chunk = obstaclePrefabs[index];
                poolDict[chunk.Type] =
                    new Pool<Obstacle>(new PrefabPoolFactory<Obstacle>(chunk.gameObject), chunk.StartCountInPool);
            }
        }

        public Obstacle SpawnObstacle(ObstacleType type, Vector3 spawnPosition)
        {
            Obstacle obstacle = poolDict[type].Allocate();
            obstacle.Spawn(spawnPosition, _player.transform.position, _chunkSystemHandler.GetFirstPlatform().CurrentGroundCollider);
            return obstacle;
        }
        public Obstacle SpawnObstacle(ObstacleType type, Vector3 spawnPosition, float force)
        {
            Obstacle obstacle = SpawnObstacle(type, spawnPosition);
            obstacle.RigidBody.velocity =
                (_player.transform.position - obstacle.transform.position).normalized * force;
            return obstacle;
        }
        public void DestroyObstacle(Obstacle obstacle)
        {
            obstacle.Reset();
            poolDict[obstacle.Type].Release(obstacle);
        }

        //private IEnumerator SpawnRandomObstacles()
        //{
        //    while (true)
        //    {
        //        //ObstacleType type;
        //        //int rand = Random.Range(-2, 2);
        //        //Obstacle obstacle;
        //        //if (rand >= 0)
        //        //{
        //        //    type = ObstacleType.TestCube;
        //        //    obstacle = SpawnObstacle(type, _player.transform.position + new Vector3(0f, 2f, 15f));
        //        //}
        //        //else
        //        //{
        //        //    type = ObstacleType.TestSphere;
        //        //    obstacle = SpawnObstacle(type, _player.transform.position + new Vector3(0f, 2f, 25f), 50f);
        //        //}
        //        yield return new WaitUntil(() => AllowSpawn(AudioBandType.Bass));
        //        Obstacle obstacle = SpawnObstacle(ObstacleType.TestCube, _player.transform.position + new Vector3(0f, 2f, 15f));
        //        yield return new WaitForSeconds(1f);
        //        DestroyObstacle(obstacle);
        //    }
        //}
        
        private IEnumerator KickBeatRoutine(ObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.Bass, 0.7f, 0.1f));
                Obstacle obstacle = SpawnObstacle(obstacleType,
                                                  _player.transform.position + new Vector3(0f, 2f, 15f));
                StartCoroutine(DestroyRoutine(obstacle));
            }
        }

        private IEnumerator SnareBeatRoutine(ObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.UpperMidrange, 0.5f, 0.25f));
                Obstacle obstacle = SpawnObstacle(obstacleType,
                                                  _player.transform.position + new Vector3(0f, 2f, 15f), 50f);
                StartCoroutine(DestroyRoutine(obstacle));
            }
        }

        private IEnumerator HatBeatRoutine(ObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.HigherMidrange, 0.5f, 0.1f));
                Obstacle obstacle = SpawnObstacle(obstacleType,
                                                  _player.transform.position + new Vector3(0f, 2f, 15f), 50f);
                StartCoroutine(DestroyRoutine(obstacle));
            }
        }

        private bool AllowSpawn(AudioBandType bandType, float amp, float timeOut)
        {
            float val = AudioPeerSystem.AudioBandBuffer[(int)bandType];
            if (val >= amp)
            {
                if (Time.time > (prevMom + timeOut))
                {
                    //Debug.Log(val);
                    prevMom = Time.time;
                    return true;
                }
            }
            //Debug.Log(val);
            return false;
        }

        private IEnumerator DestroyRoutine(Obstacle obstacle)
        {
            yield return new WaitForSeconds(1f);
            DestroyObstacle(obstacle);
        }
    }
}
