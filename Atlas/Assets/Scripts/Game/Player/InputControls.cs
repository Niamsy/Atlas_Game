// GENERATED AUTOMATICALLY FROM 'Assets/Player/Inputs/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputControls : IInputActionCollection
{
    private InputActionAsset asset;
    public InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4175ac56-6435-41f2-9803-1ecbaac72e7e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""fe7457d8-d5e6-4337-b627-d4cc530250c2"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Jump"",
                    ""id"": ""d8aa1f5b-f498-491b-b704-2cb991054936"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""Interact"",
                    ""id"": ""e18c04b3-2bbd-4ccf-9c12-a32fa767e0e9"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""UseItem"",
                    ""id"": ""ee93d372-a58f-4478-9cde-734215d6b83b"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Inventory"",
                    ""id"": ""8190cf5f-9d3f-4c28-9036-db1fcd6bbbfb"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""Menu"",
                    ""id"": ""4408c49f-9cff-420c-b70f-a373b4239fbb"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraMovement"",
                    ""id"": ""9f0098dd-027f-428c-8579-77e3c214f1b7"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraZoom"",
                    ""id"": ""2c5bf3db-8da8-48b9-af03-6a685369120f"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""CameraClick"",
                    ""id"": ""971fdad3-e326-4f38-b07b-e5f6ace2acde"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""RecenterCamera"",
                    ""id"": ""03f5ec0c-509d-4a33-a432-32adc7f770b3"",
                    ""expectedControlLayout"": ""Button"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""20b1740b-f361-4938-94e6-9bbd2619785d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c5711fcf-6c9b-4006-817b-963f5218593d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2f79f45e-6529-41bd-8064-fb4f29071be6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""699ded0a-77bd-470c-85f5-63480e4b04f4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2e6c2e16-eabc-403b-b015-ed7f721d50d5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""0b34745f-1b66-424c-844f-ce96f0976135"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9c55a20d-9bac-4156-b3d1-0d3fd40f19f5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""72249d26-3ad7-4d54-bcee-c2f11bdc1530"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2acf444b-7da5-4388-9509-a1bfa6647d9c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4f1ab475-99ac-457a-888f-4930c38c24a4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""LeftJoystick"",
                    ""id"": ""3df2eb7a-06ba-4095-b16b-5f762b161789"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2ec0beb0-8414-45eb-97e8-0e2eb79645f3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cae2d993-2c96-449f-ab2a-d4561bf6746a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c3aef07f-d2c5-41c3-af73-27a69af6bec4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""47889473-8dcd-433f-8f3d-8f5e83a33725"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""4bbf752b-0c8c-4971-8d06-2d9c2f7ccfd2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""83a3131d-1afb-4624-8489-33a1c999daee"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""8e204c73-0255-4f21-90bc-b687e38bec42"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""a0100294-fc0d-4443-8f9d-ce4182195d29"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""38cb5265-5b8d-4170-9e3e-2d833c1cd5ec"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""77b2cf4b-1a22-4eab-b385-4e1f1ca937a5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""492afabc-c8c7-40c9-9386-1a850133c147"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""bfb0d1a1-5942-46bd-a60a-639bf52b0fd0"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2,ScaleVector2(y=0.1)"",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""d1cea641-cc99-4e19-835a-49637a59ddeb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""0227c4a9-d030-499f-82e5-b66c93a2eca5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""cac0f325-2392-4088-833f-9b044eb9bcc1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""CameraClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""2814be8c-34fa-4a7d-82ea-33bf5b5c0407"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""RecenterCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""1e03e243-9f58-4a58-b22c-2eba49ec5b5b"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""RecenterCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""3fe6cab6-472b-429e-95eb-7cfdb636febd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";KeyboardAndMouse"",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""c4cc8991-6c0b-4a36-b1f7-94d6d9edf3f9"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""basedOn"": """",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Jump = m_Player.GetAction("Jump");
        m_Player_Interact = m_Player.GetAction("Interact");
        m_Player_UseItem = m_Player.GetAction("UseItem");
        m_Player_Inventory = m_Player.GetAction("Inventory");
        m_Player_Menu = m_Player.GetAction("Menu");
        m_Player_CameraMovement = m_Player.GetAction("CameraMovement");
        m_Player_CameraZoom = m_Player.GetAction("CameraZoom");
        m_Player_CameraClick = m_Player.GetAction("CameraClick");
        m_Player_RecenterCamera = m_Player.GetAction("RecenterCamera");
    }

    ~InputControls()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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

    // Player
    private InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private InputAction m_Player_Movement;
    private InputAction m_Player_Jump;
    private InputAction m_Player_Interact;
    private InputAction m_Player_UseItem;
    private InputAction m_Player_Inventory;
    private InputAction m_Player_Menu;
    private InputAction m_Player_CameraMovement;
    private InputAction m_Player_CameraZoom;
    private InputAction m_Player_CameraClick;
    private InputAction m_Player_RecenterCamera;
    public struct PlayerActions
    {
        private InputControls m_Wrapper;
        public PlayerActions(InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Player_Movement; } }
        public InputAction @Jump { get { return m_Wrapper.m_Player_Jump; } }
        public InputAction @Interact { get { return m_Wrapper.m_Player_Interact; } }
        public InputAction @UseItem { get { return m_Wrapper.m_Player_UseItem; } }
        public InputAction @Inventory { get { return m_Wrapper.m_Player_Inventory; } }
        public InputAction @Menu { get { return m_Wrapper.m_Player_Menu; } }
        public InputAction @CameraMovement { get { return m_Wrapper.m_Player_CameraMovement; } }
        public InputAction @CameraZoom { get { return m_Wrapper.m_Player_CameraZoom; } }
        public InputAction @CameraClick { get { return m_Wrapper.m_Player_CameraClick; } }
        public InputAction @RecenterCamera { get { return m_Wrapper.m_Player_RecenterCamera; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                UseItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
                UseItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
                UseItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem;
                Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                CameraMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                CameraMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                CameraMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                CameraZoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                CameraZoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                CameraZoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraZoom;
                CameraClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraClick;
                CameraClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraClick;
                CameraClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraClick;
                RecenterCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecenterCamera;
                RecenterCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecenterCamera;
                RecenterCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRecenterCamera;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Interact.started += instance.OnInteract;
                Interact.performed += instance.OnInteract;
                Interact.canceled += instance.OnInteract;
                UseItem.started += instance.OnUseItem;
                UseItem.performed += instance.OnUseItem;
                UseItem.canceled += instance.OnUseItem;
                Inventory.started += instance.OnInventory;
                Inventory.performed += instance.OnInventory;
                Inventory.canceled += instance.OnInventory;
                Menu.started += instance.OnMenu;
                Menu.performed += instance.OnMenu;
                Menu.canceled += instance.OnMenu;
                CameraMovement.started += instance.OnCameraMovement;
                CameraMovement.performed += instance.OnCameraMovement;
                CameraMovement.canceled += instance.OnCameraMovement;
                CameraZoom.started += instance.OnCameraZoom;
                CameraZoom.performed += instance.OnCameraZoom;
                CameraZoom.canceled += instance.OnCameraZoom;
                CameraClick.started += instance.OnCameraClick;
                CameraClick.performed += instance.OnCameraClick;
                CameraClick.canceled += instance.OnCameraClick;
                RecenterCamera.started += instance.OnRecenterCamera;
                RecenterCamera.performed += instance.OnRecenterCamera;
                RecenterCamera.canceled += instance.OnRecenterCamera;
            }
        }
    }
    public PlayerActions @Player
    {
        get
        {
            return new PlayerActions(this);
        }
    }
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.GetControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnCameraClick(InputAction.CallbackContext context);
        void OnRecenterCamera(InputAction.CallbackContext context);
    }
}
