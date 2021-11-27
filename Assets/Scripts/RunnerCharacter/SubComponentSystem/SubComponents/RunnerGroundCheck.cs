using BladeRunner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BladeRunner
{
    public class RunnerGroundCheck : SubComponent
    {
        //Useful for rough ground
        [SerializeField] private float groundedOffset = -0.14f;
        //The radius of the grounded check. Should match the radius of the CharacterController
        [SerializeField] private float groundedRadius = 0.28f;
        //What layers the character uses as ground
        [SerializeField] private LayerMask groundLayers;

        private GroundData _groundData;
        void Start()
        {
            _groundData = new GroundData
            {
                Grounded = false,

                GroundedOffset = groundedOffset,
                GroundedRadius = groundedRadius,
                
                GroundLayers = groundLayers,
            };
            subComponentProcessor.groundData = _groundData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_GROUND_CHECK] = this;
        }

        public override void OnFixedUpdate()
        {
            GroundedCheck();
        }

        public override void OnUpdate()
        {
            
        }
        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(RunnerControl.transform.position.x,
                                                 RunnerControl.transform.position.y - _groundData.GroundedOffset,
                                                 RunnerControl.transform.position.z);
            _groundData.Grounded =
                Physics.CheckSphere(spherePosition, _groundData.GroundedRadius, _groundData.GroundLayers, QueryTriggerInteraction.Ignore);

            RunnerControl.Animator.SetGround(_groundData.Grounded);
        }
    }
}