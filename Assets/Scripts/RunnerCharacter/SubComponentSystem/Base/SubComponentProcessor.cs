using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public enum SubComponentType
    {
        Input,
        Movement,
        Jump,
        GroundCheck,
        Crunch,
        Attack,

        COUNT,
    }
    public class SubComponentProcessor : MonoBehaviour
    {
        [SerializeField] private List<SubComponent> arrSubComponents;
        [SerializeField] private MovementData movementData;
        [SerializeField] private GroundData groundData;
        [SerializeField] private JumpData jumpData;
        
        private Runner runner;
        private Dictionary<SubComponentType, SubComponent> components;

        private void Awake()
        {
            runner = GetComponentInParent<Runner>();
            SetComponents();
        }

        private void SetComponents()
        {
            components = new Dictionary<SubComponentType, SubComponent>();
            
            foreach (var subComponent in arrSubComponents)
            {
                components.Add(subComponent.Type, subComponent);
            }
            
            SetComponent(SubComponentType.Input, runner, null);
            SetComponent(SubComponentType.Movement, runner, movementData);
            SetComponent(SubComponentType.GroundCheck, runner, groundData);
            SetComponent(SubComponentType.Jump, runner, jumpData);
            SetComponent(SubComponentType.Crunch, runner, null);
        }

        private void SetComponent(SubComponentType type, Runner runner, Data data)
        {
            if(components.ContainsKey(type))
                components[type].SetComponent(runner, data);
        }

        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.Input);
            FixedUpdateSubComponent(SubComponentType.Movement);
            // FixedUpdateSubComponent(SubComponentType.GroundCheck);
        }
        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponentType.Jump);
            UpdateSubComponent(SubComponentType.Crunch);
        }

        private void FixedUpdateSubComponent(SubComponentType type)
        {
            if (components.ContainsKey(type))
            {
                components[type].OnFixedUpdate();
            }
        }
        private void UpdateSubComponent(SubComponentType type)
        {
            if (components.ContainsKey(type))
            {
                components[type].OnUpdate();
            }
        }
    }
}

