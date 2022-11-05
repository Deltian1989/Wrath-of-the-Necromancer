using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Common.Managers;
using WotN.Player;

namespace WotN.Interactables
{
    [RequireComponent(typeof(Outline))]
    public abstract class PropInteractableBase : MonoBehaviour
    {
        [SerializeField]
        [SceneObjectsOnly]
        protected PlayerInteractable focusedPlayer;

        [SerializeField]
        [SceneObjectsOnly]
        protected PlayerMovement focusedPlayerMovement;

        [SerializeField]
        [AssetsOnly]
        protected Texture2D hoverCursorTexture;

        protected Outline outline;

        protected CursorManager cursorManager;

        void Awake()
        {
            outline = GetComponent<Outline>();
            cursorManager = CursorManager.Instance;
        }

        public virtual void HighlightProp()
        {
            cursorManager.SetCursor(hoverCursorTexture, false);
            outline.enabled = true;
        }

        public virtual void UnhighlightProp()
        {
            outline.enabled = false;
            cursorManager.SetDefaultCursorTexture();
        }

        public virtual void SetReadyToInteract(PlayerInteractable player, PlayerMovement playerMovement)
        {
            focusedPlayer = player;
            focusedPlayerMovement = playerMovement;
        }

        public virtual void UnfocusProp()
        {
            focusedPlayer = null;
            focusedPlayerMovement = null;
        }
    }
}
