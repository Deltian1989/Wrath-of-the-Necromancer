using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.Common.Utils.GamePersistance;

namespace WotN.UI.MainMenu.HeroList
{
    public class LoadedCharacterCell : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private Text heroName;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Text heroLevel;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Text heroClass;

        [SerializeField]
        private RawImage characterRendererImage;

        private PersistedCharacterData characterData;

        private HeroListUI heroListUI;

        private CharacterMinatureRenderer characterRenderer;

        private Image selectionFrame;

        void Start()
        {
            heroListUI = FindObjectOfType<HeroListUI>();
            selectionFrame = GetComponent<Image>();
        }

        public void SetCharacterData(PersistedCharacterData characterData, CharacterMinatureRenderer characterRenderer, CharacterMinatureAppearance characterMinatureAppearance)
        {
            RenderTexture characterRenderTexture = new RenderTexture(500, 500, 0, RenderTextureFormat.ARGB32);

            characterRenderer.RenderCharacter(characterRenderTexture, characterData, characterMinatureAppearance);

            this.characterData =characterData;
            heroName.text = characterData.heroName;
            heroLevel.text = $"Level {characterData.currentLevel}";
            heroClass.text = characterData.heroClass;
            this.characterRendererImage.texture= characterRenderTexture;
            this.characterRenderer = characterRenderer;
        }

        public void OnCharacterSelected()
        {
            selectionFrame.enabled = true;
            heroListUI.SetCharacterData(this);
        }

        public void DeselectCharacter()
        {
            selectionFrame.enabled = false;
        }

        public void DestroyCharacterCell()
        {
            characterRenderer.DestroyMinatureRenderer();
            Destroy(gameObject);
        }

        public PersistedCharacterData GetSavedCharactedData()
        {
            return characterData;
        }
    }
}

