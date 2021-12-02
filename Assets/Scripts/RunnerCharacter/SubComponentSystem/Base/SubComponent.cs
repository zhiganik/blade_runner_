using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public abstract class SubComponent : MonoBehaviour
    {
        [SerializeField] protected SubComponentType type;

        protected Runner runner;
        protected Data data;
        protected SubComponentProcessor processor;

        public Runner Runner
        {
            get => runner;
            
            set
            {
                if (value != null)
                    runner = value;
            }
        }
        
        public SubComponentType Type => type;

        private void Awake()
        {
            processor = GetComponentInParent<SubComponentProcessor>();
            AwakeComponent();
        }

        protected virtual void AwakeComponent()
        {
            
        }

        public void SetComponent(Runner runner, Data data)
        {
            this.runner = runner;
            this.data = data;
        }
        
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}