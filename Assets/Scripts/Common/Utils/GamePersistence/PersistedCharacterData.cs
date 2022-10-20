using System;
using System.Collections.Generic;
using WotN.Common.Utils.GamePersistence;

namespace WotN.Common.Utils.GamePersistance
{
    [Serializable]
    public class PersistedCharacterData
    {
        public int heroClassID;

        public int currentChapterId = 1;

        public int currentTownStageId = 2;

        public int currentTownId=1;

        public List<GeneratedStagePathData> generatedStagePaths = new List<GeneratedStagePathData>();

        public int currentDifficulty;

        public int reachedDifficulty;

        public string heroName;

        public string heroClass;

        public int currentLevel = 1;

        public int currentExp;

        public int strength;
        public int dexterity;
        public int magic;
        public int vitality;
        public int energy;

        public int damage;
        public int defense;
        public int magicPower;
        public int magicResist;
        public int maxHP;
        public int remainingHP;
        public int maxMP;
        public int remainingMP;
        public int maxStamina;
        public int remianingStamina;

        public int playerGold;

        public SavedEquippmentItemData[] equippedItems = new SavedEquippmentItemData[6];

        public List<SavedInventoryItemData> itemsInInventory;

        public List<SavedInventoryItemData> itemsInStash;

        public int stashedGold;
    }
}
