using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.MainMenu.NewHero
{
    public class NewHeroUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup createHeroCG;

        [SerializeField]
        private CanvasGroup newHeroWindowCG;

        [SerializeField]
        private InputField heroNameInputField;

        public void StartNewGame()
        {
            if (string.IsNullOrWhiteSpace(heroNameInputField.text))
            {
                Debug.Log("Missing hero name");
                return;
            }

            GameManager.Instance.SaveAndStartNewGame(heroNameInputField.text);
        }

        public void DisplayNewHeroWindow()
        {
            newHeroWindowCG.alpha = 1;
            newHeroWindowCG.blocksRaycasts = true;

            createHeroCG.blocksRaycasts = false;

            heroNameInputField.ActivateInputField();
        }

        public void ExitNewHeroWindow()
        {
            heroNameInputField.text = null;
            heroNameInputField.DeactivateInputField();

            GameManager.Instance.SetHeroClassDeselected();

            newHeroWindowCG.alpha = 0;
            newHeroWindowCG.blocksRaycasts = false;

            createHeroCG.blocksRaycasts = true;
        }

        public void DisplayNewHeroArea()
        {
            createHeroCG.alpha = 1;
            createHeroCG.blocksRaycasts = true;
        }

        public void ExitNewHeroArea()
        {
            createHeroCG.alpha = 0;
            createHeroCG.blocksRaycasts = false;
        }

        public void DisableNewHeroWindow()
        {
            newHeroWindowCG.blocksRaycasts=false;
        }

        public void EnableNewHeroWindow()
        {
            newHeroWindowCG.blocksRaycasts = true;
        }
    }
}
