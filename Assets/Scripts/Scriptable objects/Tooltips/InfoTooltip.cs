using UnityEngine;

namespace WotN.ScriptableObjects.Tooltips
{
    [CreateAssetMenu(fileName = "New info tooltip", menuName = "Tooltip/Info tooltip")]
    public class InfoTooltip : ScriptableObject
    {
        public int id;

        public string title;
    }
}

