using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using WotN.Common.Managers;
using WotN.UI.Utils;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.UI.Tooltips.TooltipUIElements
{
    public class GoldTooltipUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float tooltipHorizontalOffset = 0;

        [SerializeField]
        private int tooltipId;

        [SerializeField]
        private DisplayMode currentDisplayMode;

        [SerializeField]
        private UIElementType uiElementType;

        public void OnPointerEnter(PointerEventData eventData)
        {
            DisplayMode displayMode= currentDisplayMode;

            if (currentDisplayMode == DisplayMode.Inventory)
            {
                if (StashManager.Instance.IsStashOpened)
                    displayMode = DisplayMode.InventoryToStash;
            }         

            TooltipManager.Instance.DisplayTooltipWithShortcutKeyTip(transform.position, tooltipHorizontalOffset, tooltipId, displayMode, uiElementType);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideAllTooltips();
        }
    }
}

