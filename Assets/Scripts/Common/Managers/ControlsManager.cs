using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using WotN.InputSystem;

namespace WotN.Common.Managers {
    public class ControlsManager : MonoBehaviour
    {
        public static ControlsManager Instance { get;  private set; }

        public bool controlsOptionsDisplayed;

        public event Action<List<InputAction>> resetKeyBindings;
        public event Action<InputAction, int> rebindComplete;
        public event Action<InputAction, int> rebindCanceled;
        public event Action<InputAction, int> rebindStarted;

        private CustomizableControls customizableControls;

        private List<InputAction> inputActions = new List<InputAction>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                if (customizableControls == null)
                    customizableControls = new CustomizableControls();

                SetInputActions();

                customizableControls.Disable();

                LoadBindingOverrides();
            }   
        }

        public CustomizableControls GetCustomizableControls()
        {
            if (customizableControls == null)
                customizableControls = new CustomizableControls();

            return customizableControls;
        }

        public List<InputAction> GetInputActions()
        {
            return inputActions;
        }

        public void StartRebind(string actionName, int bindingIndex)
        {
            InputAction action = inputActions.FirstOrDefault(x => x.name == actionName);
            if (action == null || action.bindings.Count <= bindingIndex)
            {
                Debug.Log("Couldn't find action or binding");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                    DoRebind(action, bindingIndex, true);
            }
            else
                DoRebind(action, bindingIndex, false);
        }

        public void ResetBindings()
        {
            for (int i = 0; i < inputActions.Count; i++)
            {
                InputAction action = inputActions[i];

                if (action == null)
                {
                    Debug.Log("Could not find action or binding");
                    return;
                }

                for (int j = 0; j < action.bindings.Count; j++)
                {
                    action.RemoveBindingOverride(j);

                    SaveBindingOverride(action, j);
                }
            }

            resetKeyBindings?.Invoke(inputActions);
        }

        public bool AreKeyBindingsSetToDefault()
        {
            for (int i = 0; i < inputActions.Count; i++)
            {
                for (int j = 0; j < inputActions[i].bindings.Count; j++)
                {
                    var savedOverriddenKey = inputActions[i].bindings[j].effectivePath;

                    var defaultKeyPath = inputActions[i].bindings[j].path;

                    if (savedOverriddenKey == defaultKeyPath)
                        continue;
                    else
                        return false;

                }
            }

            return true;
        }

        private void DoRebind(InputAction actionToRebind, int bindingIndex, bool allCompositeParts)
        {
            if (actionToRebind == null || bindingIndex < 0)
                return;

            var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

            rebind.OnComplete(operation =>
            {
                operation.Dispose();

                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                        DoRebind(actionToRebind, nextBindingIndex, allCompositeParts);
                }
                string keyName = actionToRebind.GetBindingDisplayString(bindingIndex);

                SetDuplicateKeyBindingToNone(actionToRebind, bindingIndex, keyName);

                SaveBindingOverride(actionToRebind, bindingIndex);
                rebindComplete?.Invoke(actionToRebind, bindingIndex);

                AudioManager.Instance.PlayClickButton();
            });

            rebind.OnCancel(operation =>
            {
                operation.Dispose();
                actionToRebind.ApplyBindingOverride(bindingIndex, "");

                SaveNoBinding(operation.action, bindingIndex);

                rebindCanceled?.Invoke(actionToRebind, bindingIndex);

                AudioManager.Instance.PlayClickButton();
            });

            rebind.WithCancelingThrough("<Keyboard>/escape");

            rebind.WithControlsExcluding("Mouse");

            rebindStarted?.Invoke(actionToRebind, bindingIndex);
            rebind.Start(); //actually starts the rebinding process
        }

        private void SetDuplicateKeyBindingToNone(InputAction actionToRebind, int bindingIndex, string keyName)
        {
            for (int i = 0; i < inputActions.Count; i++)
            {
                for (int j = 0; j < inputActions[i].bindings.Count; j++)
                {
                    if (inputActions[i].name == actionToRebind.name && j == bindingIndex)
                    {
                        continue;
                    }
                    var currentKeyName = inputActions[i].GetBindingDisplayString(j);

                    if (currentKeyName == keyName)
                    {
                        PlayerPrefs.SetString(inputActions[i].actionMap + inputActions[i].name + j, "none");

                        inputActions[i].ApplyBindingOverride(j, "");

                        rebindComplete?.Invoke(inputActions[i], j);
                    }
                }
            }
        }

        private void SaveBindingOverride(InputAction action, int bindingIndex)
        {

            PlayerPrefs.SetString(action.actionMap + action.name + bindingIndex, action.bindings[bindingIndex].overridePath);
        }

        private void SaveNoBinding(InputAction action, int bindingIndex)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + bindingIndex, "none");
        }

        private void LoadBindingOverrides()
        {
            for (int i = 0; i < inputActions.Count; i++)
            {
                for (int j = 0; j < inputActions[i].bindings.Count; j++)
                {
                    string keyBinding = PlayerPrefs.GetString(inputActions[i].actionMap + inputActions[i].name + j);

                    if (!string.IsNullOrEmpty(keyBinding))
                    {
                        inputActions[i].ApplyBindingOverride(j, keyBinding);
                    }
                    else if (keyBinding == "none")
                    {
                        inputActions[i].ApplyBindingOverride(j, "");
                    }
                }
            }
        }

        private void SetInputActions()
        {
            if (customizableControls == null)
                customizableControls = new CustomizableControls();

            inputActions.AddRange(customizableControls);
        }
    }
}
