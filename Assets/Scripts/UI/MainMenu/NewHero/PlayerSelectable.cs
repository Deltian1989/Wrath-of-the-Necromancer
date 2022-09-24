using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.ScriptableObjects.HeroClass;

namespace WotN.UI.MainMenu.NewHero
{
    public class PlayerSelectable : MonoBehaviour
    {
        [SerializeField]
        private HeroClass heroClassSO;

        [SerializeField]
        private Text heroClassNameText;

        [SerializeField]
        private Text heroClassDescriptionText;

        public void SetHeroClassSelected()
        {
            GameManager.Instance.SetHeroClassSelected(heroClassSO);
        }

        public void OnPlayerRendererEnter(Button button)
        {
            if (button.interactable)
            {
                heroClassNameText.text = heroClassSO.className;
                heroClassDescriptionText.text = heroClassSO.heroClassDescription;
            }

        }

        public void OnPlayerRendererExit(Button button)
        {
            if (button.interactable)
            {
                heroClassNameText.text = null;
                heroClassDescriptionText.text = null;
            }
        }
    }
}
