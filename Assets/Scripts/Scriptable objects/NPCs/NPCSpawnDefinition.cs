using System;
using UnityEngine;
using WotN.Interactables;

namespace WotN.ScriptableObjects.NPCs
{
    [Serializable]
    public class NPCSpawnDefinition
    {
        public GameObject npcPrefab;

        public string spawnPointName;

        public Vector3[] walkStops;
    }
}

