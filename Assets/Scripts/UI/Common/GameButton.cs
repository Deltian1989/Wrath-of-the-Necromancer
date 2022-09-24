using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WotN.Common.Managers;

namespace WotN.UI.Common
{
    public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button button;

        void Awake()
        {
            button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (button.interactable)
                AudioManager.Instance.PlayMouseOverButton();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (button.interactable)
                AudioManager.Instance.PlayMouseOverButton();
        }
    }
}

