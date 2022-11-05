using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using WotN.Common.Managers;

namespace WotN.UI.Common
{
    public class HeroMenu : MonoBehaviour
    {
        [Header("Hero menu elements")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject heroMenuContents;

        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject[] menuWindows;

        [SerializeField]
        [ChildGameObjectsOnly]
        private RectTransform[] menuButtons;

        [SerializeField]
        private GameObject minimapWindow;

        private int selectedWindowIndex = -1;

        private AudioManager audioManager;

        private CursorManager cursorManager;

        private PlayerManager playerManager;

        void Start()
        {
            audioManager = AudioManager.Instance;
            heroMenuContents.SetActive(false);

            cursorManager = CursorManager.Instance;
            playerManager = PlayerManager.Instance;

            StashManager.Instance.onStashOpened += () => OpenMenuWindow(2);

            StashManager.Instance.onStashClosed += CloseMenu;

            InitializeKeyBindings();
        }

        public void ToggleQuestLog(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (selectedWindowIndex != 0)
                        OpenMenuWindow(0);
                    else
                        CloseMenu();
                }
            }      
        }

        public void ToggleEquipment(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (selectedWindowIndex != 1)
                        OpenMenuWindow(1);
                    else
                        CloseMenu();
                }
            }
                
                
        }

        public void ToggleInventory(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (selectedWindowIndex != 2)
                        OpenMenuWindow(2);
                    else
                        CloseMenu();
                }
            }
                
        }

        public void ToggleStats(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (selectedWindowIndex != 3)
                        OpenMenuWindow(3);
                    else
                        CloseMenu();
                }
            }
        }

        public void ToggleSkills(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (selectedWindowIndex != 4)
                        OpenMenuWindow(4);
                    else
                        CloseMenu();
                }
            }
            

        }

        public void CloseMenu()
        {
            if (selectedWindowIndex == -1)
                return;

            selectedWindowIndex = -1;

            heroMenuContents.SetActive(false);

            cursorManager.SetDefaultCursorTexture();

            audioManager.PlayClickButton();

            minimapWindow.SetActive(true);

            TooltipManager.Instance.HideAllTooltips();
        }

        public bool IsHeroMenuActive()
        {
            return heroMenuContents.gameObject.activeSelf;
        }

        private void InitializeKeyBindings()
        {
            var customizableControls = ControlsManager.Instance.GetCustomizableControls();

            customizableControls.HUD.OpenCloseInventory.started += ToggleInventory;
            customizableControls.HUD.OpenCloseEquipment.started += ToggleEquipment;
            customizableControls.HUD.OpenCloseQuestLog.started += ToggleQuestLog;
            customizableControls.HUD.OpenCloseSkills.started += ToggleSkills;
            customizableControls.HUD.OpenCloseStats.started += ToggleStats;
        }

        private void OpenMenuWindow(int i)
        {
            heroMenuContents.SetActive(true);

            EnlargeMenuButton(i);

            foreach (var menuWindow in menuWindows)
            {
                menuWindow.SetActive(false);
            }

            if (selectedWindowIndex != i)
            {
                audioManager.PlayClickButton();

                selectedWindowIndex = i;
            }

            menuWindows[i].gameObject.SetActive(true);

            minimapWindow.SetActive(false);
        }

        private void EnlargeMenuButton(int i)
        {
            foreach (var menuButton in menuButtons)
            {
                menuButton.sizeDelta = new Vector2(63f, 83f);
                menuButton.anchoredPosition = new Vector2(5.5f, 0f);
            }

            menuButtons[i].sizeDelta = new Vector2(74f, 95f);
            menuButtons[i].anchoredPosition = new Vector2(0f, 0f);
        }
    }
}
