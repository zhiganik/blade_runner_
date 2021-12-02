// GENERATED AUTOMATICALLY FROM 'Assets/ThirdPartyPackages/CustomCharacterController/CharacterControl/CharacterControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterControl"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""7f0c5017-0137-4b9e-ad36-752257dc32d3"",
            ""actions"": [
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""468ee088-d41b-4334-bfa3-c3f3e5031d8c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""cb2196cd-3253-4381-963c-629e191c13d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crunch"",
                    ""type"": ""Button"",
                    ""id"": ""d5927cb8-a490-43b0-b0a4-d3598259b85b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwipePrimary"",
                    ""type"": ""PassThrough"",
                    ""id"": ""300f7efb-ab70-428f-829a-37f507cd9b16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SwipePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2b05dfad-769d-44ce-99f7-42106673cd85"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f37da04-e317-4fe7-b63a-11642a20024c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=45,y=45)"",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""448c06cf-8072-4eae-ae32-641d0ad39e3a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14bf385a-f0b1-4f9d-ad48-f2297741ccce"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crunch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46218a8c-3480-4ff8-990f-36d393da65a2"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwipePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b22708f-0c0f-4bd6-81d8-628dd573b04d"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwipePrimary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_MouseLook = m_Character.FindAction("MouseLook", throwIfNotFound: true);
        m_Character_Jumping = m_Character.FindAction("Jumping", throwIfNotFound: true);
        m_Character_Crunch = m_Character.FindAction("Crunch", throwIfNotFound: true);
        m_Character_SwipePrimary = m_Character.FindAction("SwipePrimary", throwIfNotFound: true);
        m_Character_SwipePosition = m_Character.FindAction("SwipePosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_MouseLook;
    private readonly InputAction m_Character_Jumping;
    private readonly InputAction m_Character_Crunch;
    private readonly InputAction m_Character_SwipePrimary;
    private readonly InputAction m_Character_SwipePosition;
    public struct CharacterActions
    {
        private @CharacterControl m_Wrapper;
        public CharacterActions(@CharacterControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_Character_MouseLook;
        public InputAction @Jumping => m_Wrapper.m_Character_Jumping;
        public InputAction @Crunch => m_Wrapper.m_Character_Crunch;
        public InputAction @SwipePrimary => m_Wrapper.m_Character_SwipePrimary;
        public InputAction @SwipePosition => m_Wrapper.m_Character_SwipePosition;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouseLook;
                @Jumping.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJumping;
                @Jumping.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJumping;
                @Jumping.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJumping;
                @Crunch.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrunch;
                @Crunch.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrunch;
                @Crunch.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnCrunch;
                @SwipePrimary.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePrimary;
                @SwipePrimary.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePrimary;
                @SwipePrimary.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePrimary;
                @SwipePosition.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePosition;
                @SwipePosition.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePosition;
                @SwipePosition.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSwipePosition;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Jumping.started += instance.OnJumping;
                @Jumping.performed += instance.OnJumping;
                @Jumping.canceled += instance.OnJumping;
                @Crunch.started += instance.OnCrunch;
                @Crunch.performed += instance.OnCrunch;
                @Crunch.canceled += instance.OnCrunch;
                @SwipePrimary.started += instance.OnSwipePrimary;
                @SwipePrimary.performed += instance.OnSwipePrimary;
                @SwipePrimary.canceled += instance.OnSwipePrimary;
                @SwipePosition.started += instance.OnSwipePosition;
                @SwipePosition.performed += instance.OnSwipePosition;
                @SwipePosition.canceled += instance.OnSwipePosition;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);
    public interface ICharacterActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
        void OnJumping(InputAction.CallbackContext context);
        void OnCrunch(InputAction.CallbackContext context);
        void OnSwipePrimary(InputAction.CallbackContext context);
        void OnSwipePosition(InputAction.CallbackContext context);
    }
}
