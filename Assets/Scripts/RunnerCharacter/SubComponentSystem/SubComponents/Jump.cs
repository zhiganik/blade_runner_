using System;
using UnityEditor;
using UnityEngine;

namespace BladeRunner
{
    public class Jump : SubComponent
    {
        private JumpData jumpData;
        private bool grounded;
        
        private float Velocity 
        {
            get
            {
                if (jumpData == null) return 0;
                return Mathf.Abs(jumpData.JumpHeight * -2f * jumpData.Gravity);
            }
        }
        
        private void OnValidate()
        {
            type = SubComponentType.Jump;
        }

        public override void SetComponent(Runner runner, Data data)
        {
            this.runner = runner;
            if (data is MovementData movementData)
                jumpData = movementData.JumpData;
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            if (CheckGround())
                JumpCalculate();
            
            DownCalculate();
            
            jumpData.VerticalVelocity += jumpData.Gravity * Time.deltaTime;
        }

        private void JumpCalculate()
        {
            if (jumpData.VerticalVelocity <= 0f)
                jumpData.VerticalVelocity = -2f;
            
            if (Input.Direction == SwipeDirection.Up && jumpData.JumpTimeoutDelta <= 0.0f)
                jumpData.VerticalVelocity = Mathf.Sqrt(Velocity);

            if (jumpData.JumpTimeoutDelta >= 0.0f)
                jumpData.JumpTimeoutDelta -= Time.deltaTime;
        }

        private void DownCalculate()
        {
            jumpData.JumpTimeoutDelta = jumpData.JumpTimeout;

            if (Input.Direction != SwipeDirection.Down) return;
            
            jumpData.VerticalVelocity = -Mathf.Sqrt(Velocity);
            runner.RunnerAnimator.SetCrunch();
        }

        private bool CheckGround()
        {
            var spherePosition = runner.transform.position;
            spherePosition.y -= jumpData.Ground.GroundOffset;
            grounded = Physics.CheckSphere(spherePosition, jumpData.Ground.GroundRadius, jumpData.Ground.GroundMask,
                QueryTriggerInteraction.Ignore);
            runner.RunnerAnimator.SetGround(grounded);
            return grounded;
        }

        // private void OnDrawGizmos()
        // {
        //     if(runner)
        //     {
        //         var spherePosition = runner.transform.position;
        //         spherePosition.y -= jumpData.Ground.GroundOffset;
        //         Gizmos.DrawSphere(spherePosition, jumpData.Ground.GroundRadius);
        //     }
        // }
    }
}