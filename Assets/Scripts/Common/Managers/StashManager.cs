using System;
using System.Collections.Generic;
using UnityEngine;
using WotN.Common.Utils.GamePersistance;
using WotN.ScriptableObjects.Items;
using WotN.UI.Inventory;

namespace WotN.Common.Managers
{
    public class StashManager : MonoBehaviour
    {
        public static StashManager Instance { get; private set; }

        public bool IsStashOpened { get; private set; }

        public bool IsStashGoldWindowOpened;

        public bool IsWithdrawGoldWindowOpened;

        public event Action onStashOpened;

        public event Action onStashClosed;

        public event Action<List<ItemStack>> onItemsUpdated;

        public event Action<int> onInitializedGold;

        public event Action<int> onStashedGold;

        [SerializeField]
        private int stashedGold;

        [SerializeField]
        private List<ItemStack> stashedItems;

        [SerializeField]
        private int space = 54;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public int GetStashedGold()
        {
            return stashedGold;
        }

        public void AddItemToStash(ItemStack itemStack)
        {
            if (stashedItems.Count >= space)
            {
                return;
            }

            stashedItems.Add(itemStack);

            AudioManager.Instance.PlayUISFX(itemStack.item.useSFX);

            onItemsUpdated?.Invoke(stashedItems);

            InventoryManager.Instance.Remove(itemStack);
        }

        public void RemoveItemFromStash(ItemStack itemStack)
        {
            if (!stashedItems.Remove(itemStack))
            {
                Debug.LogError($"Inventory does not cotain any item named {itemStack.item.itemName}.");
                return;
            };

            onItemsUpdated?.Invoke(stashedItems);
        }

        public void InitializeStash(List<SavedInventoryItemData> itemsInInventory, int gold)
        {
            stashedGold = gold;

            stashedItems = new List<ItemStack>();

            foreach (var itemData in itemsInInventory)
            {
                var item = GameManager.Instance.GetItemById<Item>(itemData.itemId);

                stashedItems.Add(new ItemStack
                {
                    item = item,
                    count = itemData.count
                });
            }

            onInitializedGold?.Invoke(gold);

            onItemsUpdated?.Invoke(stashedItems);
        }

        public void SetStashedGold(int gold)
        {
            stashedGold = gold;

            onStashedGold?.Invoke(stashedGold);
        }

        public void StashGold(int goldToStash)
        {
            stashedGold += goldToStash;

            onStashedGold?.Invoke(stashedGold);
        }

        public void OpenStash()
        {
            IsStashOpened = true;
            onStashOpened?.Invoke();
        }

        public void CloseStash()
        {
            IsStashOpened = false;
            onStashClosed?.Invoke();
        }
    }

}
