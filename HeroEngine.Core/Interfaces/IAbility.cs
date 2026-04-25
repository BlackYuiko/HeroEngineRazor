using HeroEngine.Core.Enums;
using HeroEngine.Core.Models;
using System;

namespace HeroEngine.Interfaces
{
    /// <summary>
    /// Defines the contract for all abilities in the system.
    /// </summary>
    public interface IAbility
    {
        string Name { get; }
        int ManaCost { get; }
        AbilityType Type { get; }

        AbilityRarity Rarity { get; }

        /// <summary>
        /// Executes the ability effect on a target.
        /// </summary>
        /// <param name="caster">The hero using the ability.</param>
        /// <param name="target">The target receiving the effect.</param>
        void Use(AHeroes caster, ICombatant target);

        /// <summary>
        /// Displays formatted information about the ability.
        /// </summary>
        void AbilityPresentation();
    }
}
