using WotN.Player;
using WotN.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WotN.Common.Utils;
using WotN.ScriptableObjects.HeroClass;
using UnityEngine.InputSystem;
using System.Linq;
using WotN.Common.Utils.GamePersistance;
using WotN.Common.Utils.EventData.AvatarPanel;
using WotN.Common.Utils.EventData.SkillPanel;
using WotN.Common.Utils.EventData.Inventory;
using WotN.Common.Utils.EventData.Equipment;
using WotN.UI.Inventory;
using System.Collections.Generic;
using UnityEditor;
using WotN.ScriptableObjects.World;
using UnityEngine.AI;
using WotN.Interactables;
using WotN.Common.Utils.GamePersistence;
using Sirenix.OdinInspector;

namespace WotN.Common.Managers
{
    public class GameManager : GenericSingleton<GameManager>
    {
        public const string PlayerTag = "Player";

        public const string RespawnTag = "Respawn";

        public const string NPCSpawnAreaTag = "NPCSpawnArea";

        public const string StageTerrainTag = "StageTerrain";

        public bool isGameStarted;

        public bool isInGame;

        [SerializeField]
        [ChildGameObjectsOnly]
        private Canvas mainGameCanvas;

        [SerializeField]
        private HeroClass selectedHeroClass;

        [SerializeField]
        private Item[] availableItems;

        [SerializeField]
        private HeroClass[] availableHeroClasses;

        [SerializeField]
        private Chapter[] chapters;

        [SerializeField]
        private ChapterStage[] chapterStages;

        [SerializeField]
        private ChapterStagePathAreaDefinition[] chapterStageAreaDefinitions;

        [SerializeField]
        private PersistedCharacterData characterData;

        private PlayerInput playerHUDInput;

        private NavMeshDataInstance currentNavMeshInstance;

        void Start()
        {
            mainGameCanvas.enabled = false;
            SceneManager.sceneLoaded += OnSceneLoaded;

            playerHUDInput = mainGameCanvas.GetComponent<PlayerInput>();

            playerHUDInput.enabled = false;

            availableItems = Resources.LoadAll<Item>("Items");

            availableHeroClasses = Resources.LoadAll<HeroClass>("Hero classes");

            chapters = Resources.LoadAll<Chapter>("World/Chapters");

            chapterStages = Resources.LoadAll<ChapterStage>("World/Chapter stages");

            chapterStageAreaDefinitions = Resources.LoadAll<ChapterStagePathAreaDefinition>("World/Chapter stage areas");

            PlayerManager.Instance.onAvatarPanelDataUpdated += OnAvatarPanelDataUpdated;

            PlayerManager.Instance.onSkillPanelDataUpdated += OnSkillPanelDataUpdated;

            PlayerManager.Instance.onInventoryDataUpdated += OnInventoryDataUpdated;

            PlayerManager.Instance.onEquipmentDataUpdated += OnEquipmentDataUpdated;

            InventoryManager.Instance.onInventoryUpdated += OnInventoryUpdated;

            EquipmentManager.Instance.onEquipmentChanged += OnEquippmentItemsChanged;

            StashManager.Instance.onStashedGold+= OnStashedGold;

            StashManager.Instance.onItemsUpdated += OnInventoryInStashUpdated;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(InitializeGameState(scene));
        }

