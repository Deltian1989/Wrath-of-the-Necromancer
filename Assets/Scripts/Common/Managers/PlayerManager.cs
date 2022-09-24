using WotN.ScriptableObjects.Items;
using UnityEngine;
using WotN.Player;
using System;
using WotN.Common.Utils.GamePersistance;
using WotN.Common.Utils.EventData.SkillPanel;
using WotN.Common.Utils.EventData.AvatarPanel;
using WotN.Common.Utils.EventData.Inventory;
using WotN.Common.Utils.EventData.Equipment;
using WotN.Interactables;

namespace WotN.Common.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        public event Action<AvatarPanelInitializeData> onAvatarPanelDataInitialized;

        public event Action<AvatarPanelUpdateData> onAvatarPanelDataUpdated;

        public event Action<SkillPanelInitializeData> onSkillPanelDataInitialized;

        public event Action<SkillPanelUpdateData> onSkillPanelDataUpdated;

        public event Action<InventoryInitializeData> onInventoryInitialized;

        public event Action<InventoryUpdateData> onInventoryDataUpdated;

        public event Action<EquipmentInitializeData> onEquipmentDataInitialized;

        public event Action<EquipmentUpdateData> onEquipmentDataUpdated;

        public float RotateTime => rotateTime;

        public float HighlightDistance => highlightDistance;

        public LayerMask NPCInteractableLayers => npcInteractableLayers;

        public LayerMask InteractableLayers => interactableLayers;

        public float MinDistanceFromNPCToInteract => minDistanceFromNPCToInteract;

        public int DestinationSettingDistance => destinationSettingDistance;

        public LayerMask WalkableLayers => walkableLayers;

        [Header("Basic hero data")]
        [SerializeField]
        private string heroName;
        [SerializeField]
        private string heroClass;

        [SerializeField]
        private int difficulty;

        [SerializeField]
        private int currentSceneIndex;

        [SerializeField]
        private int playerGold;

        [Header("Hero level & exp")]
        [SerializeField]
        private int heroLevel;
        [SerializeField]
        private int currentExp;

        [Header("Level & exp settings")]
        [SerializeField]
        private int[] availableLevels = new int[100];
        [SerializeField]
        private int baseExp = 1000;
        [SerializeField]
        private float expMultiplier = 1.1f;

        [Header("Primary skills")]
        [SerializeField]
        private int strength;
        [SerializeField]
        private int dexterity;
        [SerializeField]
        private int magic;
        [SerializeField]
        private int vitality;
        [SerializeField]
        private int energy;

        [Header("Effective stats")]
        [SerializeField]
        private int damage;
        [SerializeField]
        private int defense;
        [SerializeField]
        private int magicPower;
        [SerializeField]
        private int magicResist;
        [SerializeField]
        private int maxHP;
        [SerializeField]
        private int remainingHP;
        [SerializeField]
        private int maxMP;
        [SerializeField]
        private int remainingMP;
        [SerializeField]
        private int maxStamina;
        [SerializeField]
        private int remianingStamina;

        [Header("Movement settings")]
        [SerializeField]
        private int destinationSettingDistance = 1000;

        [SerializeField]
        private LayerMask walkableLayers;

        [Header("Interaction settings")]
        [SerializeField]
        private int highlightDistance = 1000;

        [SerializeField]
        private LayerMask interactableLayers;

        [SerializeField]
        private LayerMask npcInteractableLayers;

        [SerializeField]
        private float rotateTime = 4;

        [SerializeField]
        private float minDistanceFromNPCToInteract = 2f;

        private PlayerAppearance playerAppearance;
        private PlayerMovement playerMovement;
        private PlayerNPCInteractable playerNPCInteractable;
        private PlayerInteractable playerInteractable;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
            for (int i = 0; i < availableLevels.Length; i++)
            {
                if (i == 0)
                {
                    availableLevels[i] = baseExp;
                }
                else
                {
                    availableLevels[i] = (int)(availableLevels[i - 1] * expMultiplier);
                }
            }

            EquipmentManager.Instance.onEquipmentInitialized += InitializePlayerEquipment;
            EquipmentManager.Instance.onEquipmentChanged += UpdatePlayerVisuals;
        }

        public int GetPlayerGold()
        {
            return playerGold;
        }

        public void SetPlayerGold(int gold)
        {
            playerGold = gold;
            onInventoryDataUpdated?.Invoke(new InventoryUpdateData { currentGold = playerGold });
        }

        public void SetSpawnedPlayer(GameObject player)
        {
            playerAppearance = player.GetComponent<PlayerAppearance>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerNPCInteractable= player.GetComponent<PlayerNPCInteractable>();
            playerInteractable=player.GetComponent<PlayerInteractable>();
        }

        public void LoadCharacterData(PersistedCharacterData characterData, Texture heroSillhoute, Texture heroPortrait)
        {
            heroName = characterData.heroName;

            difficulty = characterData.reachedDifficulty;

            currentSceneIndex = characterData.currentSceneIndex;
            heroClass = characterData.heroClass;
            heroLevel = characterData.currentLevel;
            currentExp = characterData.currentExp;
            damage = characterData.damage;
            defense = characterData.defense;
            dexterity = characterData.dexterity;
            energy = characterData.energy;
            magic = characterData.magic;
            magicPower = characterData.magicPower;
            magicResist = characterData.magicResist;
            maxHP = characterData.maxHP;
            maxMP = characterData.maxMP;
            maxStamina = characterData.maxStamina;
            remainingHP = characterData.remainingHP;
            remainingMP = characterData.remainingMP;
            remianingStamina = characterData.remianingStamina;
            strength = characterData.strength;
            vitality = characterData.vitality;
            playerGold = characterData.playerGold;

            InventoryInitializeData inventoryInitializeData = new InventoryInitializeData
            {
                currentGold = playerGold
            };

            onInventoryInitialized?.Invoke(inventoryInitializeData);

            EquipmentInitializeData equipmentInitializeData = new EquipmentInitializeData
            {
                attack = damage,
                MP = maxMP,
                resist = magicResist,
                magicPower = magicPower,
                armor = defense,
                heroLevel = heroLevel,
                HP = maxHP,
                heroSillhoute = heroSillhoute,
                heroNickname = heroName,
                heroClass = heroClass,
            };

            onEquipmentDataInitialized?.Invoke(equipmentInitializeData);

            AvatarPanelInitializeData avatarPanelInitializeData = new AvatarPanelInitializeData
            {
                heroLevel = heroLevel,
                heroName = heroName,
                heroPortrait = heroPortrait,
                maxHP = maxHP,
                remainingHP = remainingHP,
                maxMP = maxMP,
                remainingMP = remainingMP
            };

            onAvatarPanelDataInitialized?.Invoke(avatarPanelInitializeData);

            SkillPanelInitializeData skillPanelInitializeData = new SkillPanelInitializeData
            {
                heroLevel = heroLevel,
                currentExp = currentExp,
                nextLevelExp = availableLevels[heroLevel],
                maxStamina = maxStamina,
                remainingStamina = remianingStamina
            };

            onSkillPanelDataInitialized?.Invoke(skillPanelInitializeData);

        }

        public void UnfocusPlayer()
        {
            playerInteractable.UnfocusPlayer();
            TooltipManager.Instance.HideAllTooltips();
        }

        private void UpdatePlayerVisuals(ItemEquipment newItem, ItemEquipment oldItem, ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            if (oldItem is ItemArmor || newItem is ItemArmor)
            {
                if (oldItem != null)
                {
                    HideArmorPiece(oldItem as ItemArmor);
                    ShowBodyPart(itemEquipmentSlot);
                }

                if (newItem != null)
                {
                    HideBodyPart(itemEquipmentSlot);
                    ShowArmorPiece(newItem as ItemArmor);
                }
            }
            else if (oldItem is ItemWeaponBase || newItem is ItemWeaponBase)
            {
                if (oldItem != null)
                {
                    DetachWeapon(itemEquipmentSlot);
                }

                var itemWeapon = newItem as ItemWeaponBase;

                if (itemWeapon != null)
                {

                    SetEquippedWeapon(itemWeapon);
                }
                
            }
            else
            {
                Debug.LogError("Something went wrong in here. May both newItem and oldItem be null in some cases?!");
            }
        }

        private void InitializePlayerEquipment(ItemEquipment[] equipmentItems)
        {
            for (int i = 0; i < equipmentItems.Length; i++)
            {
                if (equipmentItems[i] != null && equipmentItems[i] is ItemArmor)
                {
                    HideBodyPart(equipmentItems[i].equipmentSlot);
                    ShowArmorPiece(equipmentItems[i] as ItemArmor);
                }
                else if (equipmentItems[i] != null && equipmentItems[i] is ItemWeaponBase)
                {
                    SetEquippedWeapon(equipmentItems[i] as ItemWeaponBase);
                }
            }
        }

        private void HideArmorPiece(ItemArmor oldItem)
        {
            playerAppearance.HideArmorPiece(oldItem);
        }

        private void ShowBodyPart(ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            playerAppearance.ShowBodyPart(itemEquipmentSlot);
        }

        private void HideBodyPart(ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            playerAppearance.HideBodyPart(itemEquipmentSlot);
        }

        private void ShowArmorPiece(ItemArmor armorItem)
        {
            playerAppearance.ShowArmorPiece(armorItem);
        }

        private void DetachWeapon(ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            playerAppearance.DetachWeapon(itemEquipmentSlot);
        }

        private void SetEquippedWeapon(ItemWeaponBase itemWeapon)
        {
            playerAppearance.DetachWeapon(itemWeapon.equipmentSlot);
            playerAppearance.SetEquippedWeapon(itemWeapon);
        }

        public bool IsPlayerTalking()
        {
            return playerNPCInteractable.IsPlayerTalking();
        }

        public void HandleTalkToNPCWithEscPressed()
        {
            playerNPCInteractable.HandleTalkToNPCWithEscPressed();
        }
    }
}
