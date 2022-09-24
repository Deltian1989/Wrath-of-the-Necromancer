using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "New Shield Item", menuName = "Inventory/Weapon Shield")]
    public class ItemShield : ItemWeaponBase
    {
        public int shieldDefense;

        public int magicDefense;

        [Range(0, 1)]
        public float blockChance;
    }
}
