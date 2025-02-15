using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;

namespace Assets.RunnerCharacter.ProcessorSystem
{
    public class StrafeProcessor : SwipeSubProcessor
    {
        protected override void Process(SwipeDirection swipe)
        {
           
        }

        private void Strafe(Vector3 force)
        {
            force *= 3;
            Runner.RunnerRigidbody.MovePosition(transform.position + force);
        }
    }
}