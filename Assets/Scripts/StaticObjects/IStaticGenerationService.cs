using ChunkSystem.ChunkElements;

namespace StaticObjects
{
    public interface IStaticGenerationService
    {
        public void ProceedChunkGeneration(ChunkPlatform platform);
    }
}