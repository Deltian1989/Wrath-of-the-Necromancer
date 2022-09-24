using System.Linq;
using UnityEngine;
using WotN.ScriptableObjects.Items;
using WotN.ScriptableObjects.Tooltips;
using WotN.UI.Tooltips.TooltipWindows;
using WotN.UI.Utils;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.Common.Managers
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance { get; private set; }

        [SerializeField]
        private InfoTooltip[] tooltips;

        [SerializeField]
        private Transform tooltipArea;

        [SerializeField]
        private ItemTooltip itemTooltip;

        [SerializeField]
        private GenericTooltip genericTooltip;

        [SerializeField]
        private ExtendedGenericTooltip extendedGenericTooltip;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                tooltips = Resources.LoadAll<InfoTooltip>("Tooltips");
            }
        }

        public bool IsItemTooltipActive()
        {
            return itemTooltip.gameObject.activeInHierarchy;
        }

        public bool IsInfoTooltipActive()
        {
            return genericTooltip.gameObject.activeInHierarchy;
        }

        public void DisplayInfoTooltip(Vector3 uiElementPosition, float horizontalOffsetint, int tooltipId)
        {
            var tooltip = tooltips.FirstOrDefault(x => x.id == tooltipId);

            switch (tooltip)
            {
                case InfoTooltipWithDescription:
                    {
                        var extendedTooltip = (InfoTooltipWithDescription)tooltip;

                        extendedGenericTooltip.DisplayTooltip(uiElementPosition, horizontalOffsetint, extendedTooltip.title, extendedTooltip.description);
                    }
                    break;
                case InfoTooltip:
                    {
                        genericTooltip.DisplayTooltip(uiElementPosition, horizontalOffsetint,tooltip.title);
                    }
                    break;
                default:
                    Debug.LogError($"The tooltip with id {tooltipId} does not exist.");
                    break;
            }


        }

        public void DisplayTooltipForItem(Vector3 itemSlotPosition, float horizontalOffsetint, Item item, DisplayMode displayMode)
        {
            var itemDescription = ItemHelper.GenerateItemDescription(item, displayMode);

            string shortcutKeyTip;

            if (displayMode != DisplayMode.Stash)
                shortcutKeyTip = "Left click to use the item\nShift + left click to move the item to the stash\nCtrl + left click to drop the item";
            else
                shortcutKeyTip = "Left click to use the item\nShift + left click to move the item to your inventory\nCtrl + left click to drop the item";

            itemTooltip.DisplayTooltip(itemSlotPosition, horizontalOffsetint, item.itemName, itemDescription, shortcutKeyTip);
        }

        public void HideAllTooltips()
        {
            itemTooltip.gameObject.SetActive(false);
            genericTooltip.gameObject.SetActive(false);
            extendedGenericTooltip.gameObject.SetActive(false);
        }

        
    }
}

