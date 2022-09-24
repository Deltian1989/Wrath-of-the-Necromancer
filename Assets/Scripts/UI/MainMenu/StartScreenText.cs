using UnityEngine;
using UnityEngine.InputSystem;
using WotN.Common.Managers;

namespace WotN.UI.MainMenu
{
    public class StartScreenText : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainMenu;

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
                GameManager.Instance.isGameStarted = true;

                mainMenu.SetActive(true);
                this.gameObject.SetActive(false);
                AudioManager.Instance.PlayClickButton();
            }

        }
    }
}

