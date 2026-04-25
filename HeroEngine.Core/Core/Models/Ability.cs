using HeroEngine.Core.Enums;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Provides functionality of abilities
    /// </summary>
    public class Ability : IAbility
    {
        public string Name { get; }
        public int ManaCost { get; }
        public AbilityType Type { get; }
        public AbilityRarity Rarity { get; }

        public Ability(string name, int manaCost, AbilityType type, AbilityRarity rarity)
        {
            Name = name;
            ManaCost = manaCost;
            Type = type;
            Rarity = rarity;
        }

        /// <summary>
        /// Executes the ability effect based on its type and rarity.
        /// </summary>
        /// <param name="hero">The hero casting the ability.</param>
        /// <param name="target">The target affected by the ability.</param>
        public void Use(AHeroes hero, ICombatant target) 
        {
            double multiplier = GetRarityMultiplier(Rarity);

            switch (Type)
            {
                case AbilityType.Attack:
                    int attack = (int)(UIConfig.Ability.BaseAttack * multiplier);
                    hero.AbilityAttack(attack, target);
                    break;

                case AbilityType.Healing:
                    int heal = (int)(UIConfig.Ability.BaseHeal * multiplier);
                    hero.AbilityHeal(heal);
                    break;

                case AbilityType.Defense:
                    int defense = (int)(UIConfig.Ability.BaseDefense * multiplier);
                    hero.AbilityDefense(defense);
                    break;

                case AbilityType.Support:
                    string msgSupport = UIConfig.Ability.MsgSupport;
                    hero.AbilitySupport(msgSupport);
                    break;
            }
        }

        /// <summary>
        /// Calculates the multiplier applied to an ability based on its rarity.
        /// </summary>
        /// <param name="rarity">The rarity level of the ability.</param>
        /// <returns>A multiplier value used to scale ability effects.</returns>
        public static double GetRarityMultiplier(AbilityRarity rarity)
        {
            return rarity switch
            {
                AbilityRarity.Common => UIConfig.Ability.RarityCommonMultiplier,
                AbilityRarity.Rare => UIConfig.Ability.RarityRareMultiplier,
                AbilityRarity.Epic => UIConfig.Ability.RarityEpicMultiplier,
                AbilityRarity.Legendary => UIConfig.Ability.RarityLegendaryMultiplier,
                _ => UIConfig.Ability.RarityBaseMultiplier
            };
        }

        public void AbilityPresentation()
        {
            CombatLogger.AddLog($"==={Name}===");
            CombatLogger.AddLog($"Type: {Type}, Rarity: {Rarity}, ManaCost: {ManaCost}");
        }
    }
}
