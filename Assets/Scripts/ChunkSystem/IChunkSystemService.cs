using System;
using ChunkSystem.ChunkElements;
using UnityEngine;

namespace ChunkSystem
{
    public interface IChunkSystemService
    {
        public event Action OnPlatformWasDeleted;
        public ChunkPlatform CreateChunk();
        public void DeleteChunk();
        public ChunkPlatform GetFirstPlatform();
        public Vector3 GetFirstSpawnedChunkEndPosition();
    }
}