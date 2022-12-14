//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input System/Customizable/CustomizableControls.inputactions
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

namespace WotN.InputSystem
{
    public partial class @CustomizableControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @CustomizableControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomizableControls"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""dbdd27fc-8563-4697-8b4a-0479901edf62"",
            ""actions"": [
                {
                    ""name"": ""RotateCameraToRight"",
                    ""type"": ""Button"",
                    ""id"": ""0950f91e-a717-4ba6-b0e4-b49e33e2de92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateCameraToLeft"",
                    ""type"": ""Button"",
                    ""id"": ""6465f1fa-1836-4f32-9edd-f43f3a497c8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7c2cdda4-b7ab-4517-93af-e64edf22ee6a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0450a053-ecea-4fa4-8ca3-098240bef006"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd46edd9-86a7-43d6-9862-c873f21373ae"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a45c62e8-0ec3-4e2c-9c67-0ddf028f74e4"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""HUD"",
            ""id"": ""f60d480c-03b8-4d5d-80f6-9fe2c5e8c6c0"",
            ""actions"": [
                {
                    ""name"": ""OpenCloseInventory"",
                    ""type"": ""Button"",
                    ""id"": ""cb9bbd51-39f8-485b-b387-9ed26fb853dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseEquipment"",
                    ""type"": ""Button"",
                    ""id"": ""90f2e322-4d48-4396-b447-575dd0067fbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseQuestLog"",
                    ""type"": ""Button"",
                    ""id"": ""6f9cde93-4d17-4153-8cef-bf0e455b1644"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseSkills"",
                    ""type"": ""Button"",
                    ""id"": ""e99ecaf7-e073-45a8-9b3c-dab086110313"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenCloseStats"",
                    ""type"": ""Button"",
                    ""id"": ""762aa9c8-eeff-42a1-a76c-60e8b36a2e9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f00ec765-f14e-44f1-9208-adfb04ac3c66"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d136e6e5-637f-487c-8eeb-648e2c71bb5c"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4955ef52-ea66-4aed-a521-8d4679270d67"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac7f55e8-73e5-4ba0-9695-c4c7a4d596db"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fc35b8e-b0e4-4372-9cea-ce9cfb037ca6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseQuestLog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2507301f-82d7-41d8-8462-177765e3d93e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseQuestLog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59da02fa-a7ee-4088-9fee-f7072369e4b3"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseSkills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dc296a2-c119-42be-a1c5-3a6ead3e6686"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseSkills"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d9426c6-c1f0-4d25-b7df-f79c62883561"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseStats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a136729-9480-4205-ac82-97cf08056d55"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseStats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Camera
            m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
            m_Camera_RotateCameraToRight = m_Camera.FindAction("RotateCameraToRight", throwIfNotFound: true);
            m_Camera_RotateCameraToLeft = m_Camera.FindAction("RotateCameraToLeft", throwIfNotFound: true);
            // HUD
            m_HUD = asset.FindActionMap("HUD", throwIfNotFound: true);
            m_HUD_OpenCloseInventory = m_HUD.FindAction("OpenCloseInventory", throwIfNotFound: true);
            m_HUD_OpenCloseEquipment = m_HUD.FindAction("OpenCloseEquipment", throwIfNotFound: true);
            m_HUD_OpenCloseQuestLog = m_HUD.FindAction("OpenCloseQuestLog", throwIfNotFound: true);
            m_HUD_OpenCloseSkills = m_HUD.FindAction("OpenCloseSkills", throwIfNotFound: true);
            m_HUD_OpenCloseStats = m_HUD.FindAction("OpenCloseStats", throwIfNotFound: true);
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

        // Camera
        private readonly InputActionMap m_Camera;
        private ICameraActions m_CameraActionsCallbackInterface;
        private readonly InputAction m_Camera_RotateCameraToRight;
        private readonly InputAction m_Camera_RotateCameraToLeft;
        public struct CameraActions
        {
            private @CustomizableControls m_Wrapper;
            public CameraActions(@CustomizableControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @RotateCameraToRight => m_Wrapper.m_Camera_RotateCameraToRight;
            public InputAction @RotateCameraToLeft => m_Wrapper.m_Camera_RotateCameraToLeft;
            public InputActionMap Get() { return m_Wrapper.m_Camera; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
            public void SetCallbacks(ICameraActions instance)
            {
                if (m_Wrapper.m_CameraActionsCallbackInterface != null)
                {
                    @RotateCameraToRight.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToRight.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToRight.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToLeft.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToLeft;
                    @RotateCameraToLeft.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToLeft;
                    @RotateCameraToLeft.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCameraToLeft;
                }
                m_Wrapper.m_CameraActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RotateCameraToRight.started += instance.OnRotateCameraToRight;
                    @RotateCameraToRight.performed += instance.OnRotateCameraToRight;
                    @RotateCameraToRight.canceled += instance.OnRotateCameraToRight;
                    @RotateCameraToLeft.started += instance.OnRotateCameraToLeft;
                    @RotateCameraToLeft.performed += instance.OnRotateCameraToLeft;
                    @RotateCameraToLeft.canceled += instance.OnRotateCameraToLeft;
                }
            }
        }
        public CameraActions @Camera => new CameraActions(this);

        // HUD
        private readonly InputActionMap m_HUD;
        private IHUDActions m_HUDActionsCallbackInterface;
        private readonly InputAction m_HUD_OpenCloseInventory;
        private readonly InputAction m_HUD_OpenCloseEquipment;
        private readonly InputAction m_HUD_OpenCloseQuestLog;
        private readonly InputAction m_HUD_OpenCloseSkills;
        private readonly InputAction m_HUD_OpenCloseStats;
        public struct HUDActions
        {
            private @CustomizableControls m_Wrapper;
            public HUDActions(@CustomizableControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @OpenCloseInventory => m_Wrapper.m_HUD_OpenCloseInventory;
            public InputAction @OpenCloseEquipment => m_Wrapper.m_HUD_OpenCloseEquipment;
            public InputAction @OpenCloseQuestLog => m_Wrapper.m_HUD_OpenCloseQuestLog;
            public InputAction @OpenCloseSkills => m_Wrapper.m_HUD_OpenCloseSkills;
            public InputAction @OpenCloseStats => m_Wrapper.m_HUD_OpenCloseStats;
            public InputActionMap Get() { return m_Wrapper.m_HUD; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HUDActions set) { return set.Get(); }
            public void SetCallbacks(IHUDActions instance)
            {
                if (m_Wrapper.m_HUDActionsCallbackInterface != null)
                {
                    @OpenCloseInventory.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseInventory;
                    @OpenCloseInventory.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseInventory;
                    @OpenCloseInventory.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseInventory;
                    @OpenCloseEquipment.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseEquipment;
                    @OpenCloseEquipment.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseEquipment;
                    @OpenCloseEquipment.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseEquipment;
                    @OpenCloseQuestLog.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseQuestLog;
                    @OpenCloseQuestLog.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseQuestLog;
                    @OpenCloseQuestLog.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseQuestLog;
                    @OpenCloseSkills.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseSkills;
                    @OpenCloseSkills.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseSkills;
                    @OpenCloseSkills.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseSkills;
                    @OpenCloseStats.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseStats;
                    @OpenCloseStats.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseStats;
                    @OpenCloseStats.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnOpenCloseStats;
                }
                m_Wrapper.m_HUDActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @OpenCloseInventory.started += instance.OnOpenCloseInventory;
                    @OpenCloseInventory.performed += instance.OnOpenCloseInventory;
                    @OpenCloseInventory.canceled += instance.OnOpenCloseInventory;
                    @OpenCloseEquipment.started += instance.OnOpenCloseEquipment;
                    @OpenCloseEquipment.performed += instance.OnOpenCloseEquipment;
                    @OpenCloseEquipment.canceled += instance.OnOpenCloseEquipment;
                    @OpenCloseQuestLog.started += instance.OnOpenCloseQuestLog;
                    @OpenCloseQuestLog.performed += instance.OnOpenCloseQuestLog;
                    @OpenCloseQuestLog.canceled += instance.OnOpenCloseQuestLog;
                    @OpenCloseSkills.started += instance.OnOpenCloseSkills;
                    @OpenCloseSkills.performed += instance.OnOpenCloseSkills;
                    @OpenCloseSkills.canceled += instance.OnOpenCloseSkills;
                    @OpenCloseStats.started += instance.OnOpenCloseStats;
                    @OpenCloseStats.performed += instance.OnOpenCloseStats;
                    @OpenCloseStats.canceled += instance.OnOpenCloseStats;
                }
            }
        }
        public HUDActions @HUD => new HUDActions(this);
        public interface ICameraActions
        {
            void OnRotateCameraToRight(InputAction.CallbackContext context);
            void OnRotateCameraToLeft(InputAction.CallbackContext context);
        }
        public interface IHUDActions
        {
            void OnOpenCloseInventory(InputAction.CallbackContext context);
            void OnOpenCloseEquipment(InputAction.CallbackContext context);
            void OnOpenCloseQuestLog(InputAction.CallbackContext context);
            void OnOpenCloseSkills(InputAction.CallbackContext context);
            void OnOpenCloseStats(InputAction.CallbackContext context);
        }
    }
}
