using WotN.UI.MainMenu;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.Options
{
    public class VideoOptionsUI : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text currentScreenResolutionText;

        [SerializeField]
        private TMPro.TMP_Text currentGraphicsTierText;

        [SerializeField]
        private Slider screenResolutionSlider;

        [SerializeField]
        private Slider graphicsTierSlider;

        [SerializeField]
        private Slider fullScreenSlider;

        [SerializeField]
        private Button saveButton;

        [SerializeField]
        private Button resetButton;

        private bool enableSaveButtonForScreenResolutionSlider = false;

        private bool enableSaveButtonForGraphicsTierSlider = false;

        private bool enableSaveButtonForfullScreenSlider = false;

        void Start()
        {
            screenResolutionSlider.maxValue = VideoOptionsManager.Instance.GetScreenResolutionMaxValue();
            graphicsTierSlider.maxValue = VideoOptionsManager.Instance.GetGraphicsTierMaxValue();

            var currentScreenResolutionValue = VideoOptionsManager.Instance.GetCurrentScreenResolutionValue();
            var currentGraphicsTierValue = VideoOptionsManager.Instance.GetCurrentGraphicsTierValue();
            var currentFullScreenValue = VideoOptionsManager.Instance.GetCurrentFullScreenValue();

            if (screenResolutionSlider.value == currentScreenResolutionValue)
            {
                enableSaveButtonForScreenResolutionSlider = true;
            }
            else
            {
                screenResolutionSlider.value = currentScreenResolutionValue;
            }

            if (graphicsTierSlider.value == currentGraphicsTierValue)
            {
                enableSaveButtonForGraphicsTierSlider = true;
            }
            else
            {
                graphicsTierSlider.value = currentGraphicsTierValue;
            }

            if (fullScreenSlider.value == currentFullScreenValue)
            {
                enableSaveButtonForfullScreenSlider = true;
            }
            else
            {
                fullScreenSlider.value = currentFullScreenValue;
            }

            resetButton.interactable = !VideoOptionsManager.Instance.AreOptionsSetToDefaultValues();

            
        }

        public void OnScreenResolutionChange()
        {
            currentScreenResolutionText.text = VideoOptionsManager.Instance.GetScreenResolutionTextFromValue((int)screenResolutionSlider.value);

            VideoOptionsManager.Instance.SetCurrentScreenResolution((int)screenResolutionSlider.value);

            if (!enableSaveButtonForScreenResolutionSlider)
            {
                enableSaveButtonForScreenResolutionSlider = true;
            }
            else
            {
                saveButton.interactable = !VideoOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
            }
        }

        public void OnGraphicsTierChange()
        {
            currentGraphicsTierText.text = VideoOptionsManager.Instance.GetGraphicsTierTextFromValue((GraphicsTier)graphicsTierSlider.value);

            VideoOptionsManager.Instance.SetCurrentGraphicsTier((GraphicsTier)graphicsTierSlider.value);

            if (!enableSaveButtonForGraphicsTierSlider)
            {
                enableSaveButtonForGraphicsTierSlider = true;

            }
            else
            {
                saveButton.interactable = !VideoOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
            }
        }

        public void OnFullScreenModeChange()
        {
            VideoOptionsManager.Instance.SetCurrentFullScreenMode((int)fullScreenSlider.value);

            if (!enableSaveButtonForfullScreenSlider)
            {
                enableSaveButtonForfullScreenSlider = true;
            }
            else
            {
                saveButton.interactable = !VideoOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
            }
        }

        public void SaveChanges()
        {
            VideoOptionsManager.Instance.SaveOptions();

            saveButton.interactable = false;
            resetButton.interactable = !VideoOptionsManager.Instance.AreOptionsSetToDefaultValues();

            PauseManager.Instance.UpdateOptionsUI();
        }

        public void ResetChanges()
        {
            if (!ControlsManager.Instance.controlsOptionsDisplayed)
            {
                saveButton.interactable = false;
                resetButton.interactable = false;

                VideoOptionsManager.Instance.ResetOptions();

                currentScreenResolutionText.text = VideoOptionsManager.Instance.GetCurrentScreenResolutionText();
                currentGraphicsTierText.text = VideoOptionsManager.Instance.GetCurrentGraphicsTierText();

                enableSaveButtonForScreenResolutionSlider = false;
                enableSaveButtonForGraphicsTierSlider = false;
                enableSaveButtonForfullScreenSlider = false;

                screenResolutionSlider.value = VideoOptionsManager.Instance.GetCurrentScreenResolutionValue();
                graphicsTierSlider.value = VideoOptionsManager.Instance.GetCurrentGraphicsTierValue();
                fullScreenSlider.value = VideoOptionsManager.Instance.GetCurrentFullScreenValue();

                PauseManager.Instance.UpdateOptionsUI();
            }
                
        }

        public void DiscardChanges()
        {
            VideoOptionsManager.Instance.DiscardChanges();

            UpdateUI();
        }

        public void UpdateUI()
        {
            enableSaveButtonForScreenResolutionSlider = false;
            enableSaveButtonForGraphicsTierSlider = false;
            enableSaveButtonForfullScreenSlider = false;

            screenResolutionSlider.value = VideoOptionsManager.Instance.GetCurrentScreenResolutionValue();
            graphicsTierSlider.value = VideoOptionsManager.Instance.GetCurrentGraphicsTierValue();
            fullScreenSlider.value = VideoOptionsManager.Instance.GetCurrentFullScreenValue();

            saveButton.interactable = false;
            resetButton.interactable = !VideoOptionsManager.Instance.AreOptionsSetToDefaultValues();

            currentScreenResolutionText.text = VideoOptionsManager.Instance.GetCurrentScreenResolutionText();
            currentGraphicsTierText.text = VideoOptionsManager.Instance.GetCurrentGraphicsTierText();
        }
    }

}

