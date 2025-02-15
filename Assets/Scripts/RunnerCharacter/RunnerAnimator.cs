using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;

namespace Assets.RunnerCharacter
{
    public class RunnerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private JumpData jumpData;
        private bool inJump;
        private int recentJumpType = 0;
        private float JumpDuration => jumpData.FallTimeout + jumpData.JumpTimeout;
        
        private const float Duration = 0.1f;
        private const float StrafeDuration = 0.5f;
        
        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int AttackKey = Animator.StringToHash("Attack");
        private static readonly int LeftStrafeKey = Animator.StringToHash("Left Strafe");
        private static readonly int RightStrafeKey = Animator.StringToHash("Right Strafe");
        private static readonly int CrounchKey = Animator.StringToHash("Crounch");
        
        public void SetGround(bool value)
        {
            animator.SetBool(GroundedKey, value);
        }

        public void SetAttack()
        {
            animator.SetTrigger(AttackKey);
        }
        
        public void SetForward(float value)
        {
            animator.SetFloat(ForwardKey, value, Duration, Time.deltaTime);
        }

        public void SetStrafe(int value)
        {
            switch (value)
            {
                case -1: 
                    animator.SetTrigger(LeftStrafeKey);
                    break;
                case 1:
                    animator.SetTrigger(RightStrafeKey);
                    break;
            }
        }
        
        public void SetCrunch()
        {
            animator.SetTrigger(CrounchKey);
        }
    }
}