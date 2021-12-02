using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace BladeRunner
{
    
    
    public class Input : SubComponent
    {
        private CharacterControl input;
        private InputAction primary;
        private InputAction position;
        private bool skipFrame = false;
        private Swipe recentSwipe;
        public static SwipeDirection Direction { get; private set; }

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
            primary.started += StartSwipe;
            primary.canceled += EndSwipe;
        }

        private void StartSwipe(InputAction.CallbackContext context)
        {
            var touchPosition = position.ReadValue<Vector2>();
            recentSwipe = new Swipe()
            {
                StartPosition = touchPosition,
            };
            Direction = SwipeDirection.None;
        }

        private void EndSwipe(InputAction.CallbackContext context)
        {
            if(recentSwipe == null) return;
            recentSwipe.EndPosition = position.ReadValue<Vector2>();
            Direction = Utils.CalculateDirection(recentSwipe.StartPosition, recentSwipe.EndPosition);
        }

        private void OnEnable()
        {
            input?.Enable();
        }

        private void OnDisable()
        {
            input?.Disable();
        }

        private void CheckSwipe()
        {
            if (Direction != SwipeDirection.None && !skipFrame)
                Direction = SwipeDirection.None;
            else
                skipFrame = true;
        }

        public override void OnUpdate()
        {
           
        }

        public override void OnFixedUpdate()
        {
            CheckSwipe();
        }
    }
}
