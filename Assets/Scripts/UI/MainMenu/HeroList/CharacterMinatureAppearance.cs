using WotN.ScriptableObjects.Items;
using UnityEngine;
using WotN.Common.Utils;
using WotN.Common.Managers;
using WotN.Common.Utils.GamePersistance;
using Sirenix.OdinInspector;

namespace WotN.UI.MainMenu.HeroList
{
    public class CharacterMinatureAppearance : MonoBehaviour
    {
        public string heroClass;

        [Header("Armor slots")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject head;
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject chest;
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject pants;
        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject boots;

        [Header("Weapon rest slots")]
        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform twoHandedWeaponRestSlot;
        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform oneHandWeaponRestSlot;
        [SerializeField]
        [ChildGameObjectsOnly]
        private Transform shieldRestSlot;

        [SerializeField]
        [ChildGameObjectsOnly]
        private GameObject[] armorPieces;

        public void ShowEquipmentParts(SavedEquippmentItemData[] equippedItems)
        {
            if (equippedItems.Length != 6)
            {
                Debug.LogError("6 equipment parts should be passed along even when they are null");
                return;
            }

            RenderHead(equippedItems);
            RenderChest(equippedItems);
            RenderPants(equippedItems);
            RenderBoots(equippedItems);

            if (equippedItems[4] != null)
            {
                SetEquippedMainHandWeapon(equippedItems[4]);
                
            }
            
            if (equippedItems[5] != null)
            {
                SetEquippedMainHandWeapon(equippedItems[5]);
            }
        }

        private void RenderHead(SavedEquippmentItemData[] equippedItems)
        {
            if (equippedItems[0] != null)
            {
                head.SetActive(false);

                foreach (var armorPiece in armorPieces)
                {
                    if (armorPiece.name == equippedItems[0].objectName)
                    {
                        armorPiece.SetActive(true);
                        break;
                    }
                        
                }
            }
        }

        private void RenderChest(SavedEquippmentItemData[] equippedItems)
        {
            if (equippedItems[1] != null)
            {
                chest.SetActive(false);

                foreach (var armorPiece in armorPieces)
                {
                    if (armorPiece.name == equippedItems[1].objectName)
                    {
                        armorPiece.SetActive(true);
                        break;
                    }   
                }
            }
        }

        private void RenderPants(SavedEquippmentItemData[] equippedItems)
        {
            if (equippedItems[2] != null)
            {
                pants.SetActive(false);

                foreach (var armorPiece in armorPieces)
                {
                    if (armorPiece.name == equippedItems[2].objectName)
                    {
                        armorPiece.SetActive(true);
                        break;
                    }   
                }
            }
        }

        private void RenderBoots(SavedEquippmentItemData[] equippedItems)
        {
            if (equippedItems[3] != null)
            {
                boots.SetActive(false);

                foreach (var armorPiece in armorPieces)
                {
                    if (armorPiece.name == equippedItems[3].objectName)
                    {
                        armorPiece.SetActive(true);
                        break;
                    }
                        
                }
            }
        }

        public void SetEquippedMainHandWeapon(SavedEquippmentItemData itemWeapon)
        {
            Transform weaponRestSpot = null;

            switch (itemWeapon.itemType)
            {
                case 1:
                    weaponRestSpot = oneHandWeaponRestSlot;
                    break;
                case 2:
                    weaponRestSpot = shieldRestSlot;
                    break;
                case 3:
                    weaponRestSpot = twoHandedWeaponRestSlot;
                    break;
                default:
                    Debug.LogError("Invalid item type. It can't be armor piece or of any other inapropriate type.");
                    return;
            }

            var weaponSO = GameManager.Instance.GetItemById<ItemWeaponBase>(itemWeapon.itemId);

            var weaponInstance = Instantiate(weaponSO.weaponModel, weaponRestSpot.position, weaponRestSpot.rotation);

            weaponInstance.transform.SetParent(weaponRestSpot);
        }
    }
}

