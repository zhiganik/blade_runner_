using UnityEngine;
using UnityEngine.InputSystem;

namespace BladeRunner
{
    public class RunnerJump : SubComponent
    {
        [SerializeField] private InputActionReference playerJump;
        [SerializeField] private float terminalVelocity = 53.0f;
        [SerializeField, Space] private float jumpHeight = 1.2f;
        [SerializeField] private float gravity = -15.0f;
        [SerializeField, Space] private float jumpTimeout = 0.10f;
        [SerializeField] private float fallTimeout = 0.15f;
        
        private JumpData jumpData;
        
        private void Start()
        {
            jumpData = new JumpData
            {
                VerticalVelocity = 0f,
                TerminalVelocity = terminalVelocity,
                JumpHeight = jumpHeight,
                Gravity = gravity,
                JumpTimeout = jumpTimeout,
                FallTimeout = fallTimeout,

                JumpTimeoutDelta = 0f,
                FallTimeoutDelta = 0f,
            };
            subComponentProcessor.jumpData = jumpData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_JUMP] = this;
            RunnerControl.Animator.SetJumpData(jumpData);
        }
        public override void OnFixedUpdate()
        {
            
        }

        public override void OnUpdate()
        {
            PlayerJumping();
        }
        private void PlayerJumping()
        {
            if (subComponentProcessor.groundData.Grounded)
            {
                // reset the fall timeout timer
                jumpData.FallTimeoutDelta = jumpData.FallTimeout;

                // stop our velocity dropping infinitely when grounded
                if (jumpData.VerticalVelocity < 0.0f)
                {
                    jumpData.VerticalVelocity = -2f;
                }

                // Jump
                if (playerJump.action.triggered && jumpData.JumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    //_jumpData.VerticalVelocity = Mathf.Sqrt(_jumpData.JumpHeight * -2f * _jumpData.Gravity);

                    jumpData.VerticalVelocity = jumpData.JumpHeight;
                }

                // jump timeout
                if (jumpData.JumpTimeoutDelta >= 0.0f)
                {
                    jumpData.JumpTimeoutDelta -= Time.deltaTime;
                }
                
                RunnerControl.Animator.ResetJump();
            }
            else
            {
                // reset the jump timeout timer
                jumpData.JumpTimeoutDelta = jumpData.JumpTimeout;

                // fall timeout
                if (jumpData.FallTimeoutDelta >= 0.0f)
                {
                    jumpData.FallTimeoutDelta -= Time.deltaTime;
                }
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (jumpData.VerticalVelocity < terminalVelocity)
            {
                jumpData.VerticalVelocity += jumpData.Gravity * Time.deltaTime;
            }
            
            if(!subComponentProcessor.groundData.Grounded)
                RunnerControl.Animator.SetJump();

        }
    }
}
