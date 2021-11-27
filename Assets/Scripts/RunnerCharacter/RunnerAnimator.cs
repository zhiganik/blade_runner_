using System.Collections;
using UnityEngine;

namespace BladeRunner
{
    public class RunnerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private JumpData jumpData;
        private bool inJump;
        private int recentJumpType = 0;
        private float JumpDuration => jumpData.FallTimeout + jumpData.JumpTimeout;
        
        private const float Duration = 0.1f;
        private const float JumpValue = 5f;
        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int TurnKey = Animator.StringToHash("Turn");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int JumpTypeKey = Animator.StringToHash("JumpType");
        private static readonly int CrounchKey = Animator.StringToHash("Crounch");
        
        public void SetJumpData(JumpData data)
        {
            if (data != null)
                jumpData = data;
        }
        
        
        public void SetGround(bool value)
        {
            animator.SetBool(GroundedKey, value);
        }

        public void SetForward(float value)
        {
            animator.SetFloat(ForwardKey, value, Duration, Time.deltaTime);
        }

        public void SetTurn(float value)
        {
            animator.SetFloat(TurnKey, value, Duration, Time.deltaTime);
        }

        public void SetJump()
        {
           
            if(!inJump)
            {
                inJump = true;
                recentJumpType = Random.Range(0, 2);
            }
            animator.SetFloat(JumpKey, 6, jumpData.JumpTimeout, Time.deltaTime);
            animator.SetFloat(JumpTypeKey, recentJumpType);
        }

        public void ResetJump()
        {
            animator.SetFloat(JumpKey, -2, jumpData.JumpTimeout, Time.deltaTime);
            inJump = false;

        }

        public void SetCrunch()
        {
            animator.SetTrigger(CrounchKey);
        }
    }
}