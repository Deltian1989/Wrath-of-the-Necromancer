using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    public class ItemWeaponBase : ItemEquipment
    {
        public GameObject weaponModel;

        public int requiredLevel;

        public int requiredStrength;

        public int requiredDexterity;

        public int requiredMagic;
    }
}
