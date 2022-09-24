using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WotN.Common.Managers;
using static WotN.Common.Managers.MinimapManager;

namespace WotN.Minimap
{
    public class MinimapWorldItem : MonoBehaviour
    {
        [SerializeField]
        private MinimapItemType minmapItemType;

        void Start()
        {
            MinimapManager.Instance.SetMinimapItemForObject(this, minmapItemType);
        }
    }
}