        private IEnumerator InitializeGameState(Scene scene)
        {
            if (scene.buildIndex != 0 && scene.buildIndex != 1)
            {
                mainGameCanvas.enabled = true;

                Transform stageTerrain = GameObject.FindGameObjectWithTag(StageTerrainTag).transform;

                var currentChapter = chapters.FirstOrDefault(x => x.id == characterData.currentChapterId);

                if (currentChapter == null)
                {
                    Debug.LogError("Cannot load chapter");
                    yield return null;
                }

                var currentStage = currentChapter.chapterStages.FirstOrDefault(x => x.buildIndex == characterData.currentTownStageId);

                if(currentStage == null)
                {
                    Debug.LogError("Cannot load stage");
                    yield return null;
                }

                var generatedStagePath = characterData.generatedStagePaths.FirstOrDefault(x => x.chapterId == characterData.currentChapterId && x.stageId == characterData.currentTownStageId);

                int stagePathIndex = 0;

                if (generatedStagePath == null)
                {
                    int stagePathsCount = currentStage.chapterStagePaths.Length;

                    stagePathIndex = Random.Range(0, stagePathsCount);

                    var generatedStagePathData = new GeneratedStagePathData
                    {
                        chapterId = characterData.currentChapterId,
                        stageId = characterData.currentTownStageId,
                        generatedStagePathIndex = stagePathIndex
                    };

                    characterData.generatedStagePaths.Add(generatedStagePathData);
                }
                else
                {
                    stagePathIndex = generatedStagePath.generatedStagePathIndex;
                }

                var currentChapterStagePathAreas = currentStage.chapterStagePaths[stagePathIndex].chapterStagePathAreas;

                for (int i = 0; i< currentChapterStagePathAreas.Length; i++)
                {
                    var currentChapterStagePathArea = currentChapterStagePathAreas[i];

                    Instantiate(currentChapterStagePathArea.areaPrefab, currentChapterStagePathArea.position, Quaternion.identity, stageTerrain);
                }

                var path = chapters[0].chapterStages[0].chapterStagePaths[0];

                currentNavMeshInstance = NavMesh.AddNavMeshData(path.navMeshData);

                Vector3 spawnPos = GameObject.FindGameObjectWithTag(RespawnTag).transform.position;

                GameObject player = Instantiate(
                    selectedHeroClass.characterPrefab.gameObject,
                    spawnPos, Quaternion.identity);

                PlayerManager.Instance.SetSpawnedPlayer(player);

                MinimapManager.Instance.SetPlayerForMinimapCameraToFollow(player);

                PlayerManager.Instance.LoadCharacterData(characterData, selectedHeroClass.heroSillhoute, selectedHeroClass.heroPortrait);

                InventoryManager.Instance.SetInventoryItems(characterData.itemsInInventory);
                EquipmentManager.Instance.SetEquipmentItems(characterData.equippedItems);

                StashManager.Instance.InitializeStash(characterData.itemsInStash, characterData.stashedGold);

                GameObject npcSpawnArea = GameObject.FindGameObjectWithTag(NPCSpawnAreaTag);

                Transform[] npcSpawnPoints = npcSpawnArea.GetComponentsInChildren<Transform>();

                var npcSpawnDefinitions = currentChapterStagePathAreas[0].areaDefinition.npcSpawnDefinitions;

                for (int i = 0; i < npcSpawnDefinitions.Length; i++)
                {
                    var spawnPoint = npcSpawnPoints.FirstOrDefault(x => x.name == npcSpawnDefinitions[i].spawnPointName);

                    if (spawnPoint == null)
                    {
                        Debug.LogError("No matching spawn point for the NPC " + npcSpawnDefinitions[i].npcPrefab.name);
                        continue;
                    }

                    var npcInstance = Instantiate(npcSpawnDefinitions[i].npcPrefab, spawnPoint.localPosition, spawnPoint.localRotation, spawnPoint);

                    if (npcInstance.TryGetComponent<NPCMovement>(out NPCMovement npcMovement))
                    {
                        npcMovement.SetWalkStops(spawnPoint, npcSpawnDefinitions[i].walkStops);
                    }
                }

                isInGame = true;

                playerHUDInput.enabled = true;

                ControlsManager.Instance.GetCustomizableControls().Enable();

                AudioManager.Instance.PlayMusicForCurrentScene(scene.buildIndex);
                AudioManager.Instance.PlayAmbientSFXForCurrentScene(scene.buildIndex);
            }
            else
            {
                ControlsManager.Instance.GetCustomizableControls().Disable();

                if (currentNavMeshInstance.valid)
                {
                    NavMesh.RemoveNavMeshData(currentNavMeshInstance);
                }

                isInGame = false;

                playerHUDInput.enabled = false;
            }

            yield return null;
        }

        private void OnAvatarPanelDataUpdated(AvatarPanelUpdateData eventData)
        {
            characterData.maxMP = eventData.maxMP;
            characterData.remainingMP = eventData.remainingMP;
            characterData.maxHP = eventData.maxHP;
            characterData.remainingHP = eventData.remainingHP;
            characterData.currentLevel = eventData.heroLevel;
        }

        private void OnSkillPanelDataUpdated(SkillPanelUpdateData eventData)
        {
            characterData.maxStamina= eventData.maxStamina;
            characterData.remianingStamina = eventData.remainingStamina;
            characterData.currentExp = eventData.currentExp;
            characterData.currentLevel = eventData.heroLevel;

        }

        private void OnInventoryDataUpdated(InventoryUpdateData eventData)
        {
            characterData.playerGold = eventData.currentGold;
        }

        private void OnStashedGold(int gold)
        {
            characterData.stashedGold = gold;
        }

        private void OnInventoryInStashUpdated(List<ItemStack> itemStacks)
        {
            characterData.itemsInStash = itemStacks.Select(i => new SavedInventoryItemData { itemId = i.item.ID, count = i.count }).ToList();
        }

