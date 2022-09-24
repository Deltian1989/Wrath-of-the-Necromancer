using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "New Armor Item", menuName = "Inventory/Armor Item")]
    public class ItemArmor : ItemEquipment
    {
        public string meshName;

        public int armor;

        public int magicResist;

        public int requiredLevel;

        public int requiredDexterity;

        public int requiredMagic;
    }
}
