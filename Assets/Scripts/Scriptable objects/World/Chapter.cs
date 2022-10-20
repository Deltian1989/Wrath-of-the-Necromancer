using UnityEngine;

namespace WotN.ScriptableObjects.World
{
    [CreateAssetMenu(fileName = "New chapter", menuName = "World/Chapter")]
    public class Chapter : ScriptableObject
    {
        public int id;

        public string chapterName;

        public ChapterStage[] chapterStages;
    }
}
