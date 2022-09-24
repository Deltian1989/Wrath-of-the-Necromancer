using UnityEngine;
using UnityEngine.EventSystems;

namespace WotN.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isPointerOverDialogueBox =false;

        void OnDisable()
        {
            isPointerOverDialogueBox = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerOverDialogueBox = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerOverDialogueBox = false;
        }

        public bool IsPointerOverDialogueBox()
        {
            return isPointerOverDialogueBox;
        }
    }
}

