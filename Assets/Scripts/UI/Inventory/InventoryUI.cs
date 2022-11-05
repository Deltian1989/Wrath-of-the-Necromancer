using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.Common.Utils.EventData.Inventory;

namespace WotN.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private Text playerGold;

        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject inventoryButton;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform inventoryParent;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Scrollbar scrollbar;

        [SerializeField]
        private float scrollingStep = 0.25f;

        private AudioManager audioManager;

        private InventoryManager inventoryManager;

        private ItemSlot[] itemSlots;

        void Start()
        {
            inventoryManager = InventoryManager.Instance;

            inventoryManager.onInventoryInitialized += InitializeInventoryUI;
            inventoryManager.onInventoryUpdated += OnItemsUpdated;

            PlayerManager.Instance.onInventoryInitialized += InitializeInventoryData;
            PlayerManager.Instance.onInventoryDataUpdated += UpdateInventoryData;

            audioManager = AudioManager.Instance;

            itemSlots = inventoryParent.GetComponentsInChildren<ItemSlot>();
        }

        private void InitializeInventoryUI(List<ItemStack> items)
        {

            UpdateItemsOnUI(items);

        }

        private void OnItemsUpdated(List<ItemStack> itemStacks)
        {
            UpdateItemsOnUI(itemStacks);
        }

        private void UpdateItemsOnUI(List<ItemStack> items)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (i < items.Count)
                {
                    itemSlots[i].AddItem(items[i]);
                }
                else
                {
                    itemSlots[i].ClearSlot();
                }
            }
        }

        private void InitializeInventoryData(InventoryInitializeData eventData)
        {
            playerGold.text = eventData.currentGold.ToString();
        }

        private void UpdateInventoryData(InventoryUpdateData eventData)
        {
            playerGold.text = eventData.currentGold.ToString();
        }

        public void ScrollDown()
        {
            scrollbar.value = Mathf.Clamp(scrollbar.value -= scrollingStep, 0, 1);

            audioManager.PlayMouseOverButton();
        }

        public void ScrollUp()
        {
            scrollbar.value = Mathf.Clamp(scrollbar.value += scrollingStep, 0, 1);

            audioManager.PlayMouseOverButton();
        }
    }
}