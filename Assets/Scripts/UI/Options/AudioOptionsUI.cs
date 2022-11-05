using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.Options
{
    public class AudioOptionsUI : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider masterVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider SFXVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider UIVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider NPCSpeechVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider ambientVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Slider musicVolumeSlider;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Button saveButton;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Button resetButton;

        void Start()
        {
            masterVolumeSlider.value = AudioOptionsManager.Instance.CurrentMasterVolume;
            SFXVolumeSlider.value = AudioOptionsManager.Instance.CurrentSFXVolume;
            UIVolumeSlider.value = AudioOptionsManager.Instance.CurrentUIVolume;
            NPCSpeechVolumeSlider.value = AudioOptionsManager.Instance.CurrentNPCSpeechVolume;
            ambientVolumeSlider.value = AudioOptionsManager.Instance.CurrentAmbientVolume;
            musicVolumeSlider.value = AudioOptionsManager.Instance.CurrentMusicVolume;
        }

        public void OnMasterVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentMasterVolume = masterVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void OnSFXVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentSFXVolume = SFXVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void OnUIVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentUIVolume = UIVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void OnNPCSpeechVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentNPCSpeechVolume = NPCSpeechVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void OnAmbientVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentAmbientVolume = ambientVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void OnMusicVolumeChange()
        {
            AudioOptionsManager.Instance.CurrentMusicVolume = musicVolumeSlider.value;

            saveButton.interactable = !AudioOptionsManager.Instance.AreSelectedOptionsSameAsSaved();
        }

        public void SaveChanges()
        {
            AudioOptionsManager.Instance.SaveOptions();

            saveButton.interactable = false;
            resetButton.interactable = !AudioOptionsManager.Instance.AreOptionsSetToDefaultValues();

            PauseManager.Instance.UpdateOptionsUI();
        }

        public void ResetChanges()
        {
            if (!ControlsManager.Instance.controlsOptionsDisplayed)
            {
                saveButton.interactable = false;
                resetButton.interactable = false;

                AudioOptionsManager.Instance.ResetOptions();

                masterVolumeSlider.value = AudioOptionsManager.Instance.CurrentMasterVolume;
                SFXVolumeSlider.value = AudioOptionsManager.Instance.CurrentSFXVolume;
                UIVolumeSlider.value = AudioOptionsManager.Instance.CurrentUIVolume;
                NPCSpeechVolumeSlider.value = AudioOptionsManager.Instance.CurrentNPCSpeechVolume;
                ambientVolumeSlider.value = AudioOptionsManager.Instance.CurrentAmbientVolume;
                musicVolumeSlider.value = AudioOptionsManager.Instance.CurrentMusicVolume;

                PauseManager.Instance.UpdateOptionsUI();
            }
        }

        public void DiscardChanges()
        {
            AudioOptionsManager.Instance.DiscardChanges();

            UpdateUI();
        }

        public void UpdateUI()
        {
            masterVolumeSlider.value = AudioOptionsManager.Instance.CurrentMasterVolume;
            SFXVolumeSlider.value = AudioOptionsManager.Instance.CurrentSFXVolume;
            UIVolumeSlider.value = AudioOptionsManager.Instance.CurrentUIVolume;
            NPCSpeechVolumeSlider.value = AudioOptionsManager.Instance.CurrentNPCSpeechVolume;
            ambientVolumeSlider.value = AudioOptionsManager.Instance.CurrentAmbientVolume;
            musicVolumeSlider.value = AudioOptionsManager.Instance.CurrentMusicVolume;

            saveButton.interactable = false;
            resetButton.interactable = !AudioOptionsManager.Instance.AreOptionsSetToDefaultValues();
        }
    }
}

