using UnityEngine;
using WotN.Player;
using WotN.ScriptableObjects.Items;
using WotN.UI.Inventory;

namespace WotN.ScriptableObjects.HeroClass
{
    [CreateAssetMenu(fileName = "New character class", menuName = "Hero class")]
    public class HeroClass : ScriptableObject
    {
        public int ID;

        public string className;

        public string heroClassDescription;

        public Texture heroPortrait;

        public Texture heroSillhoute;

        [Header("Primary skills at start")]
        public int strength;
        public int dexterity;
        public int magic;
        public int vitality;
        public int energy;

        [Header("Effective stats at start")]
        public int damage;
        public int defense;
        public int magicPower;
        public int magicResist;
        public int HP;
        public int MP;
        public int stamina;

        [Header("Start gold")]

        public int startGold;

        public ItemEquipment[] equippedItemsAtStart = new ItemEquipment[6];

        public ItemStack[] inventoryItemsAtStart;

        public PlayerMovement characterPrefab;
    }
}

