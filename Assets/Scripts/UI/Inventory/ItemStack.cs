using WotN.ScriptableObjects.Items;
using System;

namespace WotN.UI.Inventory
{
    [Serializable]
    public class ItemStack
    {
        public Item item;

        public int count;
    }
}
