using System;
using System.Diagnostics;
using BladeRunner;
using UnityEngine;
using Zenject;

namespace RunnerCharacter.ProcessorSystem
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