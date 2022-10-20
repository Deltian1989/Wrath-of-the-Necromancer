using System;
using UnityEngine.AI;

namespace WotN.ScriptableObjects.World
{
    [Serializable]
    public class ChapterStagePath
    {
        public ChapterStagePathArea[] chapterStagePathAreas;

        public NavMeshData navMeshData;
    }
}

