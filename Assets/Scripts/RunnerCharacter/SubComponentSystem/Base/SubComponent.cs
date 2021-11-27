using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public abstract class SubComponent : MonoBehaviour
    {
        public RunnerControl RunnerControl => subComponentProcessor.control;

        protected SubComponentProcessor subComponentProcessor;

        private void Awake()
        {
            subComponentProcessor = gameObject.GetComponentInParent<SubComponentProcessor>();
        }
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}