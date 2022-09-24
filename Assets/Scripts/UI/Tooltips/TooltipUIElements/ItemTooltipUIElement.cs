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
        protected float tooltipHorizontalOffset = 0;

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

            if (!TooltipManager.Instance.IsItemTooltipActive() && (itemStack != null && (itemStack.item is ItemWeaponBase || itemStack.item is ItemArmor)))
            {
                DisplayTooltipForItem();
            }
            else if (!TooltipManager.Instance.IsItemTooltipActive() && (itemStack != null && (itemStack.item is Item)))
            {
                CursorManager.Instance.SetCursor(CursorManager.Instance.DrinkPotionCursorTexture);
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
                else if (itemStack.item is ItemWeaponBase || itemStack.item is ItemArmor)
                {
                    DisplayTooltipForItem();
                }
                else
                {
                    TooltipManager.Instance.HideAllTooltips();

                    CursorManager.Instance.SetCursor(CursorManager.Instance.DrinkPotionCursorTexture);
                }
            }
        }

        private void DisplayTooltipForItem()
        {
            ItemStack itemStack = itemSlot.GetItemStack();

            DisplayMode displayMode;

            if (StashManager.Instance.IsStashOpened)
                displayMode = DisplayMode.InventoryToStash;
            else
                displayMode = itemSlot.GetDisplayMode();

            TooltipManager.Instance.DisplayTooltipForItem(transform.position, tooltipHorizontalOffset, itemStack.item, displayMode);

            CursorManager.Instance.SetCursor(CursorManager.Instance.EquipItemCursorTexture);
        }
    }
}

