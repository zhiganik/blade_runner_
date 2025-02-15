using Assets.RunnerCharacter.SubComponentSystem.Base;
using UnityEngine;

namespace Assets.RunnerCharacter
{
    public class Runner : MonoBehaviour
    {
        [SerializeField] private SubComponentProcessor processor;
        [SerializeField] private Rigidbody runnerRigidbody;
        [SerializeField] private RunnerAnimator runnerAnimator;

        public Rigidbody RunnerRigidbody => runnerRigidbody;
        public RunnerAnimator RunnerAnimator => runnerAnimator;

        private void Start()
        {
            processor = GetComponentInChildren<SubComponentProcessor>();
            runnerRigidbody = GetComponent<Rigidbody>();
            runnerAnimator = GetComponent<RunnerAnimator>();
        }

        private void Update()
        {
            processor.UpdateSubComponents();
        }

        private void FixedUpdate()
        {
            processor.FixedUpdateSubComponents();
        }
    }
}
