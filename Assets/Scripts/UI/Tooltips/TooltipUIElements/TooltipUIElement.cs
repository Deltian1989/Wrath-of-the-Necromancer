using UnityEngine;
using UnityEngine.EventSystems;
using WotN.Common.Managers;

namespace WotN.UI.Tooltips.TooltipUIElements
{
    public class TooltipUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float tooltipHorizontalOffset = 0;

        [SerializeField]
        private int tooltipId;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.DisplayInfoTooltip(transform.position, tooltipHorizontalOffset, tooltipId);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideAllTooltips();
        }
    }
}

