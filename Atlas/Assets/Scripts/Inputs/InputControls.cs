// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputControls : IInputActionCollection, IDisposable
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
                    ""type"": ""Value"",
                    ""id"": ""fe7457d8-d5e6-4337-b627-d4cc530250c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""d8aa1f5b-f498-491b-b704-2cb991054936"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e18c04b3-2bbd-4ccf-9c12-a32fa767e0e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""ee93d372-a58f-4478-9cde-734215d6b83b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""8190cf5f-9d3f-4c28-9036-db1fcd6bbbfb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""4408c49f-9cff-420c-b70f-a373b4239fbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""9f0098dd-027f-428c-8579-77e3c214f1b7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""2c5bf3db-8da8-48b9-af03-6a685369120f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraClick"",
                    ""type"": ""Button"",
                    ""id"": ""971fdad3-e326-4f38-b07b-e5f6ace2acde"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RecenterCamera"",
                    ""type"": ""Button"",
                    ""id"": ""03f5ec0c-509d-4a33-a432-32adc7f770b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 1"",
                    ""type"": ""Button"",
                    ""id"": ""bc110bcf-3531-422d-b8c4-f23c28ef4c77"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 2"",
                    ""type"": ""Button"",
                    ""id"": ""ca1c2456-e390-4945-9884-93f0b17b093a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 3"",
                    ""type"": ""Button"",
                    ""id"": ""7ae55088-ce76-4e2d-acb1-4afb21a25457"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 4"",
                    ""type"": ""Button"",
                    ""id"": ""f1e51a72-c7f8-4261-8ab9-10a0095da2d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 5"",
                    ""type"": ""Button"",
                    ""id"": ""0ae75eb9-f106-4716-97aa-335e61ff7133"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 6"",
                    ""type"": ""Button"",
                    ""id"": ""91624c08-65eb-4f9e-98ab-a014b4048d2d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 7"",
                    ""type"": ""Button"",
                    ""id"": ""342b3124-d7ed-4d4c-b2ca-08d87a288c4b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Select Field 8"",
                    ""type"": ""Button"",
                    ""id"": ""d246dd5a-9818-47c8-8887-8a0a116d73bb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Quest"",
                    ""type"": ""Button"",
                    ""id"": ""8b0180f4-c61d-4e51-9d4c-9c7accfa40ba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": true
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
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
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22242db6-75c0-4b8c-8e0c-4f15a257cd21"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64fd3d39-489f-41a4-8c5c-b92c9802cc13"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Quest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c93925a-d199-4dd2-9aa6-33af11610a6a"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35b4fa06-6e1c-46e2-8c65-ebf9f60b6cac"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3458c6dd-9665-4385-bc24-dda01d191eee"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2711ab4a-95e8-403b-893b-c4c25ab95e77"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af7a0c12-9007-4d9a-be08-3ab8984fbc7e"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28b427e7-a9e6-4b2f-935e-3be88846c2da"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebaeee55-f5c9-4355-84e6-48c2d05e9cd4"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Select Field 7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c324179-ea51-42cc-b7ff-74081ab75a09"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Quest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""11411269-64c8-43a5-8aa1-649f160e00e8"",
            ""actions"": [
                {
                    ""name"": ""SpeedUpTime"",
                    ""type"": ""Button"",
                    ""id"": ""42f5c89b-8fc5-4fc4-8cb8-8e1d63649556"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""673d2f7f-3328-4eeb-87a3-57c8dd1161fe"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;KeyboardAndMouse"",
                    ""action"": ""SpeedUpTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a53aa028-f38b-4ebf-bf00-8bd13046fa21"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad;KeyboardAndMouse"",
                    ""action"": ""SpeedUpTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardAndMouse"",
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
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_UseItem = m_Player.FindAction("UseItem", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
        m_Player_CameraMovement = m_Player.FindAction("CameraMovement", throwIfNotFound: true);
        m_Player_CameraZoom = m_Player.FindAction("CameraZoom", throwIfNotFound: true);
        m_Player_CameraClick = m_Player.FindAction("CameraClick", throwIfNotFound: true);
        m_Player_RecenterCamera = m_Player.FindAction("RecenterCamera", throwIfNotFound: true);
        m_Player_SelectField1 = m_Player.FindAction("Select Field 1", throwIfNotFound: true);
        m_Player_SelectField2 = m_Player.FindAction("Select Field 2", throwIfNotFound: true);
        m_Player_SelectField3 = m_Player.FindAction("Select Field 3", throwIfNotFound: true);
        m_Player_SelectField4 = m_Player.FindAction("Select Field 4", throwIfNotFound: true);
        m_Player_SelectField5 = m_Player.FindAction("Select Field 5", throwIfNotFound: true);
        m_Player_SelectField6 = m_Player.FindAction("Select Field 6", throwIfNotFound: true);
        m_Player_SelectField7 = m_Player.FindAction("Select Field 7", throwIfNotFound: true);
        m_Player_SelectField8 = m_Player.FindAction("Select Field 8", throwIfNotFound: true);
        m_Player_Quest = m_Player.FindAction("Quest", throwIfNotFound: true);
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_SpeedUpTime = m_Debug.FindAction("SpeedUpTime", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_UseItem;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_Menu;
    private readonly InputAction m_Player_CameraMovement;
    private readonly InputAction m_Player_CameraZoom;
    private readonly InputAction m_Player_CameraClick;
    private readonly InputAction m_Player_RecenterCamera;
    private readonly InputAction m_Player_SelectField1;
    private readonly InputAction m_Player_SelectField2;
    private readonly InputAction m_Player_SelectField3;
    private readonly InputAction m_Player_SelectField4;
    private readonly InputAction m_Player_SelectField5;
    private readonly InputAction m_Player_SelectField6;
    private readonly InputAction m_Player_SelectField7;
    private readonly InputAction m_Player_SelectField8;
    private readonly InputAction m_Player_Quest;
    public struct PlayerActions
    {
        private InputControls m_Wrapper;
        public PlayerActions(InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @UseItem => m_Wrapper.m_Player_UseItem;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @Menu => m_Wrapper.m_Player_Menu;
        public InputAction @CameraMovement => m_Wrapper.m_Player_CameraMovement;
        public InputAction @CameraZoom => m_Wrapper.m_Player_CameraZoom;
        public InputAction @CameraClick => m_Wrapper.m_Player_CameraClick;
        public InputAction @RecenterCamera => m_Wrapper.m_Player_RecenterCamera;
        public InputAction @SelectField1 => m_Wrapper.m_Player_SelectField1;
        public InputAction @SelectField2 => m_Wrapper.m_Player_SelectField2;
        public InputAction @SelectField3 => m_Wrapper.m_Player_SelectField3;
        public InputAction @SelectField4 => m_Wrapper.m_Player_SelectField4;
        public InputAction @SelectField5 => m_Wrapper.m_Player_SelectField5;
        public InputAction @SelectField6 => m_Wrapper.m_Player_SelectField6;
        public InputAction @SelectField7 => m_Wrapper.m_Player_SelectField7;
        public InputAction @SelectField8 => m_Wrapper.m_Player_SelectField8;
        public InputAction @Quest => m_Wrapper.m_Player_Quest;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
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
                SelectField1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField1;
                SelectField1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField1;
                SelectField1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField1;
                SelectField2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField2;
                SelectField2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField2;
                SelectField2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField2;
                SelectField3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField3;
                SelectField3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField3;
                SelectField3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField3;
                SelectField4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField4;
                SelectField4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField4;
                SelectField4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField4;
                SelectField5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField5;
                SelectField5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField5;
                SelectField5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField5;
                SelectField6.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField6;
                SelectField6.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField6;
                SelectField6.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField6;
                SelectField7.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField7;
                SelectField7.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField7;
                SelectField7.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField7;
                SelectField8.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField8;
                SelectField8.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField8;
                SelectField8.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectField8;
                Quest.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
                Quest.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
                Quest.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuest;
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
                SelectField1.started += instance.OnSelectField1;
                SelectField1.performed += instance.OnSelectField1;
                SelectField1.canceled += instance.OnSelectField1;
                SelectField2.started += instance.OnSelectField2;
                SelectField2.performed += instance.OnSelectField2;
                SelectField2.canceled += instance.OnSelectField2;
                SelectField3.started += instance.OnSelectField3;
                SelectField3.performed += instance.OnSelectField3;
                SelectField3.canceled += instance.OnSelectField3;
                SelectField4.started += instance.OnSelectField4;
                SelectField4.performed += instance.OnSelectField4;
                SelectField4.canceled += instance.OnSelectField4;
                SelectField5.started += instance.OnSelectField5;
                SelectField5.performed += instance.OnSelectField5;
                SelectField5.canceled += instance.OnSelectField5;
                SelectField6.started += instance.OnSelectField6;
                SelectField6.performed += instance.OnSelectField6;
                SelectField6.canceled += instance.OnSelectField6;
                SelectField7.started += instance.OnSelectField7;
                SelectField7.performed += instance.OnSelectField7;
                SelectField7.canceled += instance.OnSelectField7;
                SelectField8.started += instance.OnSelectField8;
                SelectField8.performed += instance.OnSelectField8;
                SelectField8.canceled += instance.OnSelectField8;
                Quest.started += instance.OnQuest;
                Quest.performed += instance.OnQuest;
                Quest.canceled += instance.OnQuest;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_SpeedUpTime;
    public struct DebugActions
    {
        private InputControls m_Wrapper;
        public DebugActions(InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpeedUpTime => m_Wrapper.m_Debug_SpeedUpTime;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                SpeedUpTime.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUpTime;
                SpeedUpTime.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUpTime;
                SpeedUpTime.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnSpeedUpTime;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                SpeedUpTime.started += instance.OnSpeedUpTime;
                SpeedUpTime.performed += instance.OnSpeedUpTime;
                SpeedUpTime.canceled += instance.OnSpeedUpTime;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
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
        void OnSelectField1(InputAction.CallbackContext context);
        void OnSelectField2(InputAction.CallbackContext context);
        void OnSelectField3(InputAction.CallbackContext context);
        void OnSelectField4(InputAction.CallbackContext context);
        void OnSelectField5(InputAction.CallbackContext context);
        void OnSelectField6(InputAction.CallbackContext context);
        void OnSelectField7(InputAction.CallbackContext context);
        void OnSelectField8(InputAction.CallbackContext context);
        void OnQuest(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnSpeedUpTime(InputAction.CallbackContext context);
    }
}
