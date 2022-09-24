using System;
using UnityEngine;

namespace WotN.ScriptableObjects.Dialogue
{
    [Serializable]
    public class DialogueOption
    {
        public string DialogueOptionText;

        [TextArea(1, 4)]
        public string Talk;
    }
}

