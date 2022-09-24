using UnityEngine;
using UnityEngine.SceneManagement;

namespace WotN.Common.Managers
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance { get; private set; }

        public Texture2D TalkCursorTexture => talkCursorTexture;

        public Texture2D EquipItemCursorTexture => equipItemCursorTexture;

        public Texture2D DrinkPotionCursorTexture => drinkPotionCursorTexture;

        public Texture2D UnequipItemCursorTexture => unequipItemCursorTexture;

        [SerializeField]
        private Texture2D equipItemCursorTexture;

        [SerializeField]
        private Texture2D unequipItemCursorTexture;

        [SerializeField]
        private Texture2D drinkPotionCursorTexture;

        [SerializeField]
        private Texture2D talkCursorTexture;

        [SerializeField]
        private Texture2D setDestinationCursorTexture;

        [SerializeField]
        private Texture2D defaultCursorIcon;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                return;

            SetDefaultCursorTexture();

            ChangeCursorMode();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void SetCursor(Texture2D cursorIcon, bool centerHotSpot = false)
        {
            var hotspot = centerHotSpot ? new Vector2(cursorIcon.width / 2, cursorIcon.height / 2) : Vector2.zero;

            Cursor.SetCursor(cursorIcon, hotspot, CursorMode.Auto);
        }

        public void SetDefaultCursorTexture()
        {
            Cursor.SetCursor(defaultCursorIcon, Vector2.zero, CursorMode.Auto);
        }

        private void ChangeCursorMode()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ChangeCursorMode();
        }
    }
}


