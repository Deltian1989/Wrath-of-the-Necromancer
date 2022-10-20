using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WotN.ScriptableObjects.Items;
using static WotN.ScriptableObjects.Items.ItemWeapon;

namespace WotN.UI.Utils
{
    public static class ItemHelper
    {

        private static StringBuilder sb = new StringBuilder();

        public static string GenerateItemDescription(Item item)
        {
            switch (item)
            {
                case ItemEquipment:
                    return SetTooltipDescription((ItemEquipment)item);
                default: 
                    return "Restores some HP/MP";
                    
            }
        }

        private static string SetTooltipDescription(ItemEquipment item)
        {
            if (sb.Length > 0)
            {
                sb.Clear();
            }

            if (item is ItemWeapon)
                SetStatsForWeaponItem(item as ItemWeapon);
            else if (item is ItemShield)
                SetStatsForShieldItem(item as ItemShield);
            else if (item is ItemArmor)
                SetStatsForArmorItem(item as ItemArmor);

            return sb.ToString();
        }

        private static void SetStatsForWeaponItem(ItemWeapon itemWeapon)
        {
            AddAttackSpeedStat(itemWeapon.attackSpeed);
            AddDamageStat(itemWeapon.minDamage, itemWeapon.maxDamage, itemWeapon.equipmentSlot);
            AddStat(itemWeapon.magicDamage, StatType.StatUp, "Magic damage");
            AddStat(itemWeapon.requiredLevel, StatType.RequiredLevel);
            AddStat(itemWeapon.requiredStrength, StatType.RequiredSkill, "strength");
            AddStat(itemWeapon.requiredDexterity, StatType.RequiredSkill, "dexterity");
            AddStat(itemWeapon.requiredMagic, StatType.RequiredSkill, "magic");
        }

        private static void SetStatsForShieldItem(ItemShield itemShield)
        {
            AddStat(itemShield.blockChance, StatType.BlockChance);
            AddStat(itemShield.shieldDefense, StatType.StatUp, "Shield defence");
            AddStat(itemShield.magicDefense, StatType.StatUp, "Magic resist");
            AddStat(itemShield.requiredLevel, StatType.RequiredLevel);
            AddStat(itemShield.requiredStrength, StatType.RequiredSkill, "strength");
            AddStat(itemShield.requiredDexterity, StatType.RequiredSkill, "dexterity");
            AddStat(itemShield.requiredMagic, StatType.RequiredSkill, "magic");
        }

        private static void SetStatsForArmorItem(ItemArmor itemArmor)
        {
            AddStat(itemArmor.armor, StatType.StatUp, "Defence");
            AddStat(itemArmor.magicResist, StatType.StatUp, "Magic resist");
            AddStat(itemArmor.requiredLevel, StatType.RequiredLevel);
            AddStat(itemArmor.requiredDexterity, StatType.RequiredSkill, "dexterity");
            AddStat(itemArmor.requiredMagic, StatType.RequiredSkill, "magic");
        }

        private static void AddStat(float value, StatType statType, string statName = null)
        {
            if (value != 0)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                switch (statType)
                {
                    case StatType.RequiredSkill:
                        sb.Append($"Required {statName}: {value}");
                        break;
                    case StatType.RequiredLevel:
                        sb.Append($"Required level: {value}");
                        break;
                    case StatType.BlockChance:
                        sb.Append($"Block chance : {value * 100}%");
                        break;
                    case StatType.StatUp:
                        sb.Append($"{statName}: +{value}");
                        break;
                    default:
                        break;
                }
            }
        }

        private static void AddAttackSpeedStat(AttackSpeed attackSpeed)
        {
            string swingSpeedText = null;

            switch (attackSpeed)
            {
                case AttackSpeed.VerySlow:
                    swingSpeedText = "Very slow swing speed";
                    break;
                case AttackSpeed.Slow:
                    swingSpeedText = "Slow swing speed";
                    break;
                case AttackSpeed.Normal:
                    swingSpeedText = "Normal swing speed";
                    break;
                case AttackSpeed.Fast:
                    swingSpeedText = "Fast swing speed";
                    break;
                case AttackSpeed.VeryFast:
                    swingSpeedText = "Very fast swing speed";
                    break;
                default:
                    break;
            }

            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(swingSpeedText);
        }

        private static void AddDamageStat(float minValue, float maxValue, ItemEquipment.EquipmentSlot equipmentSlot)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
            {
                sb.Append("One hand damage: ");
            }
            else if (equipmentSlot == ItemEquipment.EquipmentSlot.WeaponRightHand)
            {
                sb.Append("Two hands damage: ");
            }

            if (minValue == maxValue)
            {
                sb.Append($"{minValue}.");
            }
            else if (maxValue > minValue)
            {
                sb.Append($"{minValue}-{maxValue}.");
            }
        }

        public enum DisplayMode
        {
            Inventory,
            InventoryToStash,
            Equipment,
            EquipmentToStash,
            Stash
        }

        public enum UIElementType
        {
            NormalItem,
            EquipmentItem,
            PlayerGold
        }

        private enum StatType
        {
            RequiredSkill,
            RequiredLevel,
            BlockChance,
            StatUp
        }
    }
}

