using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WotN.UI.Tooltips.TooltipWindows
{
    public abstract class TooltipBase : MonoBehaviour
    {
        [SerializeField]
        protected RectTransform background;

        [SerializeField]
        protected TMP_Text title;

        [SerializeField]
        protected Transform rightArrow;

        [SerializeField]
        protected Transform leftArrow;

        [SerializeField]
        protected float verticalPadding = 50;

        [SerializeField]
        protected float horizontalPadding = 30;

        [SerializeField]
        protected float defaultTooltipWidth = 200;

        protected RectTransform currentRectTransform;

        protected RectTransform titleRect;

        public virtual void DisplayTooltip(Vector3 uiElementPosition, float horizontalOffset, params string[] tooltipBlocks)
        {
            if (!titleRect)
                titleRect = title.GetComponent<RectTransform>();

            gameObject.SetActive(true);
        }

        protected virtual Vector2 SetText(string text, TMP_Text textContainer, RectTransform textRect)
        {
            textRect.sizeDelta = new Vector2(defaultTooltipWidth, textRect.sizeDelta.y);

            textContainer.SetText(text);

            textContainer.ForceMeshUpdate();

            Vector2 textContainerSize = textContainer.GetRenderedValues(false);

            float effectiveCaptionWidth = textContainerSize.x >= defaultTooltipWidth ? defaultTooltipWidth : textContainerSize.x;

            textRect.sizeDelta = new Vector2(effectiveCaptionWidth, textContainerSize.y);

            return textContainerSize;
        }

        protected void SetTooltipPosition(Vector3 uiElementPosition, float horizontalOffset)
        {
            if (!currentRectTransform)
                currentRectTransform = GetComponent<RectTransform>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(currentRectTransform);

            var arrowDistanceFromTooltipPivot = currentRectTransform.rect.width / 2;

            float effectiveHorizontalOffset;

            float rightEdgeDistance = uiElementPosition.x - (arrowDistanceFromTooltipPivot + background.rect.width / 2) - horizontalOffset;

            if (0 > rightEdgeDistance)
            {
                effectiveHorizontalOffset = uiElementPosition.x + arrowDistanceFromTooltipPivot + horizontalOffset;

                rightArrow.gameObject.SetActive(false);

                leftArrow.gameObject.SetActive(true);
            }
            else
            {
                effectiveHorizontalOffset = uiElementPosition.x - arrowDistanceFromTooltipPivot - horizontalOffset;

                rightArrow.gameObject.SetActive(true);

                leftArrow.gameObject.SetActive(false);
            }

            transform.position = new Vector3(effectiveHorizontalOffset, uiElementPosition.y, transform.position.z);

            transform.position = new Vector3(transform.position.x, transform.position.y - (rightArrow.position.y - transform.position.y));
        }
    }
}

