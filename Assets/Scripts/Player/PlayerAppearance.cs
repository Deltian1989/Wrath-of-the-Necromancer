using WotN.ScriptableObjects.Items;
using UnityEngine;

namespace WotN.Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        [Header("Armor slots")]
        [SerializeField]
        private GameObject head;
        [SerializeField]
        private GameObject chest;
        [SerializeField]
        private GameObject pants;
        [SerializeField]
        private GameObject boots;

        [Header("Weapon rest slots")]
        [SerializeField]
        private Transform twoHandedWeaponRestSlot;
        [SerializeField]
        private Transform oneHandWeaponRestSlot;
        [SerializeField]
        private Transform shieldRestSlot;

        [SerializeField]
        private GameObject[] armorPieces;

        [Header("Equipped weapons")]
        [SerializeField]
        private GameObject equippedWeapon;
        [SerializeField]
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
