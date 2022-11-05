using Sirenix.OdinInspector;
using UnityEngine;

namespace WotN.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [BoxGroup("Basic info")]
        [LabelWidth(80)]
        public int ID = -1;
        [BoxGroup("Basic info")]
        [LabelWidth(80)]
        public string itemName = "New Item";

        [BoxGroup("Basic info")]
        [LabelWidth(80)]
        public int stackSize = 1;

        [AssetsOnly]
        [SerializeField]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public AudioClip useSFX;

        [HorizontalGroup("Graphics", 150)]
        [PreviewField(150)]
        [HideLabel]
        [AssetsOnly]
        public Sprite image;

        public virtual void Use()
        {

        }
    }
}


