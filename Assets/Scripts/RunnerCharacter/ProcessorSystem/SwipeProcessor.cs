using System;
using Assets.RunnerCharacter.SubComponentSystem.SubData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.RunnerCharacter.ProcessorSystem
{
    public class SwipeProcessor : MonoBehaviour
    {
        private Runner runner;
        private CharacterControl control;
        private InputAction primary;
        private InputAction position;

        private SwipeSubProcessor[] processors;
        
        private Swipe recentSwipe;

        public event Action<SwipeDirection> OnSwipeCompleted;

        private void OnEnable()
        {
            control?.Enable();
        }

        private void OnDisable()
        {
            control?.Disable();
        }

        private void Awake()
        {
            runner = transform.GetComponentInParent<Runner>();
            GetSubProcessors();
        }

        private void Start()
        {
            GetInputs();
        }

        private void GetSubProcessors()
        {
            processors = transform.GetComponentsInChildren<SwipeSubProcessor>();
            foreach (var processor in processors)
            {
                processor.Runner = runner;
                OnSwipeCompleted += swipe => processor.SwipeProcess?.Invoke(swipe);
            }
        }

        private void GetInputs()
        {
            control = new CharacterControl();
            control.Enable();
            primary =  control.Character.SwipePrimary;
            position = control.Character.SwipePosition;
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
            
        }

        private void EndSwipe(InputAction.CallbackContext context)
        {
            if(recentSwipe == null) return;
            recentSwipe.EndPosition = position.ReadValue<Vector2>();
            OnSwipeCompleted?.Invoke(Utils.CalculateDirection(recentSwipe.StartPosition, recentSwipe.EndPosition));
        }
    }
}