using UnityEngine;
using UnityEngine.UI;
using WotN.Common.Managers;
using UnityEngine.InputSystem;
using WotN.UI.Tooltips.TooltipUIElements;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.UI.Inventory
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private Image countFrame;

        [SerializeField]
        private Text itemCount;

        [SerializeField]
        private Button itemButton;

        [SerializeField]
        private DisplayMode displayMode;

        private ItemStack itemStack;

        private ItemTooltipUIElement itemTooltipUIElement;

        void Start()
        {
            itemTooltipUIElement=GetComponent<ItemTooltipUIElement>();
        }

        public DisplayMode GetDisplayMode()
        {
            return displayMode;
        }

        public ItemStack GetItemStack()
        {
            return itemStack;
        }

        public void AddItem(ItemStack itemStack)
        {
            this.itemStack = itemStack;

            itemIcon.enabled = true;
            itemIcon.sprite = itemStack.item.image;

            itemButton.interactable = true;

            if (itemStack.count > 1)
            {
                countFrame.gameObject.SetActive(true);
                itemCount.text = itemStack.count.ToString();
            }
            else if (itemStack.count == 1)
            {
                countFrame.gameObject.SetActive(false);
            }
        }

        public void ClearSlot()
        {
            itemStack = null;
            itemButton.interactable = false;

            itemIcon.sprite = null;
            itemIcon.enabled = false;

            itemCount.text = null;
            countFrame.gameObject.SetActive(false);
        }

        public void Use()
        {
            if (Keyboard.current.shiftKey.ReadValue() > 0f)
            {
                if (StashManager.Instance.IsStashOpened)
                {
                    if (displayMode == DisplayMode.Inventory)
                        StashManager.Instance.AddItemToStash(itemStack);
                    else if (displayMode == DisplayMode.Stash)
                        InventoryManager.Instance.AddItemFromStash(itemStack);
                }
            }
            else
            {
                itemStack.item.Use();

                AudioManager.Instance.PlayUISFX(itemStack.item.useSFX);
                if (displayMode == DisplayMode.Inventory)
                    InventoryManager.Instance.DecreaseStack(itemStack, 1);
                else if (displayMode == DisplayMode.Stash)
                    StashManager.Instance.RemoveItemFromStash(itemStack);
            }

            itemTooltipUIElement.ResetTooltip(itemStack);
        }
    }
}
