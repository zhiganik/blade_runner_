using ChunkSystem.ChunkElements;

namespace StaticObstacles
{
    public interface IStaticGenerationService
    {
        public void ProceedChunk(ChunkPlatform platform);
    }
}