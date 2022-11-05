using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Player;
using WotN.ScriptableObjects.Items;
using WotN.UI.Inventory;

namespace WotN.ScriptableObjects.HeroClass
{
    [CreateAssetMenu(fileName = "New character class", menuName = "Hero class")]
    public class HeroClass : ScriptableObject
    {
        [BoxGroup("Basic info")]
        [LabelWidth(140)]
        public int ID;
        [BoxGroup("Basic info")]
        [LabelWidth(140)]
        public string className;
        [BoxGroup("Basic info")]
        [LabelWidth(140)]
        public string heroClassDescription;

        [HorizontalGroup("Hero's graphics", 150)]
        [PreviewField(150)]
        [HideLabel]
        [AssetsOnly]
        public GameObject characterPrefab;

        [HorizontalGroup("Hero's graphics", 150)]
        [PreviewField(150)]
        [HideLabel]
        [AssetsOnly]
        public Texture heroPortrait;

        [HorizontalGroup("Hero's graphics", 150)]
        [PreviewField(150)]
        [HideLabel]
        [AssetsOnly]
        public Texture heroSillhoute;

        [TabGroup("Initial primary skills")]
        public int strength;
        [TabGroup("Initial primary skills")]
        public int dexterity;
        [TabGroup("Initial primary skills")]
        public int magic;
        [TabGroup("Initial primary skills")]
        public int vitality;
        [TabGroup("Initial primary skills")]
        public int energy;

        [TabGroup("Effective stats")]
        public int damage;
        [TabGroup("Effective stats")]
        public int defense;
        [TabGroup("Effective stats")]
        public int magicPower;
        [TabGroup("Effective stats")]
        public int magicResist;
        [TabGroup("Effective stats")]
        public int HP;
        [TabGroup("Effective stats")]
        public int MP;
        [TabGroup("Effective stats")]
        public int stamina;

        [TabGroup("Owned items & gold")]
        public ItemEquipment[] initialEquippedItems = new ItemEquipment[6];
        [TabGroup("Owned items & gold")]
        public ItemStack[] initialInventoryItems;
        [TabGroup("Owned items & gold")]
        public int startGold;
    }
}

