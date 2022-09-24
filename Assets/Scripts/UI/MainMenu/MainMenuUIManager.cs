using UnityEngine;
using WotN.Common.Managers;

namespace WotN.UI.MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip musicClip;

        [SerializeField]
        private GameObject startGameText;

        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private CanvasGroup mainMenuCG;

        [SerializeField]
        private CanvasGroup heroListCG;

        [SerializeField]
        private CanvasGroup creditsCG;

        [SerializeField]
        private CanvasGroup optionsCG;

        [SerializeField]
        private CanvasGroup createHeroCG;

        private int selectedDifficulty;

        void Start()
        {
            AudioManager.Instance.PlayMusic(musicClip);

            if (GameManager.Instance.isGameStarted)
            {
                startGameText.SetActive(false);
                mainMenu.SetActive(true);
            }
            else
            {
                startGameText.SetActive(true);
                mainMenu.SetActive(false);
            }
        }

        public void DisplayCredits()
        {
            creditsCG.alpha = 1;
            creditsCG.blocksRaycasts = true;
        }

        public void LeaveCredits()
        {
            creditsCG.alpha = 0;
            creditsCG.blocksRaycasts = false;
        }

        public void DisplayHeroList()
        {
            heroListCG.alpha = 1;
            heroListCG.blocksRaycasts = true;
        }

        public void DisplayMainMenu()
        {
            mainMenuCG.alpha = 1;
            mainMenuCG.blocksRaycasts = true;
        }

        public void LeaveMainMenu()
        {
            mainMenuCG.alpha = 0;
            mainMenuCG.blocksRaycasts = false;
        }

        public void DisplayOptions()
        {
            optionsCG.alpha = 1;
            optionsCG.blocksRaycasts = true;
        }

        public void LeaveOptions()
        {
            optionsCG.alpha = 0;
            optionsCG.blocksRaycasts = false;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ClickOnButton()
        {
            AudioManager.Instance.PlayClickButton();
        }
    }
}

