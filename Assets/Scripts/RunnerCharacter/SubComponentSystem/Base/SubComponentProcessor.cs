using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public enum SubComponentType
    {
        PLAYER_INPUT,
        CHARACTER_MOVEMENT,
        CHARACTER_JUMP,
        CHARACTER_GROUND_CHECK,
        CHARACTER_CRUNCH,
        CHARACTER_ATTACK,

        COUNT,
    }
    public class SubComponentProcessor : MonoBehaviour
    {
        public SubComponent[] ArrSubComponents;

        [HideInInspector] public RunnerControl control;

        public MovementData movementData;
        public GroundData groundData;
        public JumpData jumpData;

        private void Awake()
        {
            ArrSubComponents = new SubComponent[(int)SubComponentType.COUNT];
            control = GetComponentInParent<RunnerControl>();
        }

        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.CHARACTER_MOVEMENT);
            FixedUpdateSubComponent(SubComponentType.CHARACTER_GROUND_CHECK);
        }
        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponentType.CHARACTER_JUMP);
            UpdateSubComponent(SubComponentType.CHARACTER_CRUNCH);
        }

        private void FixedUpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnFixedUpdate();
            }
        }
        private void UpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnUpdate();
            }
        }
    }
}

