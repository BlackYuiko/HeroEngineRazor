using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using System;

namespace HeroEngine.UI
{
    /// <summary>
    /// Provides functionality to prepare and launch combat encounters between heroes and enemies.
    /// </summary>
    public static class CharacterMenu
    {
        /// <summary>
        /// Initializes a combat encounter by selecting available heroes and generating enemies.
        /// Starts the combat loop using CombatService.
        /// </summary>
        public static void BringCharacterToFight()
        {
            var heroes = HeroManager.GetHeroes();

            if (heroes.Count == 0)
            {
                Console.WriteLine(UIConfig.General.NoHeroesToCombat);
                return;
            }

            var aliveHeroes = heroes.Where(h => h.CurrentHP > 0).ToList();

            if (aliveHeroes.Count == 0)
            {
                Console.WriteLine(UIConfig.General.HeroesIsDead);
                return;
            }

            var enemies = EnemyManager.GenerateEnemies(aliveHeroes.Count);

            CombatService.StartCombat(
                aliveHeroes.Cast<ICombatant>().ToList(),
                enemies.Cast<ICombatant>().ToList()
            );
        }
    }
}
