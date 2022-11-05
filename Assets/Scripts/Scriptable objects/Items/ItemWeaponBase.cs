using Sirenix.OdinInspector;
using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    public class ItemWeaponBase : ItemEquipment
    {
        [PreviewField(150)]
        [HideLabel]
        [HorizontalGroup("Graphics", 150)]
        [AssetsOnly]
        public GameObject weaponModel;

        [MaxValue(99)]
        public int requiredLevel;

        public int requiredStrength;

        public int requiredDexterity;

        public int requiredMagic;
    }
}
