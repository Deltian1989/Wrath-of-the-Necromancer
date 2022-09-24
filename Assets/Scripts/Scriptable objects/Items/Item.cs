using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public int ID = -1;

        public string itemName = "New Item";

        public Sprite image;

        [SerializeField]
        public AudioClip useSFX;

        public int stackSize = 1;

        public virtual void Use()
        {

        }
    }
}


