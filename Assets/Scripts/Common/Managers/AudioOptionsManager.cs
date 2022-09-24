using UnityEngine;

namespace WotN.Common.Managers
{
    public class AudioOptionsManager : MonoBehaviour
    {
        public static AudioOptionsManager Instance { get; private set; }

        public float CurrentMasterVolume { get; set; }

        public float CurrentSFXVolume { get; set; }

        public float CurrentUIVolume { get; set; }

        public float CurrentNPCSpeechVolume { get; set; }

        public float CurrentAmbientVolume { get; set; }

        public float CurrentMusicVolume { get; set; }

        private float defaultMasterVolume;

        private float defaultSFXVolume;

        private float defaultUIVolume;

        private float defaultNPCSpeechVolume;

        private float defaultAmbientVolume;

        private float defaultMusicVolume;

        private const string currentMasterVolumeKeyName = "currentMasterVolume";
        private const string currentSFXVolumeKeyName = "currentSFXVolume";
        private const string currentUIVolumeKeyName = "currentUIVolume";
        private const string currentNPCSpeechVolumeKeyName = "currentNPCSpeechVolume";
        private const string currentAmbientVolumeKeyName = "currentAmbientVolume";
        private const string currentMusicVolumeKeyName = "currentMusicVolume";

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                return;

            CurrentMasterVolume = PlayerPrefs.GetFloat(currentMasterVolumeKeyName, defaultMasterVolume);
            CurrentSFXVolume = PlayerPrefs.GetFloat(currentSFXVolumeKeyName, defaultSFXVolume);
            CurrentUIVolume = PlayerPrefs.GetFloat(currentUIVolumeKeyName, defaultUIVolume);
            CurrentNPCSpeechVolume = PlayerPrefs.GetFloat(currentNPCSpeechVolumeKeyName, defaultNPCSpeechVolume);
            CurrentAmbientVolume = PlayerPrefs.GetFloat(currentAmbientVolumeKeyName, defaultAmbientVolume);
            CurrentMusicVolume = PlayerPrefs.GetFloat(currentMusicVolumeKeyName, defaultMusicVolume);
        }

        public bool AreOptionsSetToDefaultValues()
        {
            var savedCurrentMasterVolume = PlayerPrefs.GetFloat(currentMasterVolumeKeyName, defaultMasterVolume);
            var savedCurrentSFXVolume = PlayerPrefs.GetFloat(currentSFXVolumeKeyName, defaultSFXVolume);
            var savedCurrentUIVolume = PlayerPrefs.GetFloat(currentUIVolumeKeyName, defaultUIVolume);
            var savedCurrentNPCSpeechVolume = PlayerPrefs.GetFloat(currentNPCSpeechVolumeKeyName, defaultNPCSpeechVolume);
            var savedCurrentAmbientVolume = PlayerPrefs.GetFloat(currentAmbientVolumeKeyName, defaultAmbientVolume);
            var savedCurrentMusicVolume = PlayerPrefs.GetFloat(currentMusicVolumeKeyName, defaultMusicVolume);

            return defaultMasterVolume == savedCurrentMasterVolume &&
                defaultSFXVolume == savedCurrentSFXVolume &&
                savedCurrentUIVolume == defaultUIVolume &&
                savedCurrentNPCSpeechVolume == defaultNPCSpeechVolume &&
                savedCurrentAmbientVolume == defaultAmbientVolume &&
                savedCurrentMusicVolume == defaultMusicVolume;
        }

        public bool AreSelectedOptionsSameAsSaved()
        {
            var savedCurrentMasterVolume = PlayerPrefs.GetFloat(currentMasterVolumeKeyName, defaultMasterVolume);
            var savedCurrentSFXVolume = PlayerPrefs.GetFloat(currentSFXVolumeKeyName, defaultSFXVolume);
            var savedCurrentUIVolume = PlayerPrefs.GetFloat(currentUIVolumeKeyName, defaultUIVolume);
            var savedCurrentNPCSpeechVolume = PlayerPrefs.GetFloat(currentNPCSpeechVolumeKeyName, defaultNPCSpeechVolume);
            var savedCurrentAmbientVolume = PlayerPrefs.GetFloat(currentAmbientVolumeKeyName, defaultAmbientVolume);
            var savedCurrentMusicVolume = PlayerPrefs.GetFloat(currentMusicVolumeKeyName, defaultMusicVolume);

            return CurrentMasterVolume == savedCurrentMasterVolume &&
                CurrentSFXVolume == savedCurrentSFXVolume &&
                savedCurrentUIVolume == CurrentUIVolume &&
                savedCurrentNPCSpeechVolume == CurrentNPCSpeechVolume &&
                savedCurrentAmbientVolume == CurrentAmbientVolume &&
                savedCurrentMusicVolume == CurrentMusicVolume;
        }

        public void SaveOptions()
        {
            PlayerPrefs.SetFloat(currentMasterVolumeKeyName, CurrentMasterVolume);
            PlayerPrefs.SetFloat(currentSFXVolumeKeyName, CurrentSFXVolume);
            PlayerPrefs.SetFloat(currentUIVolumeKeyName, CurrentUIVolume);
            PlayerPrefs.SetFloat(currentNPCSpeechVolumeKeyName, CurrentNPCSpeechVolume);
            PlayerPrefs.SetFloat(currentAmbientVolumeKeyName, CurrentAmbientVolume);
            PlayerPrefs.SetFloat(currentMusicVolumeKeyName, CurrentMusicVolume);
        }

        public void ResetOptions()
        {
            PlayerPrefs.SetFloat(currentMasterVolumeKeyName, defaultMasterVolume);
            PlayerPrefs.SetFloat(currentSFXVolumeKeyName, defaultSFXVolume);
            PlayerPrefs.SetFloat(currentUIVolumeKeyName, defaultUIVolume);
            PlayerPrefs.SetFloat(currentNPCSpeechVolumeKeyName, defaultNPCSpeechVolume);
            PlayerPrefs.SetFloat(currentAmbientVolumeKeyName, defaultAmbientVolume);
            PlayerPrefs.SetFloat(currentMusicVolumeKeyName, defaultMusicVolume);

            CurrentMasterVolume = defaultMasterVolume;
            CurrentSFXVolume = defaultSFXVolume;
            CurrentUIVolume = defaultUIVolume;
            CurrentNPCSpeechVolume = defaultNPCSpeechVolume;
            CurrentAmbientVolume = defaultAmbientVolume;
            CurrentMusicVolume = defaultMusicVolume;
        }

        public void DiscardChanges()
        {
            CurrentMasterVolume = PlayerPrefs.GetFloat(currentMasterVolumeKeyName, defaultMasterVolume);
            CurrentSFXVolume = PlayerPrefs.GetFloat(currentSFXVolumeKeyName, defaultSFXVolume);
            CurrentUIVolume = PlayerPrefs.GetFloat(currentUIVolumeKeyName, defaultUIVolume);
            CurrentNPCSpeechVolume = PlayerPrefs.GetFloat(currentNPCSpeechVolumeKeyName, defaultNPCSpeechVolume);
            CurrentAmbientVolume = PlayerPrefs.GetFloat(currentAmbientVolumeKeyName, defaultAmbientVolume);
            CurrentMusicVolume = PlayerPrefs.GetFloat(currentMusicVolumeKeyName, defaultMusicVolume);
        }
    }
}