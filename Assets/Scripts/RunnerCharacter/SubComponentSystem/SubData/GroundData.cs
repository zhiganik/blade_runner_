using UnityEngine;

namespace BladeRunner
{
    [System.Serializable]
    public class GroundData : Data
    {
        public bool Grounded;
        public float GroundedOffset;
        public float GroundedRadius;

        public LayerMask GroundLayers;
    }
}