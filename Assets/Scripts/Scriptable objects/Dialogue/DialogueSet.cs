using Sirenix.OdinInspector;
using UnityEngine;

namespace WotN.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue Set", menuName = "Dialogue/Dialogue Set")]
    public class DialogueSet : ScriptableObject
    {
        public int ID = -1;

        [InlineEditor(InlineEditorModes.SmallPreview)]
        [AssetsOnly]
        public AudioClip[] sayHelloAudioClips;

        [InlineEditor(InlineEditorModes.SmallPreview)]
        [AssetsOnly]
        public AudioClip[] greetingAudioClips;

        public DialogueOption[] dialogueOptions;
    }
}

