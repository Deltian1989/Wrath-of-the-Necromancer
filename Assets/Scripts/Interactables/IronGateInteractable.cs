using WotN.Player;
using UnityEngine;
using WotN.Common.Managers;

namespace WotN.Interactables
{

    public class IronGateInteractable : PropInteractableBase
    {
        [SerializeField]
        private bool isGateOpening = false;

        private static readonly int openGateParam = Animator.StringToHash("openGate");

        private Collider interactionArea;

        private Animator animator;

        private AudioSource audioSource;

        void Awake()
        {
            outline = GetComponent<Outline>();
            interactionArea = GetComponent<Collider>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            cursorManager = CursorManager.Instance;
        }

        public override void HighlightProp()
        {
            if (!isGateOpening)
            {
                cursorManager.SetCursor(hoverCursorTexture, true);
                outline.enabled = true;
            }
        }

        public override void SetReadyToInteract(PlayerInteractable player, PlayerMovement playerMovement)
        {
            base.SetReadyToInteract(player, playerMovement);
            interactionArea.enabled = true;
            playerMovement.SetDestination(transform.position);
        }

        public void SetInteractable()
        {
            isGateOpening = true;
            UnhighlightProp();
        }

        public void SetUninteractable()
        {
            isGateOpening = false;
        }

        private void DisbleInteractionArea()
        {
            if (!focusedPlayer)
                interactionArea.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerInteractable>(out var player))
            {
                if (player.CompareFocusedInteractableWith(this))
                {
                    focusedPlayer = player;
                    audioSource.Play();
                    animator.SetBool(openGateParam, true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerInteractable>(out var player))
            {
                if (player.CompareFocusedInteractableWith(this))
                    player.SetFocusedInteractableNull();

                animator.SetBool(openGateParam, false);
                focusedPlayer = null;

                DisbleInteractionArea();

                audioSource.Play();
            }
        }
    }
}


