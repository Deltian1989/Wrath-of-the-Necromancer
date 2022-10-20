using UnityEngine;

namespace WotN.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue Set", menuName = "Dialogue/Dialogue Set")]
    public class DialogueSet : ScriptableObject
    {
        public int ID = -1;

        public AudioClip[] sayHelloAudioClips;

        public AudioClip[] greetingAudioClips;

        public DialogueOption[] dialogueOptions;
    }
}

