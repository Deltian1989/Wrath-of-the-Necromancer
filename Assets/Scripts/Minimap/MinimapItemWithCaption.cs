using System.Collections;
using UnityEngine;

namespace WotN.Minimap
{
    public class MinimapItemWithCaption : MinimapItem
    {
        [SerializeField]
        private TMPro.TMP_Text minimapItemCaption;

        [SerializeField]
        private Canvas minimapItemCaptionCanvas;

        void Start()
        {
            var minimapCamere = FindObjectOfType<MinimapCamera>();
            minimapItemCaptionCanvas.worldCamera = minimapCamere.GetCamera();
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(minimapWorldItem.transform.position.x, transform.position.y, minimapWorldItem.transform.position.z), movementsSmoothing * Time.deltaTime);
        }

        public void SetMinimapItemCaption(string caption)
        {
            minimapItemCaption.text = caption;
        }
    }
}