using HeroEngine.Interfaces;
using System;

namespace HeroEngine.Core.Managers
{
    /// <summary>
    /// Provides management functionality for abilities within the system.
    /// </summary>
    public static class AbilityManager
    {
        private static readonly List<IAbility> Abilities = new List<IAbility>();

        /// <summary>
        /// Adds a new ability to the global ability list.
        /// </summary>
        /// <param name="ability">The ability to be added.</param>
        public static void AddAbility(IAbility ability)
        {
            Abilities.Add(ability);
        }

        /// <summary>
        /// Retrieves the list of all registered abilities.
        /// </summary>
        /// <returns>A list containing all abilities.</returns>
        public static List<IAbility> GetAbilities()
        {
            return Abilities;
        }
    }
}