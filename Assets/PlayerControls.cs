// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Normal"",
            ""id"": ""0713454e-80d7-4673-bfb8-db48be2c7433"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""287c84ab-4e3f-499a-bebd-6d303b577fe6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""7ed4e3d8-3d46-4308-8884-1879eacf8ed3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseAim"",
                    ""type"": ""Value"",
                    ""id"": ""a13e9387-cb7a-490a-ad14-9ac98352c281"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""edef22ff-8888-437e-b4fa-ec532d9030f4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""4808a757-605b-4ac1-af0d-f8468bda3696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light"",
                    ""type"": ""Button"",
                    ""id"": ""2dcacc8c-7760-4276-becf-36f5e1fd8ff8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KeyAxis"",
                    ""type"": ""Button"",
                    ""id"": ""90c80a16-52d7-4ea3-b322-59eea012eed0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""94820f81-c10d-4d1d-b041-89fd54d72861"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""24d56c0d-641c-4e6d-9c3e-6f7fd23f8fb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""62952c9d-1297-41c3-b738-c150ab865fc4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8c700701-e33c-4471-a247-93498f4b6ccd"",
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
                    ""id"": ""bc9299b2-7f36-4af9-b5d3-3b569af38e5d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""76854e78-e9b3-4fe8-8491-4ffb29f5ccf8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e762bb71-7901-4832-87cb-31c90bb3a2ef"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""53184b49-15eb-4e99-add6-8e135cf59dbe"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fa70b49e-87e5-49c7-974a-47b33c59490b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3dac63b4-6e32-4cf9-994f-a714a82dba7d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f3f386c1-ca63-4c89-9419-9388c0e53752"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""59e669d6-0174-4c1e-be13-84b40eba1fe6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bcdb5e38-75d2-4ba6-8f4f-0f0ef9645653"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ef631e62-f07e-472e-9c25-af3b8afd872c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a306fa21-a4a4-400f-a95d-9f77dab799e4"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a77abc9-8ece-4c7c-a04a-61b8ea85b342"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0347b95-4b6c-4be1-bdb2-c0c0c3baa231"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02fe859a-7004-427d-8348-fbdd2e2638e1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bb8ef70-d466-4caa-9358-1c11cd1fab2d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""MouseAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48dc29d7-e555-4083-808e-e40b1419dea4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""339d2ca4-e95f-41ff-961d-fb6209f18df3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Light"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ecf07333-65d0-469d-a497-a108a6604e17"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a5864987-328d-45f8-9b3a-314f41f090cb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e28d736a-dae1-4f03-affe-2164b276a7e5"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8cc6eb07-4dce-4d7e-a3fd-4f2fad47dd48"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7699cc21-f7d5-4aea-8a2f-da50e16fb440"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d3e23e86-11c0-445b-86ab-df2add3763b4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f3f24663-2350-4d1c-88fa-9eb8c402c2b4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""aa5d345b-3c33-402e-909a-5597e770bd9a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a91cd728-07ae-426a-8808-e08cc637c1bc"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""76c339b2-0b72-4efb-a4ee-2a826c31263f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1619fd14-5638-4004-9f6d-5949fdc5f4a5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""10266672-79b8-41d9-9d83-fbdffe3947c8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""00144bbe-53fe-4e18-8e54-be942dd70b4f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""135efa10-8363-4e22-90eb-f89b2c5efbac"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aafea083-e968-4dd7-a5eb-2687dcbe8dc8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""144216a1-f885-4203-88d2-781e8196b834"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""669d9ebc-9e8b-4809-bf45-81995da87103"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Normal Scheme"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Normal Scheme"",
            ""bindingGroup"": ""Normal Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
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
        }
    ]
}");
        // Normal
        m_Normal = asset.FindActionMap("Normal", throwIfNotFound: true);
        m_Normal_Move = m_Normal.FindAction("Move", throwIfNotFound: true);
        m_Normal_Aim = m_Normal.FindAction("Aim", throwIfNotFound: true);
        m_Normal_MouseAim = m_Normal.FindAction("MouseAim", throwIfNotFound: true);
        m_Normal_Run = m_Normal.FindAction("Run", throwIfNotFound: true);
        m_Normal_Interaction = m_Normal.FindAction("Interaction", throwIfNotFound: true);
        m_Normal_Light = m_Normal.FindAction("Light", throwIfNotFound: true);
        m_Normal_KeyAxis = m_Normal.FindAction("KeyAxis", throwIfNotFound: true);
        m_Normal_Cancel = m_Normal.FindAction("Cancel", throwIfNotFound: true);
        m_Normal_Pause = m_Normal.FindAction("Pause", throwIfNotFound: true);
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

    // Normal
    private readonly InputActionMap m_Normal;
    private INormalActions m_NormalActionsCallbackInterface;
    private readonly InputAction m_Normal_Move;
    private readonly InputAction m_Normal_Aim;
    private readonly InputAction m_Normal_MouseAim;
    private readonly InputAction m_Normal_Run;
    private readonly InputAction m_Normal_Interaction;
    private readonly InputAction m_Normal_Light;
    private readonly InputAction m_Normal_KeyAxis;
    private readonly InputAction m_Normal_Cancel;
    private readonly InputAction m_Normal_Pause;
    public struct NormalActions
    {
        private @PlayerControls m_Wrapper;
        public NormalActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Normal_Move;
        public InputAction @Aim => m_Wrapper.m_Normal_Aim;
        public InputAction @MouseAim => m_Wrapper.m_Normal_MouseAim;
        public InputAction @Run => m_Wrapper.m_Normal_Run;
        public InputAction @Interaction => m_Wrapper.m_Normal_Interaction;
        public InputAction @Light => m_Wrapper.m_Normal_Light;
        public InputAction @KeyAxis => m_Wrapper.m_Normal_KeyAxis;
        public InputAction @Cancel => m_Wrapper.m_Normal_Cancel;
        public InputAction @Pause => m_Wrapper.m_Normal_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NormalActions set) { return set.Get(); }
        public void SetCallbacks(INormalActions instance)
        {
            if (m_Wrapper.m_NormalActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnMove;
                @Aim.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnAim;
                @MouseAim.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnMouseAim;
                @MouseAim.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnMouseAim;
                @MouseAim.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnMouseAim;
                @Run.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnRun;
                @Interaction.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnInteraction;
                @Light.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnLight;
                @Light.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnLight;
                @Light.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnLight;
                @KeyAxis.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnKeyAxis;
                @KeyAxis.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnKeyAxis;
                @KeyAxis.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnKeyAxis;
                @Cancel.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnCancel;
                @Pause.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @MouseAim.started += instance.OnMouseAim;
                @MouseAim.performed += instance.OnMouseAim;
                @MouseAim.canceled += instance.OnMouseAim;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @Light.started += instance.OnLight;
                @Light.performed += instance.OnLight;
                @Light.canceled += instance.OnLight;
                @KeyAxis.started += instance.OnKeyAxis;
                @KeyAxis.performed += instance.OnKeyAxis;
                @KeyAxis.canceled += instance.OnKeyAxis;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public NormalActions @Normal => new NormalActions(this);
    private int m_NormalSchemeSchemeIndex = -1;
    public InputControlScheme NormalSchemeScheme
    {
        get
        {
            if (m_NormalSchemeSchemeIndex == -1) m_NormalSchemeSchemeIndex = asset.FindControlSchemeIndex("Normal Scheme");
            return asset.controlSchemes[m_NormalSchemeSchemeIndex];
        }
    }
    public interface INormalActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnMouseAim(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnLight(InputAction.CallbackContext context);
        void OnKeyAxis(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
