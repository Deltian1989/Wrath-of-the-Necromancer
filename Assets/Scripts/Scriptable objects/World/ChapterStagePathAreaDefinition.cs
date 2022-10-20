using UnityEngine;
using WotN.ScriptableObjects.NPCs;

namespace WotN.ScriptableObjects.World
{
    [CreateAssetMenu(fileName = "New stage area", menuName = "World/Chapter stage area")]
    public class ChapterStagePathAreaDefinition : ScriptableObject
    {
        public int id;

        public string areaName;

        public bool isSafeArea;

        public NPCSpawnDefinition[] npcSpawnDefinitions;
    }
}
