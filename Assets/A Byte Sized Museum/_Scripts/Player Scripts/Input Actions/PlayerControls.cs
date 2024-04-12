//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/A Byte Sized Museum/_Scripts/Player Scripts/Input Actions/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5aa7561a-b056-45c2-b020-0bd9b6f3ca53"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0141499b-518a-42b9-ac46-557d50fa223b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""ba5e5116-fd5a-493f-9910-d7fe389e9256"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""559baa83-e1e0-40de-8477-1ec265356bd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e9cdeab8-77d9-4e29-bd59-58eedb5f3353"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""2d991160-2b39-4b00-a746-de61cd67a7ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Button"",
                    ""id"": ""2f0da9bc-5d5d-4c4f-af2c-967e963cb1e1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""4acf37ea-1608-4bae-873d-628d981b96ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""98701b24-b583-491d-b7c2-b6c1dcf7eed7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d59f0a0c-e42e-4d22-8efc-dc1a31a05632"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""42cb950b-647c-4a54-9806-8187d79bba4b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fe96a980-31ef-426b-bbe0-02b1417d733e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e3ecf6ee-cafc-4517-8cac-b093b97eb9e7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""89bd9c42-753a-4bbf-813c-9c3b338dcfc3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d78fdc0f-18f3-4f97-8c3c-7c245b7f1ae0"",
                    ""path"": ""<Touchscreen>/touch1/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""035fadab-e674-4ed7-919c-8227617b4fc2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b7e1cde-7e30-435a-908a-6ef49be68a7f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1673d067-e758-4a9f-ab79-886c361db190"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Hold,Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa78d010-f6e2-48b2-acdc-75a0ef4fb85c"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b4ed98a-3382-4229-b8a7-2847245cdbf3"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02a8d56b-9ad7-458d-b7f6-8f21b492571a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cheats"",
            ""id"": ""303d71d0-80a7-4c2e-9f11-a338934f640d"",
            ""actions"": [
                {
                    ""name"": ""ToggleCheats"",
                    ""type"": ""Button"",
                    ""id"": ""2b2f1b2f-af58-4011-89b3-f7d08a0871f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GiveFragment"",
                    ""type"": ""Button"",
                    ""id"": ""c9aa0167-72e3-43e2-bcc2-dd1bd0a79742"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SubtractTime"",
                    ""type"": ""Button"",
                    ""id"": ""d7a60495-c30d-4f54-aecd-497f42b270bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddTime"",
                    ""type"": ""Button"",
                    ""id"": ""7ef37b7a-cac4-443b-b4a7-dc88fac2a5db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShowDebugOverlay"",
                    ""type"": ""Button"",
                    ""id"": ""8fc9d4b3-12db-496e-935e-ece71b2301c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleUI"",
                    ""type"": ""Button"",
                    ""id"": ""641a1930-d3d8-4b8c-9e34-9eb4c66a1cc5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Two Modifiers"",
                    ""id"": ""7d46facb-647c-4e3b-8b5c-c2ae720df65d"",
                    ""path"": ""TwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCheats"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier1"",
                    ""id"": ""82a708d3-231b-48ad-9030-db7a0b997b5a"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCheats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier2"",
                    ""id"": ""c59c3f6a-b38c-419b-af09-b57c2c6af641"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCheats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""588ad3b1-8c78-4541-8082-fc513ef772c1"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCheats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""67ed274c-3f06-424e-a294-fa0c44858712"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GiveFragment"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4e3d5e4f-225c-4117-b1f2-5e69220b7bbc"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GiveFragment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""3d7e4cd2-8d67-4f7a-8412-def0d9d0423b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GiveFragment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""075a3242-282f-4ef0-8e93-b369445e5f63"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SubtractTime"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""508fce84-c22f-435c-b5b7-728567a1d2ca"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SubtractTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""fe4a41db-0b82-4cc1-8bda-d3c076c5f05e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SubtractTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""ef365772-fd6c-4a73-a905-22879e114922"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddTime"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""9392e078-3227-40a4-941d-70141b3d6dc1"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""8e78e07e-27f1-40e1-8c17-c0dfc9b7fc73"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""bc1d23bb-1d5e-4653-90e0-53f760d206a9"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowDebugOverlay"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c903f58c-4ae8-498c-9451-17e83fdad5d1"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowDebugOverlay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""5fd840f8-463c-494f-bb9f-69cae76e33b3"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowDebugOverlay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""f47cbe32-62fb-4725-91e0-f96323903146"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4a87fa6b-0daf-4577-a819-5f1e92e71e4a"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""7a4d8e2a-46d2-4778-8bd1-168e85193601"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Sneak = m_Player.FindAction("Sneak", throwIfNotFound: true);
        m_Player_Escape = m_Player.FindAction("Escape", throwIfNotFound: true);
        // Cheats
        m_Cheats = asset.FindActionMap("Cheats", throwIfNotFound: true);
        m_Cheats_ToggleCheats = m_Cheats.FindAction("ToggleCheats", throwIfNotFound: true);
        m_Cheats_GiveFragment = m_Cheats.FindAction("GiveFragment", throwIfNotFound: true);
        m_Cheats_SubtractTime = m_Cheats.FindAction("SubtractTime", throwIfNotFound: true);
        m_Cheats_AddTime = m_Cheats.FindAction("AddTime", throwIfNotFound: true);
        m_Cheats_ShowDebugOverlay = m_Cheats.FindAction("ShowDebugOverlay", throwIfNotFound: true);
        m_Cheats_ToggleUI = m_Cheats.FindAction("ToggleUI", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Sneak;
    private readonly InputAction m_Player_Escape;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Sneak => m_Wrapper.m_Player_Sneak;
        public InputAction @Escape => m_Wrapper.m_Player_Escape;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Sneak.started += instance.OnSneak;
            @Sneak.performed += instance.OnSneak;
            @Sneak.canceled += instance.OnSneak;
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Sneak.started -= instance.OnSneak;
            @Sneak.performed -= instance.OnSneak;
            @Sneak.canceled -= instance.OnSneak;
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Cheats
    private readonly InputActionMap m_Cheats;
    private List<ICheatsActions> m_CheatsActionsCallbackInterfaces = new List<ICheatsActions>();
    private readonly InputAction m_Cheats_ToggleCheats;
    private readonly InputAction m_Cheats_GiveFragment;
    private readonly InputAction m_Cheats_SubtractTime;
    private readonly InputAction m_Cheats_AddTime;
    private readonly InputAction m_Cheats_ShowDebugOverlay;
    private readonly InputAction m_Cheats_ToggleUI;
    public struct CheatsActions
    {
        private @PlayerControls m_Wrapper;
        public CheatsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleCheats => m_Wrapper.m_Cheats_ToggleCheats;
        public InputAction @GiveFragment => m_Wrapper.m_Cheats_GiveFragment;
        public InputAction @SubtractTime => m_Wrapper.m_Cheats_SubtractTime;
        public InputAction @AddTime => m_Wrapper.m_Cheats_AddTime;
        public InputAction @ShowDebugOverlay => m_Wrapper.m_Cheats_ShowDebugOverlay;
        public InputAction @ToggleUI => m_Wrapper.m_Cheats_ToggleUI;
        public InputActionMap Get() { return m_Wrapper.m_Cheats; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatsActions set) { return set.Get(); }
        public void AddCallbacks(ICheatsActions instance)
        {
            if (instance == null || m_Wrapper.m_CheatsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CheatsActionsCallbackInterfaces.Add(instance);
            @ToggleCheats.started += instance.OnToggleCheats;
            @ToggleCheats.performed += instance.OnToggleCheats;
            @ToggleCheats.canceled += instance.OnToggleCheats;
            @GiveFragment.started += instance.OnGiveFragment;
            @GiveFragment.performed += instance.OnGiveFragment;
            @GiveFragment.canceled += instance.OnGiveFragment;
            @SubtractTime.started += instance.OnSubtractTime;
            @SubtractTime.performed += instance.OnSubtractTime;
            @SubtractTime.canceled += instance.OnSubtractTime;
            @AddTime.started += instance.OnAddTime;
            @AddTime.performed += instance.OnAddTime;
            @AddTime.canceled += instance.OnAddTime;
            @ShowDebugOverlay.started += instance.OnShowDebugOverlay;
            @ShowDebugOverlay.performed += instance.OnShowDebugOverlay;
            @ShowDebugOverlay.canceled += instance.OnShowDebugOverlay;
            @ToggleUI.started += instance.OnToggleUI;
            @ToggleUI.performed += instance.OnToggleUI;
            @ToggleUI.canceled += instance.OnToggleUI;
        }

        private void UnregisterCallbacks(ICheatsActions instance)
        {
            @ToggleCheats.started -= instance.OnToggleCheats;
            @ToggleCheats.performed -= instance.OnToggleCheats;
            @ToggleCheats.canceled -= instance.OnToggleCheats;
            @GiveFragment.started -= instance.OnGiveFragment;
            @GiveFragment.performed -= instance.OnGiveFragment;
            @GiveFragment.canceled -= instance.OnGiveFragment;
            @SubtractTime.started -= instance.OnSubtractTime;
            @SubtractTime.performed -= instance.OnSubtractTime;
            @SubtractTime.canceled -= instance.OnSubtractTime;
            @AddTime.started -= instance.OnAddTime;
            @AddTime.performed -= instance.OnAddTime;
            @AddTime.canceled -= instance.OnAddTime;
            @ShowDebugOverlay.started -= instance.OnShowDebugOverlay;
            @ShowDebugOverlay.performed -= instance.OnShowDebugOverlay;
            @ShowDebugOverlay.canceled -= instance.OnShowDebugOverlay;
            @ToggleUI.started -= instance.OnToggleUI;
            @ToggleUI.performed -= instance.OnToggleUI;
            @ToggleUI.canceled -= instance.OnToggleUI;
        }

        public void RemoveCallbacks(ICheatsActions instance)
        {
            if (m_Wrapper.m_CheatsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICheatsActions instance)
        {
            foreach (var item in m_Wrapper.m_CheatsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CheatsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CheatsActions @Cheats => new CheatsActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
    }
    public interface ICheatsActions
    {
        void OnToggleCheats(InputAction.CallbackContext context);
        void OnGiveFragment(InputAction.CallbackContext context);
        void OnSubtractTime(InputAction.CallbackContext context);
        void OnAddTime(InputAction.CallbackContext context);
        void OnShowDebugOverlay(InputAction.CallbackContext context);
        void OnToggleUI(InputAction.CallbackContext context);
    }
}
