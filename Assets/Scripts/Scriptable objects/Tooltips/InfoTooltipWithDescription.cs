using UnityEngine;

namespace WotN.ScriptableObjects.Tooltips
{
    [CreateAssetMenu(fileName = "New info tooltip with description", menuName = "Tooltip/Info tooltip with description")]
    public class InfoTooltipWithDescription : InfoTooltip
    {
        [TextArea]
        public string description;
    }
}

