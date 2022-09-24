using WotN.ScriptableObjects.Items;
using WotN.UI.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;
using WotN.Common.Utils;
using System;
using WotN.Common.Utils.GamePersistance;
using static WotN.ScriptableObjects.Items.ItemEquipment;

namespace WotN.Common.Managers
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager Instance { get; private set; }

        public delegate void OnEquipmentChanged(ItemEquipment newItem, ItemEquipment oldItem, EquipmentSlot equipmentSlot);

        public event OnEquipmentChanged onEquipmentChanged;

        public event Action<ItemEquipment[]> onEquipmentInitialized;

        [SerializeField]
        private ItemEquipment[] equippedItems = new ItemEquipment[6];

        private InventoryManager inventoryManager;

        void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void Start()
        {
            inventoryManager = InventoryManager.Instance;
        }

        public void Equip(ItemEquipment newItem)
        {

            ItemEquipment oldItem = null;

            int slotIndex = (int)newItem.equipmentSlot;

            if (equippedItems[slotIndex] != null)
            {
                oldItem = equippedItems[slotIndex];

                inventoryManager.Add(new ItemStack { item = oldItem, count = 1 });
            }

            equippedItems[slotIndex] = newItem;

            if (newItem is ItemArmor && slotIndex >= 0 && slotIndex <= 3)
            {
                var armorItem = newItem as ItemArmor;
            }
            else if (newItem is ItemWeaponBase && slotIndex >= 4 && slotIndex <= 5)
            {
                if (newItem is ItemWeapon && slotIndex == 4)
                {
                    var itemWeaponTwoHanded = newItem as ItemWeapon;

                    if (itemWeaponTwoHanded.isTwoHandedWeapon)
                    {
                        var shieldToRemove = equippedItems[slotIndex + 1];

                        if (shieldToRemove)
                        {
                            equippedItems[slotIndex + 1] = null;

                            inventoryManager.Add(new ItemStack { item = shieldToRemove, count = 1 });

                            onEquipmentChanged?.Invoke(null, shieldToRemove, (EquipmentSlot)slotIndex+1);
                        }

                    }
                }
                else if (newItem is ItemShield && slotIndex == 5)
                {
                    var itemWeaponTwoHanded = equippedItems[slotIndex - 1] as ItemWeapon;

                    if (itemWeaponTwoHanded && itemWeaponTwoHanded.isTwoHandedWeapon)
                    {
                        equippedItems[slotIndex - 1] = null;

                        inventoryManager.Add(new ItemStack { item = itemWeaponTwoHanded, count = 1 });
                    }
                }

            }

            onEquipmentChanged?.Invoke(newItem, oldItem, (EquipmentSlot)slotIndex);
        }

        public void SetEquipmentItems(SavedEquippmentItemData[] equippedItemsData)
        {
            for (int i = 0; i < equippedItemsData.Length; i++)
            {
                if (equippedItemsData[i] != null)
                {
                    var equipmentItem = GameManager.Instance.GetItemById<ItemEquipment>(equippedItemsData[i].itemId);

                    equippedItems[i] = equipmentItem;
                }
                else
                {
                    equippedItems[i] = null;
                }
                
            }

            onEquipmentInitialized?.Invoke(equippedItems);
        }

        public void UnequipItem(ItemEquipment.EquipmentSlot itemSlot)
        {
            int slotIndex = (int)itemSlot;

            var oldItem = equippedItems[slotIndex];

            if (!oldItem)
            {
                Debug.LogError($"Any item on item slot {itemSlot} is not equipped.");
                return;
            }

            equippedItems[slotIndex] = null;

            onEquipmentChanged?.Invoke(null, oldItem, itemSlot);

            inventoryManager.Add(new ItemStack { item = oldItem, count = 1 });
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                for (int i = 0; i < equippedItems.Length; i++)
                {
                    equippedItems[i] = null;
                }
            }

        }
    }
}

