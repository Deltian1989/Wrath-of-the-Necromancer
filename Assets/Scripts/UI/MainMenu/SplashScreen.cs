using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using WotN.Common.Managers;

namespace WotN.UI.MainMenu
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private AudioClip splashMusic;

        void Awake()
        {
            Cursor.visible = false;
        }
        void Update()
        {
            DisplayMainMenu();
        }

        private void DisplayMainMenu()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame ||
                Mouse.current.middleButton.wasPressedThisFrame ||
                Keyboard.current.anyKey.wasPressedThisFrame)
            {
                LoadMainMenu();
            }

        }

        public void PlaySplashMusic()
        {
            AudioManager.Instance.PlayMusic(splashMusic);
        }

        public void LoadMainMenu()
        {
            AudioManager.Instance.StopMusicPermanently();
            SceneManager.LoadScene(1);
            Cursor.visible = true;
        }
    }
}

