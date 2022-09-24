using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace WotN.Common.Managers
{
    public class VideoOptionsManager : MonoBehaviour
    {
        public static VideoOptionsManager Instance { get; private set; }

        private Resolution[] availableScreenResolutions;

        private Dictionary<GraphicsTier, string> availableGraphicsTiers;

        private int currentScreenResolutionIndex;

        private int currentGraphicsTierIndex;

        private int currentFullScreenIndex;

        private int defaultScreenResolutionIndex;

        private int defaultGraphicsTierIndex;

        private int defaultFullScreenIndex = 1;

        private const string currentScreenResolutionIndexKeyName = "currentScreenResolutionIndex";
        private const string currentGraphicsTierKeyName = "currentGraphicsTier";
        private const string currentFullScreenIndexKeyName = "currentFullScreenIndex";

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                return;

            availableScreenResolutions = Screen.resolutions;

            availableGraphicsTiers = new Dictionary<GraphicsTier, string>
        {
            { GraphicsTier.Tier1, "Low Quality" },
            { GraphicsTier.Tier2, "Medium Quality" },
            { GraphicsTier.Tier3, "High Quality" }
        };

            currentScreenResolutionIndex = PlayerPrefs.GetInt(currentScreenResolutionIndexKeyName, defaultScreenResolutionIndex);
            currentGraphicsTierIndex = PlayerPrefs.GetInt(currentGraphicsTierKeyName, defaultGraphicsTierIndex);
            currentFullScreenIndex = PlayerPrefs.GetInt(currentFullScreenIndexKeyName, defaultFullScreenIndex);
        }

        public string GetCurrentScreenResolutionText()
        {
            var resolution = availableScreenResolutions[currentScreenResolutionIndex];

            return $"{resolution.width}x{resolution.height}";
        }

        public string GetCurrentGraphicsTierText()
        {
            var resolution = availableGraphicsTiers[(GraphicsTier)currentGraphicsTierIndex];

            return resolution;
        }

        public string GetScreenResolutionTextFromValue(int value)
        {
            var resolution = availableScreenResolutions[value];

            return $"{resolution.width}x{resolution.height}";
        }

        public string GetGraphicsTierTextFromValue(GraphicsTier value)
        {
            var graphicsTier = availableGraphicsTiers[value];

            return graphicsTier;
        }

        public void SetCurrentGraphicsTier(GraphicsTier value)
        {
            currentGraphicsTierIndex = (int)value;
        }

        public void SetCurrentScreenResolution(int value)
        {
            currentScreenResolutionIndex = value;
        }

        public void SetCurrentFullScreenMode(int value)
        {
            currentFullScreenIndex = value;
        }

        public int GetGraphicsTierMaxValue()
        {
            return availableGraphicsTiers.Count - 1;
        }

        public int GetScreenResolutionMaxValue()
        {
            return availableScreenResolutions.Length - 1;
        }

        public int GetCurrentGraphicsTierValue()
        {
            return currentGraphicsTierIndex;
        }

        public int GetCurrentFullScreenValue()
        {
            return currentFullScreenIndex;
        }

        public int GetCurrentScreenResolutionValue()
        {
            return currentScreenResolutionIndex;
        }

        public bool AreOptionsSetToDefaultValues()
        {
            var savedScreenResolutionIndex = PlayerPrefs.GetInt(currentScreenResolutionIndexKeyName, defaultScreenResolutionIndex);
            var savedGraphicsTierIndex = PlayerPrefs.GetInt(currentGraphicsTierKeyName, defaultGraphicsTierIndex);
            var savedFullScreenIndexIndex = PlayerPrefs.GetInt(currentFullScreenIndexKeyName, defaultFullScreenIndex);

            return defaultScreenResolutionIndex == savedScreenResolutionIndex && defaultGraphicsTierIndex == savedGraphicsTierIndex && savedFullScreenIndexIndex == defaultFullScreenIndex;
        }

        public bool AreSelectedOptionsSameAsSaved()
        {
            var savedScreenResolutionIndex = PlayerPrefs.GetInt(currentScreenResolutionIndexKeyName, defaultScreenResolutionIndex);
            var savedGraphicsTierIndex = PlayerPrefs.GetInt(currentGraphicsTierKeyName, defaultGraphicsTierIndex);
            var savedFullScreenIndexIndex = PlayerPrefs.GetInt(currentFullScreenIndexKeyName, defaultFullScreenIndex);

            return savedScreenResolutionIndex == currentScreenResolutionIndex && savedGraphicsTierIndex == currentGraphicsTierIndex && savedFullScreenIndexIndex == currentFullScreenIndex;
        }

        public void SaveOptions()
        {
            PlayerPrefs.SetInt(currentScreenResolutionIndexKeyName, currentScreenResolutionIndex);
            PlayerPrefs.SetInt(currentGraphicsTierKeyName, currentGraphicsTierIndex);
            PlayerPrefs.SetInt(currentFullScreenIndexKeyName, currentFullScreenIndex);
        }

        public void ResetOptions()
        {
            PlayerPrefs.SetInt(currentScreenResolutionIndexKeyName, defaultScreenResolutionIndex);
            PlayerPrefs.SetInt(currentGraphicsTierKeyName, defaultGraphicsTierIndex);
            PlayerPrefs.SetInt(currentFullScreenIndexKeyName, defaultFullScreenIndex);

            currentGraphicsTierIndex = defaultGraphicsTierIndex;
            currentScreenResolutionIndex = defaultScreenResolutionIndex;
            currentFullScreenIndex = defaultFullScreenIndex;
        }

        public void DiscardChanges()
        {
            currentScreenResolutionIndex = PlayerPrefs.GetInt(currentScreenResolutionIndexKeyName, defaultScreenResolutionIndex);
            currentGraphicsTierIndex = PlayerPrefs.GetInt(currentGraphicsTierKeyName, defaultGraphicsTierIndex);
            currentFullScreenIndex = PlayerPrefs.GetInt(currentFullScreenIndexKeyName, defaultFullScreenIndex);
        }
    }
}
