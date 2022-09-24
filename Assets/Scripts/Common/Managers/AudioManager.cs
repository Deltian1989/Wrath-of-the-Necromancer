using UnityEngine;

namespace WotN.Common.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("UI SFX")]
        [SerializeField]
        private AudioClip hoverOverButtonSFX;

        [SerializeField]
        private AudioClip clickButtonSFX;

        [SerializeField]
        private AudioClip pauseSFX;

        [SerializeField]
        private AudioClip[] musicClips;

        [SerializeField]
        private AudioClip[] ambientSFXClips;

        [Header("Audio sources")]
        [SerializeField]
        private AudioSource ambientSFXSource;

        [SerializeField]
        private AudioSource musicAudioSource;

        [SerializeField]
        private AudioSource uiSFXAudioSource;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void PauseBackgroundMusic()
        {
            ambientSFXSource.Pause();

            musicAudioSource.Pause();
        }

        public void StopMusicPermanently()
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = null;
        }

        public void ReplayBackgroundMusic()
        {
            ambientSFXSource.UnPause();

            musicAudioSource.UnPause();
        }

        public void PlayMouseOverButton()
        {
            uiSFXAudioSource.PlayOneShot(hoverOverButtonSFX);
        }

        public void PlayClickButton()
        {
            uiSFXAudioSource.PlayOneShot(clickButtonSFX);
        }

        public void PlayPauseSFX()
        {
            uiSFXAudioSource.PlayOneShot(pauseSFX);
        }

        public void PlayMusic(AudioClip musicAudioClip)
        {
            musicAudioSource.clip = musicAudioClip;
            musicAudioSource.Play();
        }

        public void PlayAmbientSounds(AudioClip ambientSFXClip)
        {
            ambientSFXSource.clip = ambientSFXClip;
            ambientSFXSource.Play();
        }

        public void PlayUISFX(AudioClip uiSFXClip)
        {
            uiSFXAudioSource.PlayOneShot(uiSFXClip);
        }

        public void PlayMusicForCurrentScene(int index)
        {
            if (index == 2)
            {
                musicAudioSource.clip = musicClips[0];
                musicAudioSource.Play();
            }
        }

        public void PlayAmbientSFXForCurrentScene(int index)
        {
            if (index == 2)
            {
                ambientSFXSource.clip = ambientSFXClips[0];
                ambientSFXSource.Play();
            }
        }
    }
}
