using AutoLayout3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.Common.Utils.GamePersistance;
using WotN.UI.MainMenu.NewHero;

namespace WotN.UI.MainMenu.HeroList
{
    public class HeroListUI : MonoBehaviour
    {
        [Header("Hero list areas")]
        [SerializeField]
        private CanvasGroup heroListCG;

        [SerializeField]
        private CanvasGroup createHeroCG;

        [SerializeField]
        private CanvasGroup newHeroWindowCG;

        [SerializeField]
        private CanvasGroup difficultySelectionWindowCG;

        [Header("Hero list buttons")]
        [SerializeField]
        private Button deleteButton;

        [SerializeField]
        private CanvasGroup deleteHeroWindow;

        [SerializeField]
        private Button continueButton;

        [SerializeField]
        private Button[] difficultyButtons;

        [Header("Hero list settings")]
        [SerializeField]
        private LoadedCharacterCell loadedCharacterCellPrefab;

        [SerializeField]
        private CharacterMinatureRenderer characterMinatureRendererPrefab;

        [SerializeField]
        private GameObject heroListContent;

        [SerializeField]
        private Transform characterMinatureRenderingArea;

        [SerializeField]
        private CharacterMinatureAppearance[] characterClassMinatureModels;

        private List<LoadedCharacterCell> loadedCharacterCells = new List<LoadedCharacterCell>();

        private LoadedCharacterCell focusedLoadedCharacterCell;

        private NewHeroUI newHeroUI;

        void Start()
        {
            var loadedCharacterData = GamePersistenceUtils.LoadAllCharacters();

            foreach (var characterData in loadedCharacterData)
            {
                var characterCell = Instantiate(loadedCharacterCellPrefab, heroListContent.transform);

                loadedCharacterCells.Add(characterCell);

                var heroClassMinature = GetHeroClassMinature(characterData.heroClass);

                var characterRenderer = Instantiate(characterMinatureRendererPrefab, characterMinatureRenderingArea);

                characterCell.SetCharacterData(characterData, characterRenderer, heroClassMinature);

                characterMinatureRenderingArea.GetComponent<XAxisLayoutGroup3D>().UpdateLayout();
            }

            newHeroUI=GetComponent<NewHeroUI>();
        }

        public CharacterMinatureAppearance GetHeroClassMinature(string heroClass)
        {
            foreach (var characterMinature in characterClassMinatureModels)
            {
                if (characterMinature.heroClass == heroClass)
                    return characterMinature;
            }

            return null;
        }

        public void DisplayHeroList()
        {
            heroListCG.alpha = 1;
            heroListCG.blocksRaycasts = true;
        }

        public void SetCharacterData(LoadedCharacterCell loadedCharacterCell)
        {
            if (focusedLoadedCharacterCell != loadedCharacterCell)
            {
                deleteButton.interactable = true;
                continueButton.interactable = true;
                if (focusedLoadedCharacterCell)
                    focusedLoadedCharacterCell.DeselectCharacter();
                focusedLoadedCharacterCell = loadedCharacterCell;
            }
        }

        public void DeleteHero()
        {
            focusedLoadedCharacterCell.DestroyCharacterCell();

            GamePersistenceUtils.DeleteSavedGame(focusedLoadedCharacterCell.GetSavedCharactedData().heroName);

            loadedCharacterCells.Remove(focusedLoadedCharacterCell);

            deleteButton.interactable = false;
            continueButton.interactable = false;

            heroListCG.blocksRaycasts = true;
        }

        public void PopUpDeleteHeroWindow()
        {
            deleteHeroWindow.alpha = 1;
            deleteHeroWindow.blocksRaycasts = true;

            heroListCG.blocksRaycasts = false;
        }

        public void LeaveDeleteHeroWindow()
        {
            deleteHeroWindow.alpha = 0;
            deleteHeroWindow.blocksRaycasts = false;

            heroListCG.blocksRaycasts = true;
        }

        public void LeaveHeroList()
        {
            if (focusedLoadedCharacterCell)
            {
                focusedLoadedCharacterCell.DeselectCharacter();
                focusedLoadedCharacterCell = null;
            }

            heroListCG.alpha = 0;
            heroListCG.blocksRaycasts = false;

            deleteButton.interactable = false;
            continueButton.interactable = false;
        }

        public void DisplayNewHeroArea()
        {
            if(focusedLoadedCharacterCell)
            {
                focusedLoadedCharacterCell.DeselectCharacter();
                focusedLoadedCharacterCell = null;
            }

            heroListCG.alpha = 0;
            heroListCG.blocksRaycasts = false;

            createHeroCG.alpha = 1;
            createHeroCG.blocksRaycasts = true;

            deleteButton.interactable = false;
            continueButton.interactable = false;
        }

        public void LeaveDifficultySelectionWindow()
        {
            difficultySelectionWindowCG.alpha = 0;
            difficultySelectionWindowCG.blocksRaycasts = false;

            if (heroListCG.alpha == 1)
                heroListCG.blocksRaycasts = true;
            else if (newHeroWindowCG.alpha == 1)
                newHeroWindowCG.blocksRaycasts = true;
        }

        public void PopUpDifficultySelectionWindow()
        {
            difficultySelectionWindowCG.alpha = 1;
            difficultySelectionWindowCG.blocksRaycasts = true;

            if (heroListCG.alpha == 1)
                heroListCG.blocksRaycasts = false;
            else if (newHeroWindowCG.alpha == 1)
                newHeroWindowCG.blocksRaycasts = false;

        }

        public void DisableHeroListArea()
        {
            heroListCG.blocksRaycasts = false;
        }

        public void EnableHeroListArea()
        {
            heroListCG.blocksRaycasts = true;
        }

        public void SetUpDifficultySelectionWindow()
        {

            if (focusedLoadedCharacterCell == null)
            {
                difficultyButtons[0].interactable = true;
                difficultyButtons[1].interactable = false;
                difficultyButtons[2].interactable = false;
            }
            else
            {
                for (int i = 0; i < difficultyButtons.Length; i++)
                {
                    if (i < focusedLoadedCharacterCell.GetSavedCharactedData().reachedDifficulty)
                    {
                        difficultyButtons[i].interactable = true;
                    }
                    else
                    {
                        difficultyButtons[i].interactable = false;
                    }

                }
            }
        }

        public void StartGame()
        {
            if (focusedLoadedCharacterCell)
                GameManager.Instance.LoadGame(focusedLoadedCharacterCell.GetSavedCharactedData());
            else
                newHeroUI.StartNewGame();

        }
    }
}
