using UnityEngine;
using UnityEngine.InputSystem;

namespace BladeRunner
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;

        public bool JumpInput => jumpInput.action.triggered;
        public Vector2 MoveInput => moveInput.action.ReadValue<Vector2>();
    }
}
