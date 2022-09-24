using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WotN.Common.Managers;
using WotN.UI.Inventory;

namespace WotN.UI.Stash
{
    public class StashUI : MonoBehaviour
    {
        [SerializeField]
        private Transform inventoryParent;

        [SerializeField]
        private Text goldText;

        [SerializeField]
        private GameObject stashGoldWindow;

        [SerializeField]
        private TMPro.TMP_InputField stashGoldInput;

        [SerializeField]
        private AudioClip stashGoldClip;

        private ItemSlot[] itemSlots;

        void Start()
        {
            gameObject.SetActive(false);
            StashManager.Instance.onItemsUpdated += OnStashItemsUpdated;
            StashManager.Instance.onInitializedGold += OnGoldInitialized;
            StashManager.Instance.onStashedGold += OnGoldUpdated;
            StashManager.Instance.onStashClosed += CloseStashGoldWindow;
            itemSlots = inventoryParent.GetComponentsInChildren<ItemSlot>();
        }

        private void OnStashItemsUpdated(List<ItemStack> items)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (i < items.Count)
                {
                    itemSlots[i].AddItem(items[i]);
                }
                else
                {
                    itemSlots[i].ClearSlot();
                }
            }
        }

        public void OnGoldButtonClicked()
        {
            if (StashManager.Instance.IsStashOpened == true)
            {
                if (Keyboard.current.shiftKey.ReadValue() <= 0f)
                {
                    stashGoldInput.ActivateInputField();

                    StashManager.Instance.IsStashGoldWindowOpened = true;

                    int playerGold = PlayerManager.Instance.GetPlayerGold();

                    stashGoldInput.text = playerGold.ToString();
                    stashGoldWindow.SetActive(true);

                    AudioManager.Instance.PlayClickButton();
                }
                else
                {
                    int playerGold = PlayerManager.Instance.GetPlayerGold();

                    PlayerManager.Instance.SetPlayerGold(0);

                    StashManager.Instance.StashGold(playerGold);

                    AudioManager.Instance.PlayUISFX(stashGoldClip);
                }
            }
        }

        public void CloseStashGoldWindow()
        {
            StashManager.Instance.IsStashGoldWindowOpened = false;

            stashGoldInput.DeactivateInputField();

            stashGoldInput.text = "0";

            stashGoldWindow.SetActive(false);
        }

        public void StashGold()
        {
            string goldText = stashGoldInput.text;

            stashGoldInput.DeactivateInputField();

            int goldToStash = int.Parse(goldText);

            stashGoldInput.text = "0";

            int playerGold = PlayerManager.Instance.GetPlayerGold();

            PlayerManager.Instance.SetPlayerGold(playerGold- goldToStash);

            StashManager.Instance.StashGold(goldToStash);

            AudioManager.Instance.PlayUISFX(stashGoldClip);

            CloseStashGoldWindow();
        }

        public void OnValueChanged(string goldText)
        {
            int goldToStash = int.Parse(goldText);

            int playerGold = PlayerManager.Instance.GetPlayerGold();

            var effectiveGold = Mathf.Clamp(goldToStash, 0, playerGold);

            stashGoldInput.text= effectiveGold.ToString();
        }

        private void OnGoldInitialized(int gold)
        {
            goldText.text = gold.ToString();
        }

        private void OnGoldUpdated(int gold)
        {
            goldText.text = gold.ToString();
        }

        public void CloseStash()
        {
            PlayerManager.Instance.UnfocusPlayer();

            CloseStashGoldWindow();
        }
    }
}

