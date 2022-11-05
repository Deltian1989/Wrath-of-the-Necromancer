using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using WotN.ScriptableObjects.Items;
using WotN.ScriptableObjects.ShortcutKeyTips;
using WotN.ScriptableObjects.Tooltips;
using WotN.UI.Tooltips.TooltipWindows;
using WotN.UI.Utils;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.Common.Managers
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance { get; private set; }

        [BoxGroup("Tooltip SOs")]
        [SerializeField]
        private InfoTooltip[] tooltips;

        [BoxGroup("Tooltip SOs")]
        [SerializeField]
        private ShortcutKeyTip[] shortcutKeyTips;

        [BoxGroup("Tooltip UI")]
        [SerializeField]
        private Transform tooltipArea;

        [BoxGroup("Tooltip UI")]
        [SerializeField]
        private ItemTooltip itemTooltip;

        [BoxGroup("Tooltip UI")]
        [SerializeField]
        private GenericTooltip genericTooltip;

        [BoxGroup("Tooltip UI")]
        [SerializeField]
        private ExtendedGenericTooltip extendedGenericTooltip;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                tooltips = Resources.LoadAll<InfoTooltip>("Tooltips");
                shortcutKeyTips= Resources.LoadAll<ShortcutKeyTip>("Shortcut key tips");
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

        public void DisplayTooltipWithShortcutKeyTip(Vector3 uiElementPosition, float horizontalOffsetint, int tooltipId, DisplayMode displayMode, UIElementType uiElementType)
        {
            var tooltip = tooltips.FirstOrDefault(x => x.id == tooltipId);

            string shortcutKeyTip = shortcutKeyTips.First(x => x.displayMode == displayMode && x.uiElementType == uiElementType).shortcutKeyTipDescription;

            extendedGenericTooltip.DisplayTooltip(uiElementPosition, horizontalOffsetint, tooltip.title, shortcutKeyTip);
        }

        public void DisplayInfoTooltip(Vector3 uiElementPosition, float horizontalOffsetint, int tooltipId)
        {
            var tooltip = tooltips.FirstOrDefault(x => x.id == tooltipId);

            genericTooltip.DisplayTooltip(uiElementPosition, horizontalOffsetint, tooltip.title);
        }

        public void DisplayTooltipForItem(Vector3 itemSlotPosition, float horizontalOffsetint, Item item, DisplayMode mode, UIElementType uiElementType)
        {
            var itemDescription = GenerateItemDescription(item);

            var shortcutKeytipSO = shortcutKeyTips.SingleOrDefault(x => x.displayMode == mode && x.uiElementType == uiElementType);

            if (shortcutKeytipSO == null)
            {
                Debug.LogError($"Missing shortcut key tip with display mode {mode} and UI element type {uiElementType}");
                return;
            }

            string shortcutKeyTip = shortcutKeytipSO.shortcutKeyTipDescription;

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

