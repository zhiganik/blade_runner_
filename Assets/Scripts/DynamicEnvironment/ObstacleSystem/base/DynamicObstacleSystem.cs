using AudioSystem.AudioService;
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
    public class DynamicObstacleSystem : MonoBehaviour, IDynamicObstacleSystemService, IAudioReceiver
    {
        [Header("Chunk Tools")]
        [SerializeField] private int startChunkCount;
        [Space(5)]
        [SerializeField] private DynamicObstacle[] obstaclePrefabs;

        private Dictionary<DynamicObstacleType, Pool<DynamicObstacle>> poolDict = new Dictionary<DynamicObstacleType, Pool<DynamicObstacle>>();
        private CameraTest _player;
        private IChunkSystemService _chunkSystemHandler;

        float prevMom;

        [Inject]
        private void Construct(CameraTest runnerControl, IChunkSystemService chunkSystemHandler, IAudioSystemService audioSystemService)
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
            StartCoroutine(KickBeatRoutine(DynamicObstacleType.TestCube));
            StartCoroutine(SnareBeatRoutine(DynamicObstacleType.TestSphere));
            StartCoroutine(HatBeatRoutine(DynamicObstacleType.TestCylinder));
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
        public DynamicObstacle SpawnObstacle(DynamicObstacleType type, Vector3 spawnPosition, float force)
        {
            DynamicObstacle obstacle = SpawnObstacle(type, spawnPosition);
            obstacle.RigidBody.velocity =
                (_player.transform.position - obstacle.transform.position).normalized * force;
            return obstacle;
        }

        public void DestroyObstacle(DynamicObstacle obstacle)
        {
            obstacle.Reset();
            poolDict[obstacle.Type].Release(obstacle);
        }      
        private IEnumerator KickBeatRoutine(DynamicObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.Bass, 0.7f, 0.1f));
                DynamicObstacle obstacle = SpawnObstacle(obstacleType,
                                                  _player.transform.position + new Vector3(0f, 2f, 15f));
                StartCoroutine(DestroyRoutine(obstacle));
            }
        }

        private IEnumerator SnareBeatRoutine(DynamicObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.UpperMidrange, 0.5f, 0.25f));
                DynamicObstacle obstacle = SpawnObstacle(obstacleType,
                                                  _player.transform.position + new Vector3(0f, 2f, 15f), 50f);
                StartCoroutine(DestroyRoutine(obstacle));
            }
        }

        private IEnumerator HatBeatRoutine(DynamicObstacleType obstacleType)
        {
            while (true)
            {
                yield return new WaitUntil(() => AllowSpawn(AudioBandType.HigherMidrange, 0.5f, 0.1f));
                DynamicObstacle obstacle = SpawnObstacle(obstacleType,
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

        private IEnumerator DestroyRoutine(DynamicObstacle obstacle)
        {
            yield return new WaitForSeconds(1f);
            DestroyObstacle(obstacle);
        }

        public void ReceiveAudioData()
        {
            
        }
    }
}
