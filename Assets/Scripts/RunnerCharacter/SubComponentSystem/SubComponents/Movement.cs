using System;
using UnityEngine;

namespace BladeRunner
{
    public class Movement : SubComponent
    {
        private MovementData movementData;

        private void OnValidate()
        {
            type = SubComponentType.Movement;
        }

        public override void SetComponent(Runner runner, Data data)
        {
            this.runner = runner;
            if (data is MovementData movementData)
                this.movementData = movementData;
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            ProcessMovement();
        }

        private void ProcessMovement()
        {
            JumpVelocity();
            Move();
        }

        private void JumpVelocity()
        {
            var jumpVelocity = runner.RunnerRigidbody.velocity;
            jumpVelocity.y = movementData.JumpData.VerticalVelocity;
            runner.RunnerRigidbody.velocity = jumpVelocity;
        }

        private void Move()
        {            
            var speed = movementData.Speed * Time.deltaTime;

            var moveTo = runner.transform.position;
            moveTo.z +=  speed;
            moveTo.x = movementData.StrafeData.HorizontalVelocity;
            
            runner.RunnerRigidbody.MovePosition(moveTo);
            runner.RunnerAnimator.SetForward(movementData.Speed);
        }
    }
}