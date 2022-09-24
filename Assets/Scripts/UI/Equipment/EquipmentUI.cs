using WotN.Player;
using WotN.ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.Common.Utils;
using WotN.Common.Utils.EventData.Equipment;

namespace WotN.UI.Equipment
{
    public class EquipmentUI : MonoBehaviour
    {
        [Header("Hero header")]
        [SerializeField]
        private Text heroNickname;
        [SerializeField]
        private Text heroHeader;

        [Header("Hero stats")]
        [SerializeField]
        private Text HP;
        [SerializeField]
        private Text MP;
        [SerializeField]
        private Text resist;
        [SerializeField]
        private Text attack;
        [SerializeField]
        private Text armor;
        [SerializeField]
        private Text magicPower;

        [SerializeField]
        private RawImage heroSillhoute;

        [SerializeField]
        private Transform inventoryParent;

        private EquipmentSlot[] equipmentSlots;

        private EquipmentManager equipmentManager;

        private PlayerManager playerManager;

        void Awake()
        {
            equipmentSlots = inventoryParent.GetComponentsInChildren<EquipmentSlot>();
            
        }

        void Start()
        {
            equipmentManager = EquipmentManager.Instance;

            playerManager = PlayerManager.Instance;

            equipmentManager.onEquipmentChanged += UpdateUISlot;
            equipmentManager.onEquipmentInitialized += InitializedEquipmentUI;

            playerManager.onEquipmentDataUpdated += UpdateHeroStatsUI;
            playerManager.onEquipmentDataInitialized += InitializeHeroStatsUI;
        }


        private void UpdateHeroStatsUI(EquipmentUpdateData eventData)
        {
            heroHeader.text = $"Level {eventData.heroLevel}, {eventData.heroClass}";
            HP.text =$"HP: {eventData.HP}" ;
            MP.text = $"MP: {eventData.MP}";
            resist.text = $"Resist: {eventData.resist}";
            attack.text = $"Attack: {eventData.attack}";
            armor.text = $"Armor: {eventData.armor}";
            magicPower.text = $"Magic pwr: {eventData.magicPower}";

        }

        private void InitializeHeroStatsUI(EquipmentInitializeData eventData)
        {
            heroNickname.text = eventData.heroNickname;
            heroHeader.text = $"Level {eventData.heroLevel}, {eventData.heroClass}";
            heroSillhoute.texture = eventData.heroSillhoute;
            HP.text = $"HP: {eventData.HP}";
            MP.text = $"MP: {eventData.MP}";
            resist.text = $"Resist: {eventData.resist}";
            attack.text = $"Attack: {eventData.attack}";
            armor.text = $"Armor: {eventData.armor}";
            magicPower.text = $"Magic pwr: {eventData.magicPower}";

        }

        private void InitializedEquipmentUI(ItemEquipment[] equipmentItems)
        {
            for (int i = 0; i < equipmentItems.Length; i++)
            {
                var newItem = equipmentItems[i];

                if (newItem)
                {
                    equipmentSlots[i].AddItem(newItem);
                }
                else
                {
                    if (i == 5)
                    {
                        var weaponItem = equipmentItems[i - 1] as ItemWeapon;

                        if (weaponItem != null && weaponItem.isTwoHandedWeapon)
                        {
                            equipmentSlots[i].MarkOccupied(weaponItem);

                        }
                        else
                        {
                            equipmentSlots[i].ClearSlot();
                        }
                    }
                    else
                    {
                        equipmentSlots[i].ClearSlot();
                    }
                    
                }
                    
            }
        }

        private void UpdateUISlot(ItemEquipment newItem, ItemEquipment oldItem, ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            if (oldItem)
            {
                int equipmenSlotIndex = (int)itemEquipmentSlot;

                equipmentSlots[equipmenSlotIndex].ClearSlot();

                if (oldItem is ItemWeapon && oldItem.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
                {
                    var itemWeapon = oldItem as ItemWeapon;

                    if (itemWeapon.isTwoHandedWeapon)
                    {
                        equipmentSlots[equipmenSlotIndex + 1].UnmarkOccupied();
                    }
                }
            }

            if (newItem)
            {
                int equipmenSlotIndex = (int)newItem.equipmentSlot;

                equipmentSlots[equipmenSlotIndex].AddItem(newItem);

                if (newItem is ItemWeapon && newItem.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
                {
                    var itemWeapon = newItem as ItemWeapon;

                    if (itemWeapon.isTwoHandedWeapon)
                    {
                        equipmentSlots[equipmenSlotIndex + 1].MarkOccupied(itemWeapon);
                    }
                }
                else if (newItem is ItemShield && newItem.equipmentSlot == ItemEquipment.EquipmentSlot.WeaponLeftHand)
                {
                    var twoHandedWeaponSlot = equipmentSlots[equipmenSlotIndex - 1];

                    var twoHandedWeapon = twoHandedWeaponSlot.GetEquipmentItem() as ItemWeapon;

                    if (twoHandedWeapon && twoHandedWeapon.isTwoHandedWeapon)
                    {
                        twoHandedWeaponSlot.ClearSlot();
                    }
                }
            }
        }
    }
}