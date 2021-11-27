namespace ChunkSystem
{
    public interface IChunkSystemService
    {
        public void CreateChunk(int platformCount);
        public void DeleteChunk();
        public ChunkPlatform GetFirstPlatform();
    }
}