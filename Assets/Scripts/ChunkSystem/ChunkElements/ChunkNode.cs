using System;
using Assets.Enums.ChunkEnums;
using UnityEngine;

namespace Assets.ChunkSystem.ChunkElements
{
    [Serializable]
    public class ChunkNode
    {
        public Transform nodeTransform;
        public ChunkType chunkType;
    }
}
