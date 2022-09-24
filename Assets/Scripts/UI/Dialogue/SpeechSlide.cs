using UnityEngine;
using UnityEngine.UI;

namespace WotN.UI.Dialogue
{
    public class SpeechSlide : MonoBehaviour
    {
        [SerializeField]
        private float slideSpeed = 10f;

        [SerializeField]
        private float startSlidePosition;

        [SerializeField]
        private float endPosition;

        [SerializeField]
        private RectTransform dialogBox;

        [SerializeField]
        private RectTransform parentWindow;

        [SerializeField]
        private Text talkText;

        [SerializeField]
        private RectTransform rectTransform;

        void Awake()
        {
            rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, startSlidePosition);
        }

        void Update()
        {
            if (rectTransform.rect.y <= endPosition)
                rectTransform.Translate(0, slideSpeed * Time.deltaTime, 0);
        }

        public void DismissSpeechSlide()
        {
            talkText.text = null;

            rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, startSlidePosition);

            dialogBox.gameObject.SetActive(true);
            parentWindow.gameObject.SetActive(false);

        }

        public void SetTalkText(string talkText)
        {
            this.talkText.text = talkText;
            endPosition = rectTransform.rect.height + this.talkText.rectTransform.rect.height;
        }
    }
}
