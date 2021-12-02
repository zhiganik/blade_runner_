using System;
using RunnerCharacter.ProcessorSystem;
using UnityEngine;

namespace BladeRunner
{
    public class Movement : SubComponent
    {
        private Vector3 Move;
        private float speed = 5f;
        private float sideSpeed = 2f;
        private float offset = 3f;
        private bool didChangeLastFrame = false;
        private int laneNamber;
        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            Move.z = speed;
            Move *= Time.deltaTime;
            var direction = Input.Direction;
            Debug.Log(direction);
            if (direction == SwipeDirection.Left || direction == SwipeDirection.Right)
            {
                if (!didChangeLastFrame)
                {
                    switch(direction)
                    {
                        case SwipeDirection.Left:
                            laneNamber -= 1;
                            break;
                        case SwipeDirection.Right:
                            laneNamber += 1;
                            break;
                    }
                    
                    Debug.Log(1);
                    laneNamber = Mathf.Clamp(laneNamber, -1, 1);
                }
            }
            else
            {
                didChangeLastFrame = false;
            }

            var pos = transform.position;
            pos.x = Mathf.Lerp(pos.z, laneNamber * offset, Time.deltaTime + sideSpeed);
            pos += Move;
            runner.transform.position = pos;
            runner.RunnerRigidbody.MovePosition(Move);
        }
    }
}