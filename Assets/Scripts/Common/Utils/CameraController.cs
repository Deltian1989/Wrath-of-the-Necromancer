using Cinemachine;
using WotN.UI.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using WotN.InputSystem;
using WotN.Common.Managers;

namespace WotN.Common.Utils
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float zoomSpeed = 1f;
        [SerializeField]
        private float minZoom = 5f;
        [SerializeField]
        private float maxZoom = 15f;

        [SerializeField]
        private float rotateSpeed = 100f;

        [SerializeField]
        private float yawSpeed = 100f;

        private bool orbitAroundPlayer;

        private float currentZoom = 10f;

        private float currentYaw = 0f;

        private Quaternion camTurnAngleY;

        private Quaternion camTurnAngleX;

        private CinemachineVirtualCamera virtualCamera;

        private CinemachineOrbitalTransposer virtualOrbitalTransposerCamera;

        private Transform target;

        private DialogueManager dialogueManager;

        private MouseWheelLocker[] mosueWheelLockers;

        private CustomizableControls customizableControls;

        private PlayerInput playerInput;

        void Awake()
        {
            mosueWheelLockers = FindObjectsOfType<MouseWheelLocker>(true);
            dialogueManager = DialogueManager.Instance;
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualOrbitalTransposerCamera = FindObjectOfType<CinemachineOrbitalTransposer>();

            customizableControls = ControlsManager.Instance.GetCustomizableControls();

            customizableControls.Camera.RotateCameraToLeft.started += RotateAroundPlayerToLeft;
            customizableControls.Camera.RotateCameraToLeft.canceled += RotateAroundPlayerToLeft;

            customizableControls.Camera.RotateCameraToRight.started += RotateAroundPlayerToRight;
            customizableControls.Camera.RotateCameraToRight.canceled += RotateAroundPlayerToRight;

            customizableControls.Camera.Enable();

            playerInput = GetComponentInChildren<PlayerInput>();
        }

        void Start()
        {
            target = GameObject.FindGameObjectWithTag(GameManager.PlayerTag).transform;

            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;

            var normalized = virtualOrbitalTransposerCamera.m_FollowOffset.normalized;

            virtualOrbitalTransposerCamera.m_FollowOffset = normalized * currentZoom * zoomSpeed;

            PauseManager.Instance.onPaused += DisableCameraInput;
            PauseManager.Instance.onResumed += EnableCameraInput;
        }

        void LateUpdate()
        {
            var angleY = Quaternion.AngleAxis(currentYaw, Vector3.up);

            virtualOrbitalTransposerCamera.m_FollowOffset = angleY * virtualOrbitalTransposerCamera.m_FollowOffset;

            virtualOrbitalTransposerCamera.m_FollowOffset = camTurnAngleX * virtualOrbitalTransposerCamera.m_FollowOffset;
            virtualOrbitalTransposerCamera.m_FollowOffset = camTurnAngleY * virtualOrbitalTransposerCamera.m_FollowOffset;

            if (virtualOrbitalTransposerCamera.m_FollowOffset.y <= 1f)
            {
                virtualOrbitalTransposerCamera.m_FollowOffset = new Vector3(virtualOrbitalTransposerCamera.m_FollowOffset.x, 1f, virtualOrbitalTransposerCamera.m_FollowOffset.z);
            }
        }

        void OnDestroy()
        {
            customizableControls.Camera.RotateCameraToLeft.started -= RotateAroundPlayerToLeft;
            customizableControls.Camera.RotateCameraToLeft.canceled -= RotateAroundPlayerToLeft;

            customizableControls.Camera.RotateCameraToRight.started -= RotateAroundPlayerToLeft;
            customizableControls.Camera.RotateCameraToRight.canceled -= RotateAroundPlayerToRight;

            customizableControls.Camera.Disable();

            PauseManager.Instance.onPaused -= DisableCameraInput;
            PauseManager.Instance.onResumed -= EnableCameraInput;
        }

        private void EnableCameraInput()
        {
            playerInput.enabled = true;
        }

        private void DisableCameraInput()
        {
            playerInput.enabled = false;
        }

        public void RotateAroundPlayerToLeft(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                currentYaw += yawSpeed * Time.deltaTime;
            }
            else if (callbackContext.canceled)
            {
                currentYaw = 0;
            }
        }

        public void RotateAroundPlayerToRight(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                currentYaw -= yawSpeed * Time.deltaTime;
            }
            else if (callbackContext.canceled)
            {
                currentYaw = 0;
            }
        }

        public void EnableOrbitAroundPlayer(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                orbitAroundPlayer = true;

            }
            else if (callbackContext.canceled)
            {
                orbitAroundPlayer = false;
            }

        }

        public void OrbitAroundPlayer(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (orbitAroundPlayer)
                {
                    camTurnAngleY = Quaternion.AngleAxis(callbackContext.ReadValue<Vector2>().x * Time.deltaTime * rotateSpeed, Vector3.up);
                    camTurnAngleX = Quaternion.AngleAxis(callbackContext.ReadValue<Vector2>().y * Time.deltaTime * rotateSpeed, transform.right);
                }
                else
                {
                    camTurnAngleY = Quaternion.Euler(0, 0, 0);
                    camTurnAngleX = Quaternion.Euler(0, 0, 0);
                }

            }
            else if (callbackContext.canceled)
            {
                camTurnAngleY = Quaternion.Euler(0, 0, 0);
                camTurnAngleX = Quaternion.Euler(0, 0, 0);
            }
        }

        public void ZoomInOut(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                bool isMouseWheelLocked = false;

                foreach (var mouseWheelLocker in mosueWheelLockers)
                {
                    if (!mouseWheelLocker.canZoom)
                    {
                        isMouseWheelLocked = true;
                        break;
                    }
                }

                var isPointerOverDialogueBox = dialogueManager.IsPointerOverDialogueBox();

                if (!isMouseWheelLocked && !isPointerOverDialogueBox)
                {
                    var zoomDegree = callbackContext.ReadValue<float>() / 120;

                    currentZoom -= zoomDegree;
                    var normalized = virtualOrbitalTransposerCamera.m_FollowOffset.normalized;


                    currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

                    virtualOrbitalTransposerCamera.m_FollowOffset = normalized * currentZoom * zoomSpeed;
                }
            }

        }
    }
}