        private void OnEquipmentDataUpdated(EquipmentUpdateData eventData)
        {
            characterData.defense = eventData.armor;
            characterData.damage = eventData.attack;
            characterData.maxHP = eventData.HP;
            characterData.maxMP = eventData.MP;
            characterData.magicResist = eventData.resist;
            characterData.magicPower = eventData.magicPower;
            characterData.currentLevel = eventData.heroLevel;
        }

        private void OnInventoryUpdated(List<ItemStack> itemStacks)
        {
            characterData.itemsInInventory = itemStacks.Select(i => new SavedInventoryItemData { itemId = i.item.ID, count = i.count }).ToList();
        }

        private void OnEquippmentItemsChanged(ItemEquipment newItem, ItemEquipment oldItem, ItemEquipment.EquipmentSlot itemEquipmentSlot)
        {
            var savedEquipmentItem = characterData.equippedItems[(int)itemEquipmentSlot];

            if (oldItem == null && newItem != null || oldItem != null && newItem != null)
            {
                savedEquipmentItem = new SavedEquippmentItemData();

                savedEquipmentItem.itemId = newItem.ID;

                if (newItem is ItemWeapon)
                    savedEquipmentItem.itemType = ((ItemWeapon)newItem).isTwoHandedWeapon ? 3 : 1;
                else
                    savedEquipmentItem.itemType = 2;

                if (newItem is ItemArmor)
                    savedEquipmentItem.objectName = ((ItemArmor)newItem).meshName;
                else
                    savedEquipmentItem.objectName = newItem.itemName;
            }
            else if (oldItem != null && newItem == null)
            {
                savedEquipmentItem = null;
            }
            else
            {
                savedEquipmentItem = null;
            }


            characterData.equippedItems[(int)itemEquipmentSlot] = savedEquipmentItem;
        }

        public void SaveAndStartNewGame(string heroName)
        {
            characterData = new PersistedCharacterData
            {
                heroClassID = selectedHeroClass.ID,
                heroClass= selectedHeroClass.className,
                heroName = heroName,
                currentExp = 0,
                currentLevel = 1,
                currentChapterId=1,
                currentDifficulty=1,
                currentTownStageId = 2,
                currentTownId=1,
                damage = selectedHeroClass.damage,
                defense = selectedHeroClass.defense,
                dexterity = selectedHeroClass.dexterity,
                energy = selectedHeroClass.energy,
                playerGold = selectedHeroClass.startGold,
                itemsInInventory = selectedHeroClass.initialInventoryItems.Select(i => new SavedInventoryItemData { itemId = i.item.ID, count = i.count }).ToList(),
                equippedItems = selectedHeroClass.initialEquippedItems
                .Select(item => item != null ? new SavedEquippmentItemData { itemId = item.ID, itemType = item is ItemWeapon ? (((ItemWeapon)item).isTwoHandedWeapon ? 3 : 1) : item is ItemShield ? 2 : 0, objectName = item.itemName } : null)
                .ToArray(),
                itemsInStash=new List<SavedInventoryItemData>(),
                magicPower = selectedHeroClass.magicPower,
                magicResist = selectedHeroClass.magicResist,
                maxHP = selectedHeroClass.HP,
                maxMP = selectedHeroClass.MP,
                maxStamina = selectedHeroClass.stamina,
                reachedDifficulty = 1,
                remainingHP = selectedHeroClass.HP,
                remainingMP = selectedHeroClass.MP,
                remianingStamina = selectedHeroClass.stamina,
                strength = selectedHeroClass.strength,
                vitality = selectedHeroClass.vitality,
                magic = selectedHeroClass.magic
            };

            GamePersistenceUtils.SaveGame(characterData);

            LoadingScreenManager.Instance.LoadGameScene(2);
        }

        public void SaveGame()
        {
            GamePersistenceUtils.SaveGame(characterData);
        }

        public void SetHeroClassSelected(HeroClass heroClass)
        {
            selectedHeroClass = heroClass;
        }

        public void SetHeroClassDeselected()
        {
            selectedHeroClass = null;
        }

        public void LoadGame(PersistedCharacterData characterData)
        {
            this.characterData = characterData;

            foreach (var heroClass in availableHeroClasses)
            {
                if (heroClass.ID == characterData.heroClassID)
                {
                    selectedHeroClass = heroClass;
                    break;
                }

            }

            if (!selectedHeroClass)
            {
                Debug.LogError("Hero class has not been selected. Something went wrong here");
                return;
            }

            LoadingScreenManager.Instance.LoadGameScene(characterData.currentTownStageId);
        }

        public TItem GetItemById<TItem>(int id) where TItem : Item
        {
            foreach (var item in availableItems)
            {
                if (item.ID == id && item is TItem)
                    return (TItem)item;
            }

            return null;
        }
    }
}

