using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Weapon Item")]
    public class ItemWeapon : ItemWeaponBase
    {
        public enum AttackSpeed
        {
            VerySlow,
            Slow,
            Normal,
            Fast,
            VeryFast
        }

        public int minDamage;

        public int maxDamage;

        public AttackSpeed attackSpeed;

        public int magicDamage;

        public bool isTwoHandedWeapon;
    }
}
