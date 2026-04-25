using HeroEngine.Core.Models;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Managers
{
    /// <summary>
    /// Provides functionality to generate and manage enemy entities.
    /// </summary>
    public static class EnemyManager
    {
        private static readonly List<AEnemies> enemies = new List<AEnemies>();

        /// <summary>
        /// Generates a list of enemies based on the number of heroes.
        /// </summary>
        /// <param name="heroCount">The number of heroes participating in combat.</param>
        /// <returns>A list of generated enemies.</returns>
        public static List<AEnemies> GenerateEnemies(int heroCount)
        {
            enemies.Clear();

            int enemyCount = Math.Max(1, heroCount);

            for (int i = 0; i < enemyCount; i++)
            {
                enemies.Add(new Minion($"{UIConfig.Enemy.EnemyMinion}{i + 1}"));
            }

            if (heroCount > 1)
            {
                enemies.Add(new Elite(UIConfig.Enemy.EnemyElite));
            }
            if (heroCount >= 3)
            {
                enemies.Add(new Boss(UIConfig.Enemy.EnemyBoss));
            }

            return enemies;
        }
    }
}
