using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WotN.Common.Managers;
using WotN.Common.Utils;
using WotN.UI.Utils;

namespace WotN.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int horizontalSpeedParam = Animator.StringToHash("HorizontalSpeed");

        private PlayerManager playerManager;

        private PlayerNPCInteractable playerNPCInteractable;
        private PlayerInteractable playerInteractable;
        private PlayerInput playerInput;

        private Camera cam;
        private NavMeshAgent navMeshAgent;
        private Animator anim;

        void Awake()
        {
            playerNPCInteractable = GetComponent<PlayerNPCInteractable>();
            playerInteractable = GetComponent<PlayerInteractable>();
            cam = FindObjectOfType<CameraController>().GetComponent<Camera>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
        }

        void Start()
        {
            playerManager = PlayerManager.Instance;
            PauseManager.Instance.onResumed += EnablePlayerInput;
            PauseManager.Instance.onPaused += DisablePlayerInput;
            playerInput.actions["MovePlayerToDestination"].started += MovePlayer;
        }

        void Update()
        {
            HandlePlayerMovementAnimation();
        }

        void OnDestroy()
        {
            PauseManager.Instance.onResumed -= EnablePlayerInput;
            PauseManager.Instance.onPaused -= DisablePlayerInput;

            playerInput.actions["MovePlayerToDestination"].started -= MovePlayer;
        }

        public void MovePlayer(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (RaycastUtilities.PointerIsOverUI(Mouse.current.position.ReadValue()))
                { return; }

                if (!playerNPCInteractable.IsPlayerTalking())
                {
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                    if (Physics.Raycast(ray, out var hit, playerManager.DestinationSettingDistance, playerManager.WalkableLayers))
                    {

                        navMeshAgent.SetDestination(hit.point);
                    }
                }
            }
        }

        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }

        private void HandlePlayerMovementAnimation()
        {
            anim.SetFloat(horizontalSpeedParam, navMeshAgent.velocity.magnitude);
        }

        private void EnablePlayerInput()
        {
            playerInput.enabled = true;
        }

        private void DisablePlayerInput()
        {
            playerInput.enabled = false;
        }
    }
}
