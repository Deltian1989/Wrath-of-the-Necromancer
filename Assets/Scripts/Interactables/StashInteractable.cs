using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Common.Managers;
using WotN.Player;

namespace WotN.Interactables
{
    public class StashInteractable : PropInteractableBase
    {
        private readonly string openParameter = "open";

        [SerializeField]
        [ChildGameObjectsOnly]
        private Collider stashOpenTriggerSpot;

        [SerializeField]
        private float rotateTime = 6f;

        [SerializeField]
        [AssetsOnly]
        private AudioClip openChestClip;

        [AssetsOnly]
        [SerializeField]
        private AudioClip closeChestClip;

        private Animator anim;

        private AudioSource audioSource;

        public override void SetReadyToInteract(PlayerInteractable player, PlayerMovement playerMovement)
        {
            base.SetReadyToInteract(player, playerMovement);

            if (anim == null)
                anim=GetComponent<Animator>();

            if (audioSource == null)
                audioSource =GetComponent<AudioSource>();

            stashOpenTriggerSpot.enabled = true;

            playerMovement.SetDestination(stashOpenTriggerSpot.transform.position);
        }

        void Update()
        {
            HandleFocusOnStash();
        }

        public override void UnfocusProp()
        {
            base.UnfocusProp();
            StashManager.Instance.CloseStash();
            audioSource.PlayOneShot(closeChestClip);
            anim.SetBool(openParameter, false);
        }

        public bool IsTriggeredByPlayer(PlayerInteractable player)
        {
            return focusedPlayer == player;
        }

        public void OpenStashWindow()
        {
            anim.SetBool(openParameter, true);

            StashManager.Instance.OpenStash();
            audioSource.PlayOneShot(openChestClip);
        }

        private void HandleFocusOnStash()
        {
            if (focusedPlayer && StashManager.Instance.IsStashOpened)
                focusedPlayer.transform.rotation = Quaternion.Slerp(focusedPlayer.transform.rotation, Quaternion.LookRotation(transform.position - focusedPlayer.transform.position), rotateTime * Time.deltaTime);

        }
    }
}

