using HeroEngine.Core.Models;
using System;

namespace HeroEngine.Core.Managers
{
    /// <summary>
    /// Provides management functionality for hero entities.
    /// </summary>
    public static class HeroManager
    {
        private static readonly List<AHeroes> Heroes = new List<AHeroes>();

        /// <summary>
        /// Adds a new hero to the global hero list.
        /// </summary>
        /// <param name="hero">The hero to be added.</param>
        public static void AddHero(AHeroes hero)
        {
            Heroes.Add(hero);
        }

        /// <summary>
        /// Retrieves the list of all registered heroes.
        /// </summary>
        /// <returns>A list containing all heroes.</returns>
        public static List<AHeroes> GetHeroes()
        {
            return Heroes;
        }
    }
}
