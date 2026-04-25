using HeroEngine.Core.Managers;
using HeroEngine.UI;

namespace HeroEngine
{
    /// <summary>
    /// Entry point of the HeroEngine application.
    /// Handles the main menu loop and user navigation across the system.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main application entry point.
        /// Displays the main menu and routes user input to the corresponding systems.
        /// </summary>
        public static void Main()
        {
            bool exit = false;

            Console.WriteLine(UIConfig.MainMenu.Welcome);

            do
            {
                Console.WriteLine(UIConfig.MainMenu.MenuHeroEngine);
                Console.WriteLine(UIConfig.MainMenu.MenuCreateHero);
                Console.WriteLine(UIConfig.MainMenu.MenuHeroStats);
                Console.WriteLine(UIConfig.MainMenu.MenuCreateAbility);
                Console.WriteLine(UIConfig.MainMenu.MenuAbilityStats);
                Console.WriteLine(UIConfig.MainMenu.MenuAsignAbilityToHero);
                Console.WriteLine(UIConfig.MainMenu.MenuCombat);
                Console.WriteLine(UIConfig.MainMenu.MenuExit);
                Console.Write(UIConfig.MainMenu.MenuChooseOption);

                if (int.TryParse(Console.ReadLine(), out int numberMenuInput))
                {
                    switch (numberMenuInput)
                    {
                        case 0:
                            exit = true;
                            Console.Write(UIConfig.MainMenu.GoodBye);
                            break;

                        case 1:
                            HeroMenu.CreateHero();
                            break;

                        case 2:
                            HeroMenu.ListHeroes();
                            break;

                        case 3:
                            AbilityMenu.CreateAbility();
                            break;

                        case 4:
                            AbilityMenu.ListAbilities();
                            break;
                        case 5:
                            HeroMenu.ChooseHeroToAsignAbility();
                            break;

                        case 6:
                            CharacterMenu.BringCharacterToFight();
                            break;

                        default:
                            Console.WriteLine(UIConfig.General.ErrorNumberInput);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(UIConfig.General.ErrorNumberInput);
                }
                Console.WriteLine();

            } while (!exit);
        }
    }
}
    


