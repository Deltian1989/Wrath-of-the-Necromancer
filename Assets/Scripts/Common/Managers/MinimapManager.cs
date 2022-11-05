using Sirenix.OdinInspector;
using UnityEngine;
using WotN.Interactables;
using WotN.Minimap;

namespace WotN.Common.Managers
{
    public class MinimapManager : MonoBehaviour
    {
        public static MinimapManager Instance { get; private set; }

        private const float BASE_HEIGHT_IN_3D_WORLD = 99000;

        private const string minimapItemsAreaTag = "MinimapItemsArea";

        [SerializeField]
        [AssetsOnly]
        private MinimapItem playerMinimapItemPrefab;

        [SerializeField]
        [AssetsOnly]
        private MinimapItemWithCaption npcMinimapItemPrefab;

        [SerializeField]
        [AssetsOnly]
        private MinimapItem stashMinimapItemPrefab;

        private MinimapCamera minimapCamera;

        private Transform minimapItemsAreaParent;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void SetPlayerForMinimapCameraToFollow(GameObject player)
        {
            if (!minimapCamera)
                minimapCamera = FindObjectOfType<MinimapCamera>();

            minimapCamera.SetPlayerToFollow(player);
        }

        public void SetMinimapItemForObject(MinimapWorldItem minimapObject, MinimapItemType minimapworldItemType)
        {
            MinimapItem currentMinimapItemPrefab;

            if (!minimapItemsAreaParent)
                minimapItemsAreaParent = GameObject.FindGameObjectWithTag(minimapItemsAreaTag).transform;

            switch (minimapworldItemType)
            {
                default:
                case MinimapItemType.Player:
                    currentMinimapItemPrefab = playerMinimapItemPrefab;
                    break;
                case MinimapItemType.NPC:
                    currentMinimapItemPrefab = npcMinimapItemPrefab;
                    break;
                case MinimapItemType.Stash:
                    currentMinimapItemPrefab = stashMinimapItemPrefab;
                    break;
            }

            var minimapItemInstance = Instantiate(currentMinimapItemPrefab, new Vector3(minimapObject.transform.position.x, BASE_HEIGHT_IN_3D_WORLD+ minimapItemsAreaParent.position.y, minimapObject.transform.position.z), Quaternion.Euler(0,180,0), minimapItemsAreaParent);

            minimapItemInstance.AssignMinimapWorldItem(minimapObject);

            if (minimapworldItemType == MinimapItemType.NPC && currentMinimapItemPrefab is MinimapItemWithCaption)
            {
                var isNpcInteractable = minimapObject.TryGetComponent<NPCInteractable>(out var npcInteractable);

                if (!isNpcInteractable)
                {
                    Debug.Log("Current game object for which minimap item is should be set up is not of NPCInteractable class");
                    return;
                }

                 ((MinimapItemWithCaption)minimapItemInstance).SetMinimapItemCaption(npcInteractable.GetNPCName());
            }
        }

        public enum MinimapItemType{
            Player,
            NPC,
            Stash
        }
    }
}
