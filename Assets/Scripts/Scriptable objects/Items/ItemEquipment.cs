using WotN.Common.Managers;
using WotN.UI.Equipment;

namespace WotN.ScriptableObjects.Items
{
    public class ItemEquipment : Item
    {
        public enum EquipmentSlot
        {
            Head,
            Chest,
            Legs,
            Feet,
            WeaponRightHand,
            WeaponLeftHand
        }

        public EquipmentSlot equipmentSlot;

        public override void Use()
        {
            EquipmentManager.Instance.Equip(this);
        }
    }
}
