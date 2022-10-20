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

        [Header("Stash gold window")]
        [SerializeField]
        private GameObject stashGoldWindow;

        [SerializeField]
        private Text playerGoldText;

        [SerializeField]
        private TMPro.TMP_InputField stashGoldInput;

        [Header("Withdraw gold window")]
        [SerializeField]
        private GameObject withdrawGoldWindow;

        [SerializeField]
        private Text stashedGoldText;

        [SerializeField]
        private TMPro.TMP_InputField withdrawGoldInput;

        [Header("Other")]
        [SerializeField]
        private AudioClip goldClip;

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

        public void DisplayStashGoldWindow()
        {
            if (PlayerManager.Instance.GetPlayerGold() > 0)
            {
                if (StashManager.Instance.IsStashOpened == true)
                {
                    if (Keyboard.current.shiftKey.ReadValue() <= 0f)
                    {
                        stashGoldInput.Select();

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

                        AudioManager.Instance.PlayUISFX(goldClip);
                    }
                }
            }
        }

        public void DisplayWithdrawGoldWindow()
        {
            if (StashManager.Instance.GetStashedGold() > 0)
            {
                if (Keyboard.current.shiftKey.ReadValue() <= 0f)
                {
                    withdrawGoldInput.Select();

                    StashManager.Instance.IsWithdrawGoldWindowOpened = true;

                    int stashedGold = StashManager.Instance.GetStashedGold();

                    withdrawGoldInput.text = stashedGold.ToString();
                    withdrawGoldWindow.SetActive(true);

                    AudioManager.Instance.PlayClickButton();
                }
                else
                {
                    int stashedGold = StashManager.Instance.GetStashedGold();

                    StashManager.Instance.SetStashedGold(0);

                    PlayerManager.Instance.AddPlayerGold(stashedGold);

                    AudioManager.Instance.PlayUISFX(goldClip);
                }
            }
        }

        public void CloseWithdrawGoldWindow()
        {
            StashManager.Instance.IsWithdrawGoldWindowOpened = false;

            withdrawGoldInput.DeactivateInputField();

            withdrawGoldInput.text = "0";

            withdrawGoldWindow.SetActive(false);
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

            AudioManager.Instance.PlayUISFX(goldClip);

            CloseStashGoldWindow();
        }

        public void WithdrawGold()
        {
            string goldText = withdrawGoldInput.text;

            withdrawGoldInput.DeactivateInputField();

            int goldToWithdraw = int.Parse(goldText);

            withdrawGoldInput.text = "0";

            int stashedGold = StashManager.Instance.GetStashedGold();

            StashManager.Instance.SetStashedGold(stashedGold - goldToWithdraw);

            PlayerManager.Instance.AddPlayerGold(goldToWithdraw);

            AudioManager.Instance.PlayUISFX(goldClip);

            CloseWithdrawGoldWindow();
        }

        public void OnGoldToStashChanged(string goldText)
        {
            if (string.IsNullOrWhiteSpace(goldText))
                goldText = "0";

            int goldToStash = int.Parse(goldText);

            int playerGold = PlayerManager.Instance.GetPlayerGold();

            var effectiveGold = Mathf.Clamp(goldToStash, 0, playerGold);

            stashGoldInput.text= effectiveGold.ToString();
        }

        public void OnGoldToWithdrawChanged(string goldText)
        {
            if (string.IsNullOrWhiteSpace(goldText))
                goldText = "0";

            int goldToWithdraw = int.Parse(goldText);

            int stashedGold = StashManager.Instance.GetStashedGold();

            var effectiveGold = Mathf.Clamp(goldToWithdraw, 0, stashedGold);

            withdrawGoldInput.text = effectiveGold.ToString();
        }

        private void OnGoldInitialized(int gold)
        {
            stashedGoldText.text = gold.ToString();
        }

        private void OnGoldUpdated(int gold)
        {
            stashedGoldText.text = gold.ToString();
        }

        public void CloseStash()
        {
            PlayerManager.Instance.UnfocusPlayer();

            CloseStashGoldWindow();
        }
    }
}

