using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WotN.Interactables;
using WotN.Player;

namespace WotN.Interactables
{
    public class InteractableTriggerSpot : MonoBehaviour
    {
        private Collider spotCollider;

        private StashInteractable stash;

        void Awake()
        {
            spotCollider = GetComponent<Collider>();
            stash = GetComponentInParent<StashInteractable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            spotCollider.enabled = false;

            bool isPlayer = other.TryGetComponent(out PlayerInteractable currentPlayer);

            if (!isPlayer)
            {
                Debug.Log("Error. Current object which triggered the interaction spot is not player");
            }

            if (stash.IsTriggeredByPlayer(currentPlayer))
            {
                stash.OpenStashWindow();
            }
        }
    }
}
