using System;

namespace HeroEngine.Interfaces
{
    /// <summary>
    /// Defines behavior for entities that can use abilities.
    /// </summary>
    public interface IAbilityUser
    {
        /// <summary>
        /// Gets the collection of abilities available to the user.
        /// </summary>
        IReadOnlyList<IAbility> Abilities { get; }

        /// <summary>
        /// Adds a new ability to the user.
        /// </summary>
        /// <param name="ability">The ability to add.</param>
        void AddAbility(IAbility ability);

        /// <summary>
        /// Uses a specified ability on a target.
        /// </summary>
        /// <param name="ability">The ability to use.</param>
        /// <param name="target">The target of the ability.</param>
        void UseAbility(IAbility ability, ICombatant target);
    }
}
