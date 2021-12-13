using System;
using RunnerCharacter.ProcessorSystem;
using UnityEngine;

namespace BladeRunner
{
    public class Strafe : SubComponent
    {
        [SerializeField] private StrafeData strafeData;
        private bool skipFrame;
        private int currentLine = 0;
        
        public override void SetComponent(Runner runner, Data data)
        {
            this.runner = runner;
            if (data is MovementData movementData)
                strafeData = movementData.StrafeData;
        }
        
        public override void OnUpdate()
        {
        }
        
        public override void OnFixedUpdate()
        {
            if(!runner || strafeData == null) return;
            ChangeLine();
            StrafeCalculate();
        }

        private void ChangeLine()
        {
            var direction = Input.Direction;
            
            if (ValidationDirection(direction))
            {
                if (!skipFrame)
                {
                    var strafe = 0;
                    switch(direction)
                    {
                        case SwipeDirection.Left:
                            strafe -= 1;
                            break;
                        case SwipeDirection.Right:
                            strafe += 1;
                            break;
                    }

                    skipFrame = true;
                    currentLine += strafe;
                    currentLine = Mathf.Clamp(currentLine, -1, 1);
                    runner.RunnerAnimator.SetStrafe(strafe);
                }
            }
            else
            {
                skipFrame = false;
            }
        }

        private void StrafeCalculate()
        {
            var line = currentLine * strafeData.LinesOffset;
            var interpolation = Time.deltaTime * strafeData.StrafeSpeed;
            
            strafeData.HorizontalVelocity = Mathf.Lerp(strafeData.HorizontalVelocity, line, interpolation);
        }

        private bool ValidationDirection(SwipeDirection direction) =>
            direction == SwipeDirection.Left || direction == SwipeDirection.Right;
    }
}