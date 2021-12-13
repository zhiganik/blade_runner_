
using UnityEngine;

namespace BladeRunner
{
    [System.Serializable]
    public class StrafeData : Data
    {
        [SerializeField] private float strafeSpeed;
        [SerializeField] private float linesOffset;

        public float HorizontalVelocity { get; set; }
        public float StrafeSpeed => strafeSpeed;
        public float LinesOffset => linesOffset;
    }
}