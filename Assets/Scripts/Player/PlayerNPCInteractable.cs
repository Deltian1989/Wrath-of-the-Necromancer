using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WotN.Common.Managers;
using WotN.Common.Utils;
using WotN.Interactables;
using WotN.Player;
using WotN.UI.Utils;

public class PlayerNPCInteractable : MonoBehaviour
{
    [SerializeField]
    private NPCInteractable highlightedNPC;

    [SerializeField]
    private NPCInteractable focusedNPC;

    private bool isTalking;

    private PlayerManager playerManager;

    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private Camera cam;
    private DialogueManager dialogueManager;

    void Awake()
    {
        cam = FindObjectOfType<CameraController>().GetComponent<Camera>(); ;
        playerMovement = GetComponent<PlayerMovement>();
        dialogueManager = DialogueManager.Instance;
        playerInput = GetComponent<PlayerInput>();
        dialogueManager.onDialogueBoxClosed += StopTalking;
    }

    void Start()
    {
        playerManager = PlayerManager.Instance;
        playerInput.actions["MovePlayerToDestination"].started += MovePlayerToNPC;
        playerInput.actions["DismissDialogueWithMouseButton"].started += HandleTalkWithNPCWithMouseButtonPressed;
    }

    void Update()
    {
        HandleHoveringOverNPC();
    }

    void OnDestroy()
    {
        playerInput.actions["MovePlayerToDestination"].started -= MovePlayerToNPC;
        playerInput.actions["DismissDialogueWithMouseButton"].started -= HandleTalkWithNPCWithMouseButtonPressed;
    }

    public void MovePlayerToNPC(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (RaycastUtilities.PointerIsOverUI(Mouse.current.position.ReadValue()))
                return;

            if (!IsPlayerTalking())
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out var hit, playerManager.HighlightDistance, playerManager.NPCInteractableLayers))
                {
                    focusedNPC = hit.transform.GetComponent<NPCInteractable>();
                }
                else
                {
                    focusedNPC = null;
                }
            }
        }
    }

    public void HandleTalkToNPCWithEscPressed()
    {
        if (!dialogueManager.IsNPCTalking() && isTalking)
        {
            dialogueManager.CloseDialogueWindow();

        }
        else if (dialogueManager.IsNPCTalking() && isTalking)
        {
            dialogueManager.SetNPCIsNotTalking();
        }
    }

    public void HandleTalkWithNPCWithMouseButtonPressed(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (!dialogueManager.IsNPCTalking() && isTalking)
            {

                if (!dialogueManager.IsPointerOverDialogueBox())
                    dialogueManager.CloseDialogueWindow();

            }
            else if (dialogueManager.IsNPCTalking() && isTalking)
            {
                dialogueManager.SetNPCIsNotTalking();
            }
        }
    }

    public bool IsPlayerTalking()
    {
        return isTalking;
    }

    private void HandleHoveringOverNPC()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (highlightedNPC)
            {
                highlightedNPC.UnhighlightNpc();
                highlightedNPC = null;
            }
            return;
        }

        if (!isTalking)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hitInteractable, playerManager.HighlightDistance, playerManager.NPCInteractableLayers))
            {

                HandleNPCInteraction(hitInteractable);

            }
            else
            {
                if (highlightedNPC)
                {
                    highlightedNPC.UnhighlightNpc();
                    highlightedNPC = null;
                }
            }

            if (focusedNPC)
            {
                float distanceFromNPC = CheckDistanceFromFocusedNPC();

                if (distanceFromNPC <= playerManager.MinDistanceFromNPCToInteract)
                {
                    playerMovement.SetDestination(transform.position);

                    var npcMovment = focusedNPC.GetComponent<NPCMovement>();
                    isTalking = true;
                    npcMovment.TalkToPlayer(this);


                    if (highlightedNPC)
                    {
                        highlightedNPC.UnhighlightNpc();
                        highlightedNPC = null;
                    }

                    focusedNPC.OpenDialogueBox();
                }
                else
                {
                    playerMovement.SetDestination(focusedNPC.transform.position);
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(focusedNPC.transform.position - transform.position), playerManager.RotateTime * Time.deltaTime);


        }
    }

    private float CheckDistanceFromFocusedNPC()
    {
        if (focusedNPC)
        {
            return Vector3.Distance(transform.position, focusedNPC.transform.position);
        }

        return playerManager.MinDistanceFromNPCToInteract + 1;
    }

    private void HandleNPCInteraction(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent<NPCInteractable>(out var npc))
        {
            npc.HighlightNpc();
            highlightedNPC = npc;
        }
        else
        {
            if (highlightedNPC)
            {
                highlightedNPC.UnhighlightNpc();
                highlightedNPC = null;
            }
        }

    }

    private void StopTalking()
    {
        isTalking = false;

        focusedNPC.UnfocusNPC();

        focusedNPC = null;
    }
}
