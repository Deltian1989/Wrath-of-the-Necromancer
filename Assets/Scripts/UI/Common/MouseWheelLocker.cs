using UnityEngine;
using UnityEngine.EventSystems;

namespace WotN.UI.Common
{
    public class MouseWheelLocker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool canZoom = true;

        public void OnPointerEnter(PointerEventData eventData)
        {
            canZoom = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            canZoom = true;
        }

        private void OnDisable()
        {
            canZoom = true;
        }
    }
}

