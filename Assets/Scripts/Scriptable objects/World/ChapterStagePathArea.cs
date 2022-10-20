using System;
using UnityEngine;
using UnityEngine.AI;

namespace WotN.ScriptableObjects.World
{
    [Serializable]
    public class ChapterStagePathArea
    {
        public ChapterStagePathAreaDefinition areaDefinition;

        public Vector3 position;

        public GameObject areaPrefab;
    }
}
