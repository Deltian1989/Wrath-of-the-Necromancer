using UnityEngine;

namespace WotN.ScriptableObjects.Tooltips
{
    [CreateAssetMenu(fileName = "New info tooltip", menuName = "Tooltips/Info tooltip")]
    public class InfoTooltip : ScriptableObject
    {
        public int id;

        public string title;
    }
}

