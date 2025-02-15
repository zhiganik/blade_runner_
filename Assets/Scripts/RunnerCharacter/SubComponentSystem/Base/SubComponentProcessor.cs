using System.Collections.Generic;
using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;

namespace Assets.RunnerCharacter.SubComponentSystem.Base
{
    public enum SubComponentType
    {
        Input,
        Strafe,
        Jump,
        Movement,
        Crunch,
        Attack,

        COUNT,
    }
    public class SubComponentProcessor : MonoBehaviour
    {
        [SerializeField] private MovementData movementData;
        
        private Runner runner;
        [SerializeField] private SubComponent[] subComponents;
        private Dictionary<SubComponentType, SubComponent> components;

        private void Awake()
        {
            runner = GetComponentInParent<Runner>();
            SetComponents();
        }
        
        private void SetComponents()
        {
            components = new Dictionary<SubComponentType, SubComponent>();
            subComponents = GetComponentsInChildren<SubComponent>();
            
            foreach (var subComponent in subComponents)
            {
                components.Add(subComponent.Type, subComponent);
            }
            
            SetComponent(SubComponentType.Input, runner, null);
            SetComponent(SubComponentType.Strafe, runner, movementData);
            SetComponent(SubComponentType.Jump, runner, movementData);
            SetComponent(SubComponentType.Movement, runner, movementData);
            SetComponent(SubComponentType.Attack, runner, null);
        }

        private void SetComponent(SubComponentType type, Runner runner, Data data)
        {
            if(components.ContainsKey(type))
                components[type].SetComponent(runner, data);
        }

        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.Input);
            FixedUpdateSubComponent(SubComponentType.Strafe);
            FixedUpdateSubComponent(SubComponentType.Jump);
            FixedUpdateSubComponent(SubComponentType.Movement);
            FixedUpdateSubComponent(SubComponentType.Attack);
        }
        public void UpdateSubComponents()
        {
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

