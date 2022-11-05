using Sirenix.OdinInspector;
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

        [AssetsOnly]
        public GameObject areaPrefab;
    }
}
