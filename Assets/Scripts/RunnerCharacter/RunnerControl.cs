using UnityEngine;

namespace BladeRunner
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(RunnerAnimator))]
    public class RunnerControl : MonoBehaviour
    {
        private SubComponentProcessor subComponentProcessor;
        
        public Rigidbody Rigidbody { get; private set; }
        public RunnerAnimator Animator { get; private set; }
        

        private void Awake()
        {
            subComponentProcessor = GetComponentInChildren<SubComponentProcessor>();
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<RunnerAnimator>();
        }
        private void FixedUpdate()
        {
            subComponentProcessor.FixedUpdateSubComponents();
        }
        private void Update()
        {
            subComponentProcessor.UpdateSubComponents();
        }
    }
}
