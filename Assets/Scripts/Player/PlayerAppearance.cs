using WotN.ScriptableObjects.Items;
using UnityEngine;
using Sirenix.OdinInspector;

namespace WotN.Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        [BoxGroup("Armor slots")]
        [HorizontalGroup("Armor slots/Horizontal")]
        [VerticalGroup("Armor slots/Horizontal/left"), LabelWidth(50)]
        [PreviewField(150)]
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject head;

        [VerticalGroup("Armor slots/Horizontal/left"), LabelWidth(50)]
        [SerializeField]
        [PreviewField(150)]
        [ChildGameObjectsOnly]
        private GameObject chest;

        [SerializeField]
        [VerticalGroup("Armor slots/Horizontal/right"), LabelWidth(50)]
        [PreviewField(150)]
        [ChildGameObjectsOnly]
        private GameObject pants;

        [SerializeField]
        [ChildGameObjectsOnly]
        [PreviewField(150)]
        [VerticalGroup("Armor slots/Horizontal/right"), LabelWidth(50)]
        private GameObject boots;

        [BoxGroup("Weapon rest slots")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform twoHandedWeaponRestSlot;
        [BoxGroup("Weapon rest slots")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform oneHandWeaponRestSlot;
        [BoxGroup("Weapon rest slots")]
        [SerializeField]
        [ChildGameObjectsOnly()]
        private Transform shieldRestSlot;

        [SerializeField]
        [ChildGameObjectsOnly]
        [PreviewField(150, ObjectFieldAlignment.Center)]
        private GameObject[] armorPieces;

        [BoxGroup("Equipped weapons")]
        [SerializeField]
        [ChildGameObjectsOnly]
        [HorizontalGroup("Equipped weapons/Horizontal"), LabelWidth(100)]
        [PreviewField(150)]
        private GameObject equippedWeapon;
        [HorizontalGroup("Equipped weapons/Horizontal"), LabelWidth(100)]
        [SerializeField]
        [ChildGameObjectsOnly]
        [PreviewField(150)]
        private GameObject equippedShield;

        public void ShowBodyPart(ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            switch (itemEquipmentSlot)
            {

                case ItemArmor.EquipmentSlot.Chest:
                    chest.SetActive(true);
                    break;
                case ItemArmor.EquipmentSlot.Head:
                    head.SetActive(true);
                    break;
                case ItemArmor.EquipmentSlot.Legs:
                    pants.SetActive(true);
                    break;
                case ItemArmor.EquipmentSlot.Feet:
                    boots.SetActive(true);
                    break;
            }
        }

        public void HideBodyPart(ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            switch (itemEquipmentSlot)
            {

                case ItemArmor.EquipmentSlot.Chest:
                    chest.SetActive(false);
                    break;
                case ItemArmor.EquipmentSlot.Head:
                    head.SetActive(false);
                    break;
                case ItemArmor.EquipmentSlot.Legs:
                    pants.SetActive(false);
                    break;
                case ItemArmor.EquipmentSlot.Feet:
                    boots.SetActive(false);
                    break;
            }
        }

        public void ShowArmorPiece(ItemArmor armorItem)
        {
            foreach (var armorPiece in armorPieces)
            {

                if (armorPiece.name == armorItem.meshName)
                {
                    armorPiece.SetActive(true);
                    break;
                }

            }
        }

        public void HideArmorPiece(ItemArmor armorItem)
        {
            foreach (var armorPiece in armorPieces)
            {
                if (armorPiece.name == armorItem.meshName)
                {
                    armorPiece.SetActive(false);
                    break;
                }

            }
        }

        public void DetachWeapon(ItemEquipment.EquipmentSlot equipmentSlot)
        {
            if (equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
                Destroy(equippedWeapon);
            else
                Destroy(equippedShield);
        }

        public void SetEquippedWeapon(ItemWeaponBase itemWeapon)
        {
            Transform weaponRestSpot = null;

            if (itemWeapon.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
            {
                var itemWeaponRgihtHand = itemWeapon as ItemWeapon;

                if (itemWeaponRgihtHand.isTwoHandedWeapon)
                {
                    weaponRestSpot = twoHandedWeaponRestSlot;
                }
                else
                {
                    weaponRestSpot = oneHandWeaponRestSlot;
                }

            }
            else if (itemWeapon.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponLeftHand)
            {
                weaponRestSpot = shieldRestSlot;
            }

            var weaponInstance = Instantiate(itemWeapon.weaponModel, weaponRestSpot.position, weaponRestSpot.rotation);


            if (itemWeapon.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponLeftHand)
            {
                equippedShield = weaponInstance;

                var twoHandedWeapon = twoHandedWeaponRestSlot.GetComponentInChildren<MeshRenderer>();

                if (twoHandedWeapon)
                    Destroy(twoHandedWeapon.gameObject);
            }
            else
            {
                equippedWeapon = weaponInstance;
            }

            weaponInstance.transform.SetParent(weaponRestSpot);
        }
    }
}
