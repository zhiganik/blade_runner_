using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BladeRunner
{
    public class Input : SubComponent
    {
        public static SwipeDirection Direction { get; private set; }
        public static bool Tap;        
        private CharacterControl input;
        private InputAction primary;
        private InputAction position;
        private InputAction tap;
        private Swipe recentSwipe;
        
        private bool skipSwipeFrame = false;
        private bool skipTapFrame = false;
        private bool swipeInProgress = false;
        
        private const float MaxDistance = 30f;

        private void OnValidate()
        {
            type = SubComponentType.Input;
        }
        
        protected override void AwakeComponent()
        {
            GetInputs();
        }
        
        private void GetInputs()
        {
            input = new CharacterControl();
            input.Enable();
            primary =  input.Character.SwipePrimary;
            position = input.Character.SwipePosition;
            tap = input.Character.Tap;
            primary.started += StartSwipe;
            tap.started += StartTap;
            primary.canceled += EndSwipe;
        }

        private void StartTap(InputAction.CallbackContext context)
        {
            Tap = true;
            skipTapFrame = false;
        }

        private void StartSwipe(InputAction.CallbackContext context)
        {
            var touchPosition = position.ReadValue<Vector2>();
            swipeInProgress = true;
            recentSwipe = new Swipe()
            {
                StartPosition = touchPosition,
            };
            
            Direction = SwipeDirection.None;
        }

        private void EndSwipe(InputAction.CallbackContext context)
        {
            if(swipeInProgress)
            {
                swipeInProgress = false;
                if (recentSwipe == null) return;
                recentSwipe.EndPosition = position.ReadValue<Vector2>();
                Direction = Utils.CalculateDirection(recentSwipe.StartPosition, recentSwipe.EndPosition);
                recentSwipe = null;
            }
        }

        private void OnEnable()
        {
            input?.Enable();
        }

        private void OnDisable()
        {
            input?.Disable();
        }

        private void CheckTap()
        {
            if (Tap && skipTapFrame)
            {
                Tap = false;
                skipTapFrame = false;
                Debug.Log("tap");
            }
            else
                skipTapFrame = true;
        }
        
        private void CheckSwipe()
        {
            if (Direction != SwipeDirection.None && skipSwipeFrame)
            {
                Direction = SwipeDirection.None;                
                skipSwipeFrame = false;
            }
            else
            {
                skipSwipeFrame = true;
            }

            if (recentSwipe != null)
            {
                recentSwipe.EndPosition = position.ReadValue<Vector2>();
                if(recentSwipe.Distance >= MaxDistance && swipeInProgress)
                {
                    Direction = Utils.CalculateDirection(recentSwipe.StartPosition, recentSwipe.EndPosition);
                    swipeInProgress = false;
                    recentSwipe = null;
                }
            }
        }
        
        public override void OnUpdate()
        {
            
        }

        public override void OnFixedUpdate()
        {
            CheckTap();
            CheckSwipe();
        }
    }
}
