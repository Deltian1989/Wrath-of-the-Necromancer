using WotN.ScriptableObjects.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WotN.UI.Inventory;
using WotN.Common.Utils.GamePersistance;
using System;

namespace WotN.Common.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private int space = 54;

        [SerializeField]
        private List<ItemStack> items;

        public event Action<List<ItemStack>> onInventoryInitialized;

        public event Action<List<ItemStack>> onInventoryUpdated;

        public static InventoryManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        public void Add(ItemStack item)
        {
            if (items.Count >= space)
            {
                return;
            }

            items.Add(item);

            onInventoryUpdated?.Invoke(items);
        }

        public void AddItemFromStash(ItemStack itemStack)
        {
            if (items.Count >= space)
            {
                return;
            }

            items.Add(itemStack);

            AudioManager.Instance.PlayUISFX(itemStack.item.useSFX);

            onInventoryUpdated?.Invoke(items);

            StashManager.Instance.RemoveItemFromStash(itemStack);
        }

        public void Remove(ItemStack item)
        {
            if (!items.Remove(item))
            {
                Debug.LogError($"Inventory does not cotain any item named {item.item.itemName}.");
                return;
            };

            onInventoryUpdated?.Invoke(items);
        }

        public void DecreaseStack(ItemStack item, int amount)
        {
            if (!items.Contains(item))
            {
                Debug.LogError($"Inventory does not cotain any item named {item.item.itemName}.");
                return;
            }

            item.count -= amount;

            if (item.count <= 0)
            {
                items.Remove(item);

            }

            onInventoryUpdated?.Invoke(items);

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                items.Clear();
            }

        }

        public void SetInventoryItems(List<SavedInventoryItemData> itemsInInventory)
        {
            foreach (var itemData in itemsInInventory)
            {
                var item = GameManager.Instance.GetItemById<Item>(itemData.itemId);

                items.Add(new ItemStack
                {
                    item = item,
                    count = itemData.count
                });
            }

            onInventoryInitialized?.Invoke(items);
        }
    }
}

