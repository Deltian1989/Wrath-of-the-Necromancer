using UnityEngine;

namespace WotN.ScriptableObjects.World
{
    [CreateAssetMenu(fileName = "New chapter stage", menuName = "World/Chapter stage")]
    public class ChapterStage : ScriptableObject
    {

        public string stageName;

        public int buildIndex;

        public ChapterStagePath[] chapterStagePaths;
    }
}

