using UnityEngine;
using UnityEngine.UI;
using WotN.ScriptableObjects.Items;
using WotN.Common.Managers;
using WotN.UI.Tooltips.TooltipUIElements;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.UI.Equipment
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField]
        private ItemEquipment equippedItem;

        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private Button button;

        [SerializeField]
        private DisplayMode displayMode;

        private EquipmentItemTooltipUIElement equipmentItemTooltipUIElement;

        void Start()
        {
            equipmentItemTooltipUIElement=GetComponent<EquipmentItemTooltipUIElement>();
        }

        public DisplayMode GetDisplayMode()
        {
            return displayMode;
        }

        public ItemEquipment GetEquipmentItem()
        {
            return equippedItem;
        }

        public void AddItem(ItemEquipment item)
        {
            UnmarkOccupied();

            equippedItem = item;
            itemIcon.sprite = equippedItem.image;
            button.interactable = true;
            itemIcon.gameObject.SetActive(true);
        }

        public void ClearSlot()
        {
            itemIcon.sprite = null;
            itemIcon.gameObject.SetActive(false);
            button.interactable = false;

            equippedItem = null;
        }

        public void UnequipItem()
        {
            AudioManager.Instance.PlayUISFX(equippedItem.useSFX);

            EquipmentManager.Instance.UnequipItem(equippedItem.equipmentSlot);

            equipmentItemTooltipUIElement.ResetTooltip(equippedItem);
        }

        public void MarkOccupied(ItemWeaponBase weaponItem)
        {
            itemIcon.sprite = weaponItem.image;
            button.interactable = false;
            itemIcon.gameObject.SetActive(true);

            var iconColor = itemIcon.color;
            iconColor.a = 0.5f;

            itemIcon.color = iconColor;

            equippedItem = null;
        }

        public void UnmarkOccupied()
        {
            itemIcon.sprite = null;
            button.interactable = false;
            itemIcon.gameObject.SetActive(false);

            var iconColor = itemIcon.color;
            iconColor.a = 1f;

            itemIcon.color = iconColor;
        }
    }
}
