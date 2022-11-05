using WotN.ScriptableObjects.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WotN.UI.Dialogue;
using Sirenix.OdinInspector;

namespace WotN.Common.Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public delegate void OnDialogueBoxClosed();

        public static DialogueManager Instance;

        public event OnDialogueBoxClosed onDialogueBoxClosed;

        [SerializeField]
        private CanvasGroup dialogueAreaCG;

        [SerializeField]
        private CanvasGroup playerHUDCG;

        [SerializeField]
        private RectTransform talkWindow;

        [SerializeField]
        private SpeechSlide speechSlideArea;

        [SerializeField]
        private DialogueBox dialogueWindow;

        [SerializeField]
        private int dialogueOptionsCount;

        private TMP_Text[] dialogueOptionTexts;

        private Button[] dialogueOptionButtons;

        private bool isNPCTalking;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            dialogueOptionTexts = dialogueWindow.GetComponentsInChildren<TMP_Text>(true);

            dialogueOptionButtons = dialogueWindow.GetComponentsInChildren<Button>(true);

            dialogueOptionsCount = dialogueOptionTexts.Length;
        }

        public void PopUpDialogueWindow(DialogueOption[] dialogueOptions)
        {
            dialogueAreaCG.alpha = 1;
            dialogueAreaCG.blocksRaycasts = true;

            playerHUDCG.blocksRaycasts = false;

            if (dialogueOptions.Length >= dialogueOptionsCount)
            {
                Debug.LogError($"Number of dialogue options can't be more than {dialogueOptionsCount - 1}");
                return;
            }

            dialogueWindow.gameObject.SetActive(true);

            for (int i = 0; i < dialogueOptions.Length; i++)
            {
                dialogueOptionTexts[i].gameObject.SetActive(true);

                dialogueOptionTexts[i].SetText(dialogueOptions[i].DialogueOptionText);

                dialogueOptionTexts[i].ForceMeshUpdate();

                var dialogueOptionSize = dialogueOptionTexts[i].GetRenderedValues(false);

                var dialogOptionRectTransform = dialogueOptionTexts[i].rectTransform;

                dialogOptionRectTransform.sizeDelta = new Vector2(dialogueOptionSize.x, dialogueOptionSize.y);

                var dialogueTalk = dialogueOptions[i].Talk;

                var talkAudioClip = dialogueOptions[i].talkAudioClip;

                dialogueOptionButtons[i].onClick
                    .AddListener(() => DisplaySpeechSlideWindow(dialogueTalk, talkAudioClip) );
            }

            int lastIndex = dialogueOptions.Length;

            dialogueOptionTexts[lastIndex].gameObject.SetActive(true);

            dialogueOptionTexts[lastIndex].SetText("Back");

            dialogueOptionTexts[lastIndex].ForceMeshUpdate();

            var backOptionSize = dialogueOptionTexts[lastIndex].GetRenderedValues(false);

            var backOptionRectTransform = dialogueOptionTexts[lastIndex].rectTransform;

            backOptionRectTransform.sizeDelta = new Vector2(backOptionSize.x, backOptionSize.y);

            
            dialogueOptionButtons[lastIndex].onClick.AddListener(CloseDialogueWindow);
        }

        public bool IsPointerOverDialogueBox() => dialogueWindow.IsPointerOverDialogueBox();
        

        public bool IsNPCTalking() => isNPCTalking;

        public void SetNPCIsNotTalking()
        {
            AudioManager.Instance.StopSpeechAudio();

            speechSlideArea.DismissSpeechSlide();

            isNPCTalking = false;
        }

        public void CloseDialogueWindow()
        {
            dialogueAreaCG.alpha = 0;
            dialogueAreaCG.blocksRaycasts = false;

            playerHUDCG.blocksRaycasts = true;

            dialogueWindow.gameObject.SetActive(false);

            for (int i = 0; i < dialogueOptionTexts.Length; i++)
            {
                dialogueOptionTexts[i].gameObject.SetActive(false);

                dialogueOptionTexts[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 536.0536f);
                dialogueOptionTexts[i].text = "";
                dialogueOptionButtons[i].onClick.RemoveAllListeners();
            }

            onDialogueBoxClosed?.Invoke();
        }

        private void DisplaySpeechSlideWindow(string dialogueTalk, AudioClip talkClip)
        {
            AudioManager.Instance.PlaySpeechAudio(talkClip);

            dialogueWindow.gameObject.SetActive(false);

            isNPCTalking = true;

            talkWindow.gameObject.SetActive(true);

            speechSlideArea.SetTalkText(dialogueTalk);
        }
    }
}

