using ChunkSystem;
using InputSystem;
using StaticObjects;
using UnityEngine;
using Zenject;

namespace BaseCore
{
    public class ChunkServiceManager : MonoBehaviour
    {
        [SerializeField] private int startChunkCount;
        
        private IStaticGenerationService _generationService;
        private IChunkSystemService _chunkService;
        private CameraTest _player;
        
        private const float PositionTolerance = 10f;

        [Inject]
        private void Construct(IStaticGenerationService newGenerationService, IChunkSystemService newChunkService,
            CameraTest player)
        {
            _generationService = newGenerationService;
            _chunkService = newChunkService;
            _player = player;
        }

        private void Start()
        {
            InitializeSubscribers();
            CreateStartChunks();
        }

        private void CreateStartChunks()
        {
            for (var i = 0; i < startChunkCount; i++)
            {
                GenerateChunk();
            }
        }

        private void InitializeSubscribers()
        {
            _chunkService.OnPlatformWasDeleted += GenerateChunk;
        }

        private void Update()
        {
            var firstChunkEndPosition = _chunkService.GetFirstSpawnedChunkEndPosition().z;
            if (_player.transform.position.z > firstChunkEndPosition + PositionTolerance)
            {
                _chunkService.DeleteChunk();
            }
        }

        private void GenerateChunk()
        {
            var newChunk = _chunkService.CreateChunk();
            _generationService.ProceedChunkGeneration(newChunk);
            newChunk.Activate();
        }
    }
}