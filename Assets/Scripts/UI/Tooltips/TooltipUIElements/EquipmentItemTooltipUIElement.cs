using UnityEngine;
using UnityEngine.EventSystems;
using WotN.Common.Managers;
using WotN.ScriptableObjects.Items;
using WotN.UI.Equipment;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.UI.Tooltips.TooltipUIElements
{
    public class EquipmentItemTooltipUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float tooltipHorizontalOffset = 0;

        private EquipmentSlot equipmentSlot;

        void Start()
        {
            equipmentSlot = GetComponent<EquipmentSlot>();
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
            var equipmentItem = equipmentSlot.GetEquipmentItem();

            if (!TooltipManager.Instance.IsItemTooltipActive() && (equipmentItem != null && (equipmentItem is ItemWeaponBase || equipmentItem is ItemArmor)))
            {
                DisplayTooltipForItem();
            }
        }

        public void ResetTooltip(ItemEquipment equipmentItem)
        {
            if (TooltipManager.Instance.IsItemTooltipActive())
            {
                if (equipmentItem == null)
                {
                    TooltipManager.Instance.HideAllTooltips();

                    CursorManager.Instance.SetDefaultCursorTexture();
                }
                else if (equipmentItem is ItemWeaponBase || equipmentItem is ItemArmor)
                {
                    DisplayTooltipForItem();
                }
            }
        }

        private void DisplayTooltipForItem()
        {
            var equipmentItem = equipmentSlot.GetEquipmentItem();

            DisplayMode displayMode;

            if (StashManager.Instance.IsStashOpened)
                displayMode = DisplayMode.EquipmentToStash;
            else
                displayMode = DisplayMode.Equipment;

            TooltipManager.Instance.DisplayTooltipForItem(transform.position, tooltipHorizontalOffset, equipmentItem, displayMode, UIElementType.EquipmentItem);

            CursorManager.Instance.SetCursor(CursorManager.Instance.UnequipItemCursorTexture);
        }

    }
}