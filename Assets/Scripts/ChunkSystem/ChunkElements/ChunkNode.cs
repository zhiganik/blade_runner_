using System;
using Enums;
using UnityEngine;

namespace ChunkSystem
{
    [Serializable]
    public class ChunkNode
    {
        public Transform nodeTransform;
        public ChunkType chunkType;
    }
}
