using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WotN.InputSystem;
using WotN.UI.Common;
using WotN.UI.Options;
using WotN.UI.Stash;

namespace WotN.Common.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager Instance { get; private set; }

        public event Action onPaused;

        public event Action onResumed;

        [SerializeField]
        private HeroMenu menuWindow;

        [SerializeField]
        private StashUI stashUI;

        [SerializeField]
        private CanvasGroup playerHUDCG;

        [SerializeField]
        private CanvasGroup pauseAreaCG;

        [SerializeField]
        private CanvasGroup pauseWindowCG;

        [SerializeField]
        private CanvasGroup optionsWindowCG;

        [SerializeField]
        private CanvasGroup videoSettingsSectionCG;

        [SerializeField]
        private CanvasGroup audioSettingsSectionCG;

        [SerializeField]
        private CanvasGroup controlsWindowCG;

        [SerializeField]
        private VideoOptionsUI videoOptionsUI;

        [SerializeField]
        private AudioOptionsUI audioOptionsUI;

        private AudioManager audioManager;

        private LoadingScreenManager loadingScreenManager;

        private PlayerManager playerManager;

        private CustomizableControls customizableControls;

        void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        void Start()
        {
            audioManager = AudioManager.Instance;
            loadingScreenManager = LoadingScreenManager.Instance;
            playerManager = PlayerManager.Instance;
            customizableControls=ControlsManager.Instance.GetCustomizableControls();
        }

        public void PauseGame(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                if (!playerManager.IsPlayerTalking())
                {
                    if (menuWindow.IsHeroMenuActive() || StashManager.Instance.IsStashOpened)
                    {
                        if (StashManager.Instance.IsStashGoldWindowOpened)
                        {
                            AudioManager.Instance.PlayClickButton();
                            stashUI.CloseStashGoldWindow();
                            return;
                        }
                            

                        if (StashManager.Instance.IsStashOpened)
                            PlayerManager.Instance.UnfocusPlayer();

                        if (menuWindow.IsHeroMenuActive())
                            menuWindow.CloseMenu();
                    }
                    else
                    {
                        audioManager.PlayPauseSFX();
                        if (Time.timeScale == 1)
                        {
                            customizableControls.Disable();

                            Time.timeScale = 0;

                            playerHUDCG.blocksRaycasts = false;
                            playerHUDCG.interactable = false;

                            pauseAreaCG.alpha=1;
                            pauseAreaCG.blocksRaycasts = true;
                            audioManager.PauseBackgroundMusic();
                            onPaused?.Invoke();
                        }
                        else if (Time.timeScale == 0)
                        {

                            ResumeGame();
                        }
                    }
                }
                else
                {
                    playerManager.HandleTalkToNPCWithEscPressed();
                }
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;

            pauseAreaCG.alpha = 0;
            pauseAreaCG.blocksRaycasts = false;

            pauseWindowCG.alpha = 1;
            pauseWindowCG.blocksRaycasts = true;

            optionsWindowCG.alpha = 0;
            optionsWindowCG.blocksRaycasts = false;

            controlsWindowCG.alpha = 0;
            controlsWindowCG.blocksRaycasts = false;

            playerHUDCG.blocksRaycasts = true;
            playerHUDCG.interactable = true;

            customizableControls.Enable();

            audioManager.PlayPauseSFX();
            audioManager.ReplayBackgroundMusic();

            onResumed?.Invoke();
        }

        public void DisplayOptionsWindow()
        {
            pauseWindowCG.alpha = 0;
            pauseWindowCG.blocksRaycasts = false;

            optionsWindowCG.alpha = 1;
            optionsWindowCG.blocksRaycasts = true;
        }

        public void DisplayControlsWindow()
        {
            pauseWindowCG.alpha = 0;
            pauseWindowCG.blocksRaycasts = false;

            controlsWindowCG.alpha = 1;
            controlsWindowCG.blocksRaycasts = true;
        }

        public void CloseOptionsWindow()
        {
            optionsWindowCG.alpha = 0;
            optionsWindowCG.blocksRaycasts = false;

            videoSettingsSectionCG.alpha = 1;
            videoSettingsSectionCG.blocksRaycasts = true;

            audioSettingsSectionCG.alpha = 0;
            audioSettingsSectionCG.blocksRaycasts = false;

            pauseWindowCG.alpha = 1;
            pauseWindowCG.blocksRaycasts = true;
        }

        public void CloseControlsWindow()
        {
            controlsWindowCG.alpha = 0;
            controlsWindowCG.blocksRaycasts = false;

            pauseWindowCG.alpha = 1;
            pauseWindowCG.blocksRaycasts = true;
        }

        public void DisplayVideoSettingsSection()
        {
            videoSettingsSectionCG.alpha = 1;
            videoSettingsSectionCG.blocksRaycasts = true;

            audioSettingsSectionCG.alpha = 0;
            audioSettingsSectionCG.blocksRaycasts = false;
        }

        public void DisplayAudioSettingsSection()
        {
            audioSettingsSectionCG.alpha = 1;
            audioSettingsSectionCG.blocksRaycasts = true;

            videoSettingsSectionCG.alpha = 0;
            videoSettingsSectionCG.blocksRaycasts = false;
        }

        public void UpdateOptionsUI()
        {
            videoOptionsUI.UpdateUI();

            audioOptionsUI.UpdateUI();
        }

        public void ExitGame()
        {
            playerHUDCG.alpha = 1;
            playerHUDCG.blocksRaycasts = true;
            playerHUDCG.interactable = true;

            pauseAreaCG.alpha = 0;
            pauseAreaCG.blocksRaycasts = false;

            GameManager.Instance.SaveGame();

            loadingScreenManager.LoadGameScene(1);
        }
    }
}
