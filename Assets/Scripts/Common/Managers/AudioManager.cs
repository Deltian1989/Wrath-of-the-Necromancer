using Sirenix.OdinInspector;
using UnityEngine;

namespace WotN.Common.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("UI SFX")]
        [SerializeField]
        [AssetsOnly]
        private AudioClip hoverOverButtonSFX;

        [SerializeField]
        [AssetsOnly]
        private AudioClip clickButtonSFX;

        [SerializeField]
        [AssetsOnly]
        private AudioClip pauseSFX;

        [SerializeField]
        [AssetsOnly]
        private AudioClip[] musicClips;

        [SerializeField]
        [AssetsOnly]
        private AudioClip[] ambientSFXClips;

        [Header("Audio sources")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private AudioSource ambientSFXSource;

        [SerializeField]
        [ChildGameObjectsOnly]
        private AudioSource musicAudioSource;

        [SerializeField]
        [ChildGameObjectsOnly]
        private AudioSource uiSFXAudioSource;

        [SerializeField]
        [ChildGameObjectsOnly]
        private AudioSource speechAudioSource;

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

        public void PlaySpeechAudio(AudioClip speechAudioClip)
        {
            StopSpeechAudio();

            speechAudioSource.PlayOneShot(speechAudioClip);
        }

        public void StopSpeechAudio()
        {
            speechAudioSource.Stop();
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
