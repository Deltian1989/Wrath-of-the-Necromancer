using WotN.ScriptableObjects.Items;
using System.Text;
using TMPro;
using UnityEngine;
using static WotN.ScriptableObjects.Items.ItemWeapon;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace WotN.UI.Tooltips.TooltipWindows
{
    public class ItemTooltip : TooltipBase
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private TMP_Text itemDescription;

        [SerializeField]
        [ChildGameObjectsOnly]
        private TMP_Text itemShortcutTip;

        [SerializeField]
        private float minTooltipWidth = 328.54f;

        [SerializeField]
        private float maxTooltipWidth = 420f;

        private float currentTitleWidth;

        private RectTransform itemDescriptionRect;

        private RectTransform itemShortcutTipRect;

        public override void DisplayTooltip(Vector3 uiElementPosition, float horizontalOffset, params string[] tooltipBlocks)
        {
            base.DisplayTooltip(uiElementPosition, horizontalOffset, tooltipBlocks);

            if (!itemDescriptionRect)
                itemDescriptionRect = itemDescription.GetComponent<RectTransform>();

            if (!itemShortcutTipRect)
                itemShortcutTipRect = itemShortcutTip.GetComponent<RectTransform>();

            if (tooltipBlocks.Length != 3)
            {
                Debug.LogError("Generic tooltip must contain only one text block");
            }

            var titleSize = SetText(tooltipBlocks[0], title, titleRect);

            var descriptionSize = SetTextForTextBlock(tooltipBlocks[1], itemDescription, itemDescriptionRect);

            var itemShortcutTipSize = SetTextForTextBlock(tooltipBlocks[2], itemShortcutTip, itemShortcutTipRect);

            var effectiveBackgroundWidth = titleRect.sizeDelta.x > itemDescriptionRect.sizeDelta.x ? titleRect.sizeDelta.x : itemDescriptionRect.sizeDelta.x;

            effectiveBackgroundWidth = itemShortcutTipSize.x > effectiveBackgroundWidth ? itemShortcutTipSize.x : effectiveBackgroundWidth;

            titleRect.sizeDelta = new Vector2(effectiveBackgroundWidth, titleRect.sizeDelta.y);

            itemDescriptionRect.sizeDelta = new Vector2(effectiveBackgroundWidth, itemDescriptionRect.sizeDelta.y);

            background.sizeDelta = new Vector2(effectiveBackgroundWidth + horizontalPadding, titleSize.y + descriptionSize.y+ itemShortcutTipSize.y+ verticalPadding);
            SetTooltipPosition(uiElementPosition, horizontalOffset);
        }

        protected override Vector2 SetText(string text, TMP_Text textContainer, RectTransform textRect)
        {
            textRect.sizeDelta = new Vector2(maxTooltipWidth, textRect.sizeDelta.y);

            textContainer.SetText(text);

            textContainer.ForceMeshUpdate();

            Vector2 textContainerSize = textContainer.GetRenderedValues(false);

            currentTitleWidth = textContainerSize.x;

            float effectiveTextBlockWidth = textContainerSize.x <= minTooltipWidth ? minTooltipWidth : textContainerSize.x < maxTooltipWidth ? textContainerSize.x : maxTooltipWidth;

            textRect.sizeDelta = new Vector2(effectiveTextBlockWidth, textContainerSize.y);

            return textContainerSize;
        }

        private Vector3 SetTextForTextBlock(string text, TMP_Text textContainer, RectTransform textRect)
        {
            textRect.sizeDelta = new Vector2(maxTooltipWidth, textRect.sizeDelta.y);

            textContainer.SetText(text);

            textContainer.ForceMeshUpdate();

            Vector2 textContainerSize = textContainer.GetRenderedValues(false);

            float maxTextBoxLength = textContainerSize.x > currentTitleWidth ? textContainerSize.x : currentTitleWidth;

            float effectiveTooltipWidth = maxTextBoxLength < minTooltipWidth ? minTooltipWidth : maxTextBoxLength > maxTooltipWidth ? maxTooltipWidth : maxTextBoxLength;

            textRect.sizeDelta = new Vector2(effectiveTooltipWidth, textContainerSize.y);

            return textContainerSize;
        }
    }
}


