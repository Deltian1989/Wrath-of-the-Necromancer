using WotN.ScriptableObjects.Dialogue;
using TMPro;
using UnityEngine;
using WotN.Common.Managers;

namespace WotN.Interactables
{
    public class NPCInteractable : MonoBehaviour
    {
        private const string NPCNameTag = "NPCName";

        [SerializeField]
        private DialogueSet dialogueSet;

        [SerializeField]
        private string npcName;

        [SerializeField]
        private Transform npcNameSpot;

        private Outline outline;

        private DialogueManager dialogueManager;

        private CursorManager cursorManager;

        private Camera cam;

        private RectTransform npcNamePosition;

        private TextMeshProUGUI npcNameTitle;

        private GameObject NPCNameplate;

        private NPCMovement npcMovement;

        void Awake()
        {
            outline = GetComponent<Outline>();
            dialogueManager = FindObjectOfType<DialogueManager>();
            cam = Camera.main;
            npcMovement = GetComponent<NPCMovement>();
        }

        void Start()
        {
            cursorManager = CursorManager.Instance;

            NPCNameplate = GameObject.FindWithTag(NPCNameTag);

            npcNamePosition = NPCNameplate.GetComponent<RectTransform>();
            npcNameTitle = npcNamePosition.GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if (outline.enabled)
            {
                Vector3 screenPoint = cam.WorldToScreenPoint(npcNameSpot.position);

                npcNamePosition.position = screenPoint;
            }
        }

        public void HighlightNpc()
        {
            npcNameTitle.enabled = true;

            npcNameTitle.text = npcName;

            Vector3 screenPoint = cam.WorldToScreenPoint(npcNameSpot.position);

            npcNamePosition.position = screenPoint;

            outline.enabled = true;
            cursorManager.SetCursor(cursorManager.TalkCursorTexture);
        }

        public void UnhighlightNpc()
        {
            npcNameTitle.enabled = false;

            npcNameTitle.text = null;

            outline.enabled = false;
            cursorManager.SetDefaultCursorTexture();
        }

        public virtual void OpenDialogueBox()
        {
            dialogueManager.PopUpDialogueWindow(dialogueSet.dialogueOptions);
        }

        public void UnfocusNPC()
        {
            npcMovement.StopTalking();
        }

        public string GetNPCName()
        {
            return npcName;
        }
    }
}

