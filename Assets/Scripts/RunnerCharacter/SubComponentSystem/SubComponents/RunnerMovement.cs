using UnityEngine;
using UnityEngine.InputSystem;

namespace BladeRunner
{
    public class RunnerMovement : SubComponent
    {
        [SerializeField] private InputActionReference playerMovement;

        //Move speed of the character in m/s
        [Space]
        [SerializeField] private float moveSpeed = 2.0f;
        [Space]
        //How fast the character turns to face movement direction
        [Range(0.0f, 0.3f)]
        [SerializeField] private float rotationSmoothTime = 0.12f;
        //Acceleration and deceleration
        [SerializeField] private float speedChangeRate = 10.0f;

        private float speed;
        private float targetRotation = 0.0f;
        private float rotationVelocity;
        private MovementData movementData;

        private Rigidbody RunnerRigidBody => RunnerControl.Rigidbody;
        private RunnerAnimator RunnerAnimator => RunnerControl.Animator;
        private Vector2 InputMovement => playerMovement.action.ReadValue<Vector2>();
        private void Start()
        {
            movementData = new MovementData
            {
                Speed = 0f,
            };
            subComponentProcessor.movementData = movementData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_MOVEMENT] = this;
        }

        public override void OnFixedUpdate()
        {
            ProcessPlayerMovement();
            ProcessPlayerRotation();
        }

        public override void OnUpdate()
        {
              
        }
        private void ProcessPlayerMovement()
        {
            RunnerRigidBody.velocity = (CalculateDirection() * (CalculateSpeed()) + new Vector3(0.0f, subComponentProcessor.jumpData.VerticalVelocity, 0.0f));
        }
        
        private float CalculateSpeed()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = moveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            //Vector2 MOVEMENT = PlayerMovement.action.ReadValue<Vector2>();

            //a reference to the players current horizontal velocity
            var velocity = RunnerRigidBody.velocity;
            float currentHorizontalSpeed = new Vector3(velocity.x, 0.0f, velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = InputMovement.magnitude;
            if (InputMovement == Vector2.zero)
            {
                inputMagnitude = 1f;
            }

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, speedChangeRate);

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            RunnerAnimator.SetForward(speed);
            return speed;
        }
        private Vector3 CalculateDirection()
        {
            Vector3 inputDirection = new Vector3(InputMovement.x, 0.0f, InputMovement.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (InputMovement != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                targetRotation = Mathf.Clamp(targetRotation, -45f, 45f);
            }
            else
            {
                targetRotation = 0f;
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward/*(-runnerControl.transform.right)*/;

            return targetDirection.normalized;
        }
        private void ProcessPlayerRotation()
        {
            float rotation = Mathf.SmoothDampAngle(RunnerControl.transform.eulerAngles.y,
                                                   targetRotation,
                                                   ref rotationVelocity,
                                                   rotationSmoothTime);
            RunnerAnimator.SetTurn(InputMovement.x);
            RunnerControl.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
}
