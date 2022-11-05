using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WotN.Common.Managers;
using WotN.Common.Utils;
using WotN.Interactables;
using WotN.UI.Utils;

namespace WotN.Player
{
    public class PlayerInteractable : MonoBehaviour
    {
        [SerializeField]
        [SceneObjectsOnly]
        private PropInteractableBase focusedPropInteractable;

        [SerializeField]
        [SceneObjectsOnly]
        private PropInteractableBase highlightedPropInteractable;

        private Camera cam;

        private PlayerManager playerManager;

        private PlayerNPCInteractable playerNPCInteractable;
        private PlayerMovement playerMovement;
        private PlayerInput playerInput;

        void Awake()
        {
            cam = FindObjectOfType<CameraController>().GetComponent<Camera>();
            playerNPCInteractable = GetComponent<PlayerNPCInteractable>();
            playerMovement = GetComponent<PlayerMovement>();
            playerInput = GetComponent<PlayerInput>();
        }

        void Start()
        {
            playerManager = PlayerManager.Instance;
            playerInput.actions["MovePlayerToDestination"].started += MovePlayerToInteractableProp;
        }

        void Update()
        {
            HandleHoveringOverInteractableProp();
        }

        void OnDestroy()
        {
            playerInput.actions["MovePlayerToDestination"].started -= MovePlayerToInteractableProp;
        }

        public void MovePlayerToInteractableProp(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (RaycastUtilities.PointerIsOverUI(Mouse.current.position.ReadValue()))
                    return;

                if (!playerNPCInteractable.IsPlayerTalking())
                {
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out var hit, playerManager.HighlightDistance, playerManager.InteractableLayers))
                    {
                        bool isPropInteractable = hit.transform.TryGetComponent<PropInteractableBase>(out var currentFocusedPropInteractable);

                        if (isPropInteractable)
                        {
                            focusedPropInteractable = currentFocusedPropInteractable;
                            currentFocusedPropInteractable.SetReadyToInteract(this, playerMovement);
                            return;
                        }
                    }

                    UnfocusPlayer();
                }
            }
        }

        public bool CompareFocusedInteractableWith(PropInteractableBase propInteractable)
        {
            return focusedPropInteractable == propInteractable;
        }

        public void SetFocusedInteractableNull()
        {
            focusedPropInteractable = null;
        }

        public void UnfocusPlayer()
        {
            if (focusedPropInteractable)
            {
                focusedPropInteractable.UnfocusProp();
                focusedPropInteractable = null;
            }
        }

        private void HandleHoveringOverInteractableProp()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (!playerNPCInteractable.IsPlayerTalking())
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out var hitInteractable, playerManager.HighlightDistance, playerManager.InteractableLayers))
                {
                    HandlePropInteraction(hitInteractable);

                }
                else
                {
                    if (highlightedPropInteractable)
                    {
                        highlightedPropInteractable.UnhighlightProp();
                        highlightedPropInteractable = null;
                    }

                }

            }
        }

        private void HandlePropInteraction(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent<PropInteractableBase>(out var prop))
            {
                prop.HighlightProp();
                highlightedPropInteractable = prop;
            }
            else
            {
                if (highlightedPropInteractable)
                {
                    highlightedPropInteractable.UnhighlightProp();
                    highlightedPropInteractable = null;
                }
            }

        }
    }
}

