using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WotN.ScriptableObjects.Tooltips;

namespace WotN.UI.Tooltips.TooltipWindows
{
    public class ExtendedGenericTooltip : TooltipBase
    {
        [SerializeField]
        [ChildGameObjectsOnly]
        private TMP_Text descriptionText;

        private RectTransform descriptionRect;

        public override void DisplayTooltip(Vector3 uiElementPosition, float horizontalOffset, params string[] tooltipBlocks)
        {
            base.DisplayTooltip(uiElementPosition, horizontalOffset, tooltipBlocks);

            if (!descriptionRect)
                descriptionRect = descriptionText.GetComponent<RectTransform>();

            if (tooltipBlocks.Length != 2)
            {
                Debug.LogError("Generic tooltip must contain two text blocks");
            }

            var titleSize = SetText(tooltipBlocks[0], title, titleRect);

            var descriptionSize = SetText(tooltipBlocks[1], descriptionText, descriptionRect);

            var effectiveBackgroundWidth = titleRect.sizeDelta.x > descriptionRect.sizeDelta.x ? titleRect.sizeDelta.x : descriptionRect.sizeDelta.x;

            background.sizeDelta = new Vector2(effectiveBackgroundWidth + horizontalPadding, titleSize.y + descriptionSize.y + verticalPadding);

            SetTooltipPosition(uiElementPosition, horizontalOffset);
        }
    }
}

