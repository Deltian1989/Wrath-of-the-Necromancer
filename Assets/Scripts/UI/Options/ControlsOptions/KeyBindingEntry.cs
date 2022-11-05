using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WotN.UI.Options.Controls
{
    public class KeyBindingEntry : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private TMP_Text functionName;

        [SerializeField]
        [ChildGameObjectsOnly]
        private KeyBindingButton keyBindingButton;

        [SerializeField]
        [ChildGameObjectsOnly]
        private KeyBindingButton altKeyBindingButton;

        private InputAction inputAction;

        public void CreateKeyBindingEntry(InputAction inputAction)
        {
            this.inputAction = inputAction;

            var actionName= inputAction.name;

            functionName.text = actionName;

            keyBindingButton.SetBindingIndex(0);

            altKeyBindingButton.SetBindingIndex(1);

            string bindingKeyName = inputAction.GetBindingDisplayString(0);
            string altBindingKeyName = inputAction.GetBindingDisplayString(1);

            if (string.IsNullOrEmpty(bindingKeyName) || bindingKeyName == "none")
            {
                keyBindingButton.SetButtonToNoInputBindingMode();
                
            }
            else
            {
                keyBindingButton.SetButtonToBoundInputMode(bindingKeyName);
            }

            if (string.IsNullOrEmpty(altBindingKeyName)|| altBindingKeyName == "none")
            {
                altKeyBindingButton.SetButtonToNoInputBindingMode();
            }
            else
            {
                altKeyBindingButton.SetButtonToBoundInputMode(altBindingKeyName);
            }
        }

        public void SetKeyBindingButtonToListeningMode(int bindingIndex)
        {
            if (bindingIndex == 0)
            {
                keyBindingButton.SetButtonToInputListeningMode();
            }
            else if (bindingIndex == 1)
            {
                altKeyBindingButton.SetButtonToInputListeningMode();
            }
        }

        public void SetKeyBindingButtonToNone(int bindingIndex)
        {
            if (bindingIndex == 0)
            {
                keyBindingButton.SetButtonToNoInputBindingMode();
            }
            else if (bindingIndex == 1)
            {
                altKeyBindingButton.SetButtonToNoInputBindingMode();
            }
        }

        public InputAction GetInputAction()
        {
            return inputAction;
        }

        public void SetKeyBinding(string keyBinding, int bindingIndex)
        {
            if (bindingIndex == 0)
            {
                keyBindingButton.SetButtonToBoundInputMode(keyBinding);
            }
            else if (bindingIndex == 1)
            {
                altKeyBindingButton.SetButtonToBoundInputMode(keyBinding);
            }
        }
    }
}