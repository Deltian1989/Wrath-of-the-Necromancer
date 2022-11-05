using Sirenix.OdinInspector;
using System;
using UnityEngine;
using WotN.Interactables;

namespace WotN.ScriptableObjects.NPCs
{
    [Serializable]
    public class NPCSpawnDefinition
    {
        public string spawnPointName;

        [AssetsOnly]
        [PreviewField(150)]
        public GameObject npcPrefab;

        public Vector3[] walkStops;
    }
}

