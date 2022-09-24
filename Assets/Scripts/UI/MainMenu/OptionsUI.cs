using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.MainMenu
{
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup[] optionsSections;

        [SerializeField]
        private Button[] optionsSectionButtons;

        [SerializeField]
        private Button resetButton;

        public void DisplayOptionsSection(int index)
        {
            for (int i = 0; i < optionsSections.Length; i++)
            {
                if (i == index)
                {
                    optionsSections[index].alpha = 1;
                    optionsSections[index].blocksRaycasts = true;
                }
                else
                {
                    optionsSections[i].alpha = 0;
                    optionsSections[i].blocksRaycasts = false;
                }

                if (index == 2)
                {
                    ControlsManager.Instance.controlsOptionsDisplayed = true;
                    resetButton.interactable = !ControlsManager.Instance.AreKeyBindingsSetToDefault();
                }
                else if (index == 1)
                {
                    ControlsManager.Instance.controlsOptionsDisplayed = false;
                    resetButton.interactable = !AudioOptionsManager.Instance.AreOptionsSetToDefaultValues();
                }
                else if (index == 0)
                {
                    ControlsManager.Instance.controlsOptionsDisplayed = false;
                    resetButton.interactable = !VideoOptionsManager.Instance.AreOptionsSetToDefaultValues();
                }
            }

        }

        public void LeaveOptions()
        {
            DisplayOptionsSection(0);
        }
    }
}
