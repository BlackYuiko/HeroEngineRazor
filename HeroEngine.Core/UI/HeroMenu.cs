using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using System;

namespace HeroEngine.UI
{
    // <summary>
    /// Provides a console-based menu system for creating, listing, and managing heroes.
    /// Also handles hero-related configuration such as abilities assignment.
    /// </summary>
    public static class HeroMenu
    {
        /// /// <summary>
        /// Creates a new hero based on user input (type, name, and level) and registers it in the HeroManager.
        /// </summary>
        public static void CreateHero()
        {
            Console.WriteLine(UIConfig.HeroMenu.MenuWarrior);
            Console.WriteLine(UIConfig.HeroMenu.MenuMage);
            Console.WriteLine(UIConfig.HeroMenu.MenuRogue);
            Console.Write(UIConfig.HeroMenu.MenuChooseType);

            string? heroName;
            int heroLevel;

            if (int.TryParse(Console.ReadLine(), out int numberMenuInput))
            {
                if (numberMenuInput >= 1 && numberMenuInput <= 3)
                {
                    heroName = GetHeroName();

                    Console.Write(UIConfig.HeroMenu.MenuChooseLevel);

                    if (int.TryParse(Console.ReadLine(), out int numberLevelInput) && numberLevelInput >= 2 && numberLevelInput <= 3)
                    {
                        heroLevel = numberLevelInput;
                    }
                    else
                    {
                        heroLevel = 1;
                    }

                    switch (numberMenuInput)
                    {
                        case 1:
                            HeroManager.AddHero(new Warrior(heroName, heroLevel));
                            break;

                        case 2:
                            HeroManager.AddHero(new Mage(heroName, heroLevel));
                            break;

                        case 3:
                            HeroManager.AddHero(new Rogue(heroName, heroLevel));
                            break;
                    }
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

        /// <summary>
        /// Displays all registered heroes in the system.
        /// </summary>
        public static void ListHeroes()
        {
            var heroes = HeroManager.GetHeroes();

            if (heroes.Count == 0)
            {
                Console.WriteLine(UIConfig.HeroMenu.NoHeroesToList);
            }
            else
            {
                foreach (var hero in heroes)
                {
                    Console.WriteLine();
                    hero.Presentation();
                }
            }
        }

        /// <summary>
        /// Allows the user to select a hero and assign an available ability to them.
        /// </summary>
        public static void ChooseHeroToAsignAbility()
        {
            var heroes = HeroManager.GetHeroes();

            var abilities = AbilityManager.GetAbilities();

            
            if (heroes.Count != 0 && abilities.Count != 0)
            {
                Console.WriteLine();
                for (int i = 0; i < heroes.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {heroes[i].Name}");
                }

                Console.Write(UIConfig.HeroMenu.ChooseHeroToAsignAbility);
                if (int.TryParse(Console.ReadLine(), out int numberHeroInput))
                {
                    if (numberHeroInput > 0 && numberHeroInput <= heroes.Count)
                    {
                        AbilityMenu.AsignAbilityToHero(heroes[numberHeroInput - 1]);
                        
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
                Console.WriteLine(UIConfig.HeroMenu.NoHeroesOrHabilities);
            }
        }

        private static string GetHeroName()
        {
            string? name;

            do
            {
                Console.Write(UIConfig.HeroMenu.MenuChooseName);
                name = Console.ReadLine();

                bool alreadyExists = HeroManager.GetHeroes().Any(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (alreadyExists)
                {
                    Console.WriteLine(UIConfig.Heroes.HeroAlreadyExist);
                    name = null;
                }

            } while (string.IsNullOrWhiteSpace(name));

            return name;
        }
    }
}
