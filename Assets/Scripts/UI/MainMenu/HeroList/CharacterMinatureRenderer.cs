using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Common.Managers;
using WotN.Common.Utils.GamePersistance;

namespace WotN.UI.MainMenu.HeroList
{
    public class CharacterMinatureRenderer : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private Camera renderingCamera;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform renderedCharacterSpot;

        [SerializeField]
        [ChildGameObjectsOnly]
        private CharacterMinatureAppearance renderedCharacterMinature;

        public void RenderCharacter(RenderTexture renderTexture, PersistedCharacterData characterData, CharacterMinatureAppearance characterMinatureModel)
        {
            renderingCamera.targetTexture = renderTexture;

            if (characterMinatureModel == null)
            {
                Debug.LogError("Missing character minature model for hero class " + characterData.heroClass);
                return;
            }

            CharacterMinatureAppearance characterMinatureInstance = Instantiate(characterMinatureModel, renderedCharacterSpot.position, Quaternion.Euler(0,180,0),renderedCharacterSpot);

            characterMinatureInstance.ShowEquipmentParts(characterData.equippedItems);

            renderedCharacterMinature = characterMinatureInstance;
        }

        public void DestroyMinatureRenderer()
        {
            Destroy(gameObject);
        }
    }
}

