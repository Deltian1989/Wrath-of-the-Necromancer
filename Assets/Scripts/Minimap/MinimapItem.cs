using UnityEngine;

namespace WotN.Minimap
{
    public class MinimapItem : MonoBehaviour
    {
        protected MinimapWorldItem minimapWorldItem;

        [SerializeField]
        protected float movementsSmoothing = 6;

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(minimapWorldItem.transform.position.x, transform.position.y, minimapWorldItem.transform.position.z), movementsSmoothing * Time.deltaTime);
        }

        public void AssignMinimapWorldItem(MinimapWorldItem minimapWorldItem)
        {
            this.minimapWorldItem = minimapWorldItem;
        }

    }
}