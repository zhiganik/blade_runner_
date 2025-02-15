using System;
using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;

namespace Assets.RunnerCharacter.ProcessorSystem
{
    public class SwipeSubProcessor : MonoBehaviour
    {
        private Runner runner;

        public Action<SwipeDirection> SwipeProcess;
        
        public Runner Runner
        {
            get => runner;
            set
            {
                if (value != null)
                    runner = value;
            }
        }

        private void Start()
        {
            SwipeProcess += Process;
        }
        
        protected virtual void Process(SwipeDirection swipe)
        {
            
        }
        
    }
}