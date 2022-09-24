using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.Common.Utils.EventData.AvatarPanel;
using WotN.Common.Utils.EventData.SkillPanel;

namespace WotN.UI.Common
{
    public class UIHandler : MonoBehaviour
    {
        [Header("Avatar fields")]
        [SerializeField]
        private RawImage avatarHeroPortrait;
        [SerializeField]
        private Text avatarLevelIcon;
        [SerializeField]
        private Text avatarName;
        [SerializeField]
        private Image hpLine;
        [SerializeField]
        private Image mpLine;

        [Header("Skills panel")]
        [SerializeField]
        private Image staminaBar;
        [SerializeField]
        private Image expBar;
        [SerializeField]
        private Text levelIcon;

        [SerializeField]
        private GameObject stashWindow;

        [SerializeField]
        private GameObject avatarPanel;

        private PauseManager pauseManager;

        private AudioManager audioManager;

        void Start()
        {
            pauseManager = PauseManager.Instance;

            audioManager = AudioManager.Instance;

            PlayerManager.Instance.onAvatarPanelDataInitialized += InitializeAvatarPanel;

            PlayerManager.Instance.onAvatarPanelDataUpdated += UpdateAvatarPanel;

            PlayerManager.Instance.onSkillPanelDataInitialized += InitializeSkillPanel;

            PlayerManager.Instance.onSkillPanelDataUpdated += UpdateSkillPanel;

            StashManager.Instance.onStashOpened += OpenStashWindow;

            StashManager.Instance.onStashClosed+= CloseStashWindow;
        }

        private void InitializeAvatarPanel(AvatarPanelInitializeData eventData)
        {
            avatarHeroPortrait.texture = eventData.heroPortrait;
            avatarLevelIcon.text = eventData.heroLevel.ToString();
            avatarName.text = eventData.heroName.ToString();
            hpLine.fillAmount = eventData.remainingHP / eventData.maxHP;
            mpLine.fillAmount = eventData.remainingMP / eventData.maxMP;
        }

        private void UpdateAvatarPanel(AvatarPanelUpdateData eventData)
        {
            avatarLevelIcon.text = eventData.heroLevel.ToString();
            hpLine.fillAmount = eventData.remainingHP / eventData.maxHP;
            mpLine.fillAmount = eventData.remainingMP / eventData.maxMP;
        }

        private void InitializeSkillPanel(SkillPanelInitializeData eventData)
        {
            staminaBar.fillAmount = eventData.remainingStamina / eventData.maxStamina;
            expBar.fillAmount = eventData.currentExp / eventData.nextLevelExp;
            levelIcon.text = eventData.heroLevel.ToString();
        }

        private void UpdateSkillPanel(SkillPanelUpdateData eventData)
        {
            staminaBar.fillAmount = eventData.remainingStamina / eventData.maxStamina;
            expBar.fillAmount = eventData.currentExp / eventData.nextLevelExp;
            levelIcon.text = eventData.heroLevel.ToString();
        }

        private void OpenStashWindow()
        {
            stashWindow.SetActive(true);

            avatarPanel.SetActive(false);
        }

        private void CloseStashWindow()
        {
            stashWindow.SetActive(false);

            avatarPanel.SetActive(true);
        }

        public void HoverOverButton()
        {
            audioManager.PlayMouseOverButton();
        }

        public void ClickOnButton()
        {
            audioManager.PlayClickButton();
        }

        public void ResumeGame()
        {
            pauseManager.ResumeGame();
        }

        public void DisplayOptionsWindow()
        {
            pauseManager.DisplayOptionsWindow();
        }

        public void DisplayControlsWindow()
        {
            pauseManager.DisplayControlsWindow();
        }

        public void CloseOptionsWindow()
        {
            pauseManager.CloseOptionsWindow();
        }

        public void CloseControlsWindow()
        {
            pauseManager.CloseControlsWindow();
        }

        public void DisplayVideoSettingsSection()
        {
            pauseManager.DisplayVideoSettingsSection();
        }

        public void DisplayAudioSettingsSection()
        {
            pauseManager.DisplayAudioSettingsSection();
        }

        public void ExitGame()
        {
            pauseManager.ExitGame();
        }
    }
}
