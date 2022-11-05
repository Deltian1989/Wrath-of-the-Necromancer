using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.Options.Controls
{
    public class KeyBindingButton : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private TMP_Text keyBindingText;

        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject keyBindingNoneText;

        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject pressKeyText;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Button button;

        [SerializeField]
        private KeyBindingEntry keyBindingEntry;

        private int bindingIndex;

        void Start()
        {
            button.onClick.AddListener(() => StartRebind());
        }

        public void SetBindingIndex(int index)
        {
            bindingIndex = index;
        }

        public void SetButtonToInputListeningMode()
        {
            button.interactable = false;
            keyBindingText.text = null;

            keyBindingNoneText.SetActive(false);
            pressKeyText.SetActive(true);
        }

        public void SetButtonToBoundInputMode(string keyBinding)
        {
            button.interactable = true;
            keyBindingText.gameObject.SetActive(true);
            keyBindingText.text = keyBinding;

            keyBindingNoneText.SetActive(false);

            pressKeyText.SetActive(false);
        }

        public void SetButtonToNoInputBindingMode()
        {
            button.interactable = true;
            keyBindingText.text = null;

            keyBindingNoneText.SetActive(true);
            pressKeyText.SetActive(false);
        }

        private void StartRebind()
        {
            ControlsManager.Instance.StartRebind(keyBindingEntry.GetInputAction().name, bindingIndex);

            AudioManager.Instance.PlayMouseOverButton();
        }
    }
}