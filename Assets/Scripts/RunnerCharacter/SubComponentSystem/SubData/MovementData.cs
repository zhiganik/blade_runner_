
using UnityEngine;

namespace BladeRunner
{
    [System.Serializable]
    public class MovementData : Data
    {
        [SerializeField] private float speed;
        [SerializeField] private StrafeData strafeData;
        [SerializeField] private JumpData jumpData;
        
        public float Speed => speed;
        public StrafeData StrafeData => strafeData; 
        public JumpData JumpData => jumpData;
    }
}