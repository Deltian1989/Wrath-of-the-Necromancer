using UnityEngine;
using UnityEngine.EventSystems;
using WotN.Common.Managers;
using WotN.ScriptableObjects.Items;
using WotN.UI.Inventory;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.UI.Tooltips.TooltipUIElements
{
    public class ItemTooltipUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float tooltipHorizontalOffset = 0;

        private ItemSlot itemSlot;

        void Start()
        {
            itemSlot=GetComponent<ItemSlot>();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (TooltipManager.Instance.IsItemTooltipActive())
            {
                TooltipManager.Instance.HideAllTooltips();
            }

            CursorManager.Instance.SetDefaultCursorTexture();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemStack itemStack = itemSlot.GetItemStack();

            if (!TooltipManager.Instance.IsItemTooltipActive() && itemStack != null)
            {
                DisplayTooltipForItem();
            }
        }

        public void ResetTooltip(ItemStack itemStack)
        {
            if (TooltipManager.Instance.IsItemTooltipActive())
            {
                if (itemStack == null)
                {
                    TooltipManager.Instance.HideAllTooltips();

                    CursorManager.Instance.SetDefaultCursorTexture();
                }
                else
                {
                    DisplayTooltipForItem();
                }
            }
        }

        private void DisplayTooltipForItem()
        {
            ItemStack itemStack = itemSlot.GetItemStack();

            DisplayMode displayMode =itemSlot.GetDisplayMode();

            UIElementType uiElementType;

            if (itemStack.item is ItemEquipment)
                uiElementType = UIElementType.EquipmentItem;
            else
                uiElementType = UIElementType.NormalItem;

            if (displayMode != DisplayMode.Stash)
            {
                if (StashManager.Instance.IsStashOpened)
                    displayMode = DisplayMode.InventoryToStash;
                else
                    displayMode = DisplayMode.Inventory;
            }

            TooltipManager.Instance.DisplayTooltipForItem(transform.position, tooltipHorizontalOffset, itemStack.item, displayMode, uiElementType);
        }
    }
}

