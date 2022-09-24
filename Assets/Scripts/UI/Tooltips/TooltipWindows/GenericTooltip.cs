using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WotN.ScriptableObjects.Tooltips;

namespace WotN.UI.Tooltips.TooltipWindows
{
    public class GenericTooltip : TooltipBase
    {
        public override void DisplayTooltip(Vector3 uiElementPosition, float horizontalOffset, params string[] tooltipBlocks)
        {
            base.DisplayTooltip(uiElementPosition, horizontalOffset, tooltipBlocks);

            if (tooltipBlocks.Length != 1)
            {
                Debug.LogError("Generic tooltip must contain only one text block");
            }

            var titleSize = SetText(tooltipBlocks[0], title, titleRect);

            var effectiveBackgroundWidth = defaultTooltipWidth > titleRect.sizeDelta.x ? titleRect.sizeDelta.x : defaultTooltipWidth;

            background.sizeDelta = new Vector2(effectiveBackgroundWidth + horizontalPadding, titleSize.y + verticalPadding);

            SetTooltipPosition(uiElementPosition, horizontalOffset);
        }
    }
}

