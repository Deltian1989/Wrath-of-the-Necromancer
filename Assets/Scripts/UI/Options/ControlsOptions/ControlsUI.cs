using WotN.UI.MainMenu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WotN.Common.Managers;
using Sirenix.OdinInspector;

namespace WotN.UI.Options.Controls
{
    public class ControlsUI : MonoBehaviour
    {
        [SerializeField]
        [AssetsOnly]
        private KeyBindingEntry keyBindingEntryPrefab;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform scrollViewContent;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Button resetButton;

        [SerializeField]
        [ChildGameObjectsOnly]
        private List<KeyBindingEntry> keyBindings;

        void Start()
        {
            ControlsManager.Instance.rebindComplete += UpdateInputEntry;
            ControlsManager.Instance.rebindStarted += SetKeyBindingEntryToListeningMode;
            ControlsManager.Instance.rebindCanceled += SetKeyBindingEntryToNoInputBound;
            ControlsManager.Instance.resetKeyBindings += ResetKeyBindings;

            InitializeUI();
        }

        void OnDestroy()
        {
            ControlsManager.Instance.rebindComplete -= UpdateInputEntry;
            ControlsManager.Instance.rebindStarted -= SetKeyBindingEntryToListeningMode;
            ControlsManager.Instance.rebindCanceled -= SetKeyBindingEntryToNoInputBound;
            ControlsManager.Instance.resetKeyBindings -= ResetKeyBindings;
        }

        public void ResetBindings()
        {
            if (ControlsManager.Instance.controlsOptionsDisplayed)
                ControlsManager.Instance.ResetBindings();
        }

        public void ResetBindingsInPauseWindow()
        {
            ControlsManager.Instance.ResetBindings();
        }

        private void InitializeUI()
        {
            List<InputAction> inputActions = ControlsManager.Instance.GetInputActions();

            for (int i = 0; i < inputActions.Count; i++)
            {
                KeyBindingEntry keyBindingEntry = Instantiate(keyBindingEntryPrefab, scrollViewContent);

                keyBindingEntry.CreateKeyBindingEntry(inputActions[i]);

                keyBindings.Add(keyBindingEntry);
            }

            resetButton.interactable = !ControlsManager.Instance.AreKeyBindingsSetToDefault();
        }

        private KeyBindingEntry GetKeyBindingEntry(InputAction actionToRebind)
        {
            var keyBindingEntry = keyBindings.Find(x => x.GetInputAction().name == actionToRebind.name);

            return keyBindingEntry;
        }

        private void ResetKeyBindings(List<InputAction> inputActions)
        {
            if (inputActions.Count != keyBindings.Count)
            {
                Debug.LogError("Count of input actions must be equal to the count of key bindnigs on UI");
            }

            for (int i = 0; i < inputActions.Count; i++)
            {
                for (int j = 0; j < inputActions[i].bindings.Count; j++)
                {
                    string keyName = inputActions[i].GetBindingDisplayString(j);

                    if (keyName != "none" && !string.IsNullOrEmpty(keyName))
                        keyBindings[i].SetKeyBinding(keyName, j);
                    else
                        keyBindings[i].SetKeyBindingButtonToNone(j);
                }

            }

            resetButton.interactable = false;
        }

        private void UpdateInputEntry(InputAction actionToRebind, int bindingIndex)
        {
            var keyBindingEntry = GetKeyBindingEntry(actionToRebind);

            if (actionToRebind.GetBindingDisplayString(bindingIndex) == "")
            {
                keyBindingEntry.SetKeyBindingButtonToNone(bindingIndex);
            }
            else
            {
                keyBindingEntry.SetKeyBinding(actionToRebind.GetBindingDisplayString(bindingIndex), bindingIndex);
            }

            resetButton.interactable = !ControlsManager.Instance.AreKeyBindingsSetToDefault();
        }

        private void SetKeyBindingEntryToListeningMode(InputAction actionToRebind, int bindingIndex)
        {
            var keyBindingEntry = GetKeyBindingEntry(actionToRebind);

            keyBindingEntry.SetKeyBindingButtonToListeningMode(bindingIndex);

            resetButton.interactable = !ControlsManager.Instance.AreKeyBindingsSetToDefault();
        }

        private void SetKeyBindingEntryToNoInputBound(InputAction actionToRebind, int bindingIndex)
        {
            var keyBindingEntry = GetKeyBindingEntry(actionToRebind);

            keyBindingEntry.SetKeyBindingButtonToNone(bindingIndex);

            resetButton.interactable = !ControlsManager.Instance.AreKeyBindingsSetToDefault();
        }
    }
}