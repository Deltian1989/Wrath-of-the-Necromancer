using UnityEngine;
using static WotN.UI.Utils.ItemHelper;

namespace WotN.ScriptableObjects.ShortcutKeyTips
{
    [CreateAssetMenu(fileName = "New shortcut key tip", menuName = "Shortcut key tip")]
    public class ShortcutKeyTip : ScriptableObject
    {
        public DisplayMode displayMode;

        public UIElementType uiElementType;

        [TextArea(3,5)]
        public string shortcutKeyTipDescription;
    }
}

