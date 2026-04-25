using HeroEngine.Core.Enums;
using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using System;

namespace HeroEngine.UI
{
    /// <summary>
    /// Provides a console-based menu system for creating, listing, assigning, and using abilities.
    /// </summary>
    public static class AbilityMenu
    {
        /// <summary>
        /// Creates a new ability by requesting user input and registering it in the AbilityManager.
        /// </summary>
        public static void CreateAbility()
        {
            string abilityName = GetAbilityName();
            int typeInput = GetAbilityTypeInput();
            int rarityInput = GetAbilityRarityInput();

            AbilityType type = MapAbilityType(typeInput);
            AbilityRarity rarity = MapAbilityRarity(rarityInput);

            int manaCost = (int)(UIConfig.AbilityMenu.BaseManaCost * Ability.GetRarityMultiplier(rarity));

            AbilityManager.AddAbility(new Ability(abilityName, manaCost, type, rarity));
        }

        /// <summary>
        /// Displays a list of all available abilities sorted by rarity.
        /// </summary>
        public static void ListAbilities()
        {
            var abilities = AbilityManager.GetAbilities();

            if (abilities.Count == 0)
            {
                Console.WriteLine(UIConfig.AbilityMenu.NoAbilitiesToList);
            }
            else
            {
                foreach (var ability in abilities.OrderByDescending(a => a.Rarity).ToList())
                {
                    Console.WriteLine();
                    ability.AbilityPresentation();
                }
            }
        }

        /// <summary>
        /// Assigns an available ability from the system to a hero.
        /// </summary>
        /// <param name="hero">The hero receiving the ability.</param>
        public static void AsignAbilityToHero(AHeroes hero)
        {
            if (hero is IAbilityUser abilityUser)
            {
                var abilities = AbilityManager.GetAbilities();

                Console.WriteLine();
                for (int i = 0; i < abilities.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {abilities[i].Name}");
                }

                Console.Write(UIConfig.AbilityMenu.ChooseAbilityToAsign);
                if (int.TryParse(Console.ReadLine(), out int numberAbilityInput))
                {
                    if (numberAbilityInput > 0 && numberAbilityInput <= abilities.Count)
                    {
                        abilityUser.AddAbility(abilities[numberAbilityInput - 1]);
                    }
                    else
                    {
                        Console.WriteLine(UIConfig.General.ErrorNumberInput);
                    }
                }
                else
                {
                    Console.WriteLine(UIConfig.General.ErrorNumberInput);
                }
                
            }
            else
            {
                Console.WriteLine(UIConfig.AbilityMenu.HeroCantHabilities);
            }
        }

        /// <summary>
        /// Allows a hero to select and use one of their abilities against a target in combat.
        /// </summary>
        /// <param name="hero">The hero using the ability.</param>
        /// <param name="target">The combat target affected by the ability.</param>
        public static void ChooseAbilityToFight(AHeroes hero, ICombatant target)
        {
            if (hero is not IAbilityUser abilityUser)
            {
                Console.WriteLine(UIConfig.AbilityMenu.CantUseHabilities);
                return;
            }

            var abilities = abilityUser.Abilities;

            if (abilities.Count == 0)
            {
                Console.WriteLine(UIConfig.AbilityMenu.NoAbilitiesEquiped);
                return;
            }

            Console.WriteLine();
            for (int i = 0; i < abilities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {abilities[i].Name}");
            }
            Console.Write(UIConfig.AbilityMenu.aChooseHability);

            if (!int.TryParse(Console.ReadLine(), out int choice) ||
                choice < 1 || choice > abilities.Count)
            {
                Console.WriteLine(UIConfig.General.ErrorNumberInput);
                return;
            }

            var selectedAbility = abilities[choice - 1];

            abilityUser.UseAbility(selectedAbility, target);
        }

        private static string GetAbilityName()
        {
            string? name;

            do
            {
                Console.Write(UIConfig.AbilityMenu.MenuAbilityName);
                name = Console.ReadLine();

                bool alreadyExists = AbilityManager.GetAbilities().Any(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (alreadyExists)
                {
                    Console.WriteLine(UIConfig.AbilityMenu.NameAlreadyExist);
                    name = null;
                }

            } while (string.IsNullOrWhiteSpace(name));

            return name;
        }

        private static int GetAbilityTypeInput()
        {
            int input;
            bool valid;

            do
            {
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityAttack);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityDefense);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityHealing);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilitySupport);
                Console.Write(UIConfig.AbilityMenu.MenuAbilityType);

                valid = int.TryParse(Console.ReadLine(), out input)
                        && input >= 1 && input <= 4;

                if (!valid)
                {
                    Console.WriteLine(UIConfig.General.ErrorNumberInput);
                }

            } while (!valid);

            return input;
        }

        private static int GetAbilityRarityInput()
        {
            int input;
            bool valid;

            do
            {
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityCommon);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityRare);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityEpic);
                Console.WriteLine(UIConfig.AbilityMenu.MenuAbilityLegendary);
                Console.Write(UIConfig.AbilityMenu.MenuAbilityRarity);

                valid = int.TryParse(Console.ReadLine(), out input)
                        && input >= 1 && input <= 4;

                if (!valid)
                {
                    Console.WriteLine(UIConfig.General.ErrorNumberInput);
                }

            } while (!valid);

            return input;
        }

        private static AbilityType MapAbilityType(int input)
        {
            return input switch
            {
                1 => AbilityType.Attack,
                2 => AbilityType.Defense,
                3 => AbilityType.Healing,
                4 => AbilityType.Support,
                _ => AbilityType.Attack
            };
        }

        private static AbilityRarity MapAbilityRarity(int input)
        {
            return input switch
            {
                1 => AbilityRarity.Common,
                2 => AbilityRarity.Rare,
                3 => AbilityRarity.Epic,
                4 => AbilityRarity.Legendary,
                _ => AbilityRarity.Common
            };
        }
    }
}
