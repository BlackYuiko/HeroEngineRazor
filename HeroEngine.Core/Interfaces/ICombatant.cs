using System;

namespace HeroEngine.Interfaces
{
    /// <summary>
    /// Represents a combat participant in the system.
    /// </summary>
    public interface ICombatant
    {
        string Name { get; }
        int MaxHP { get; }
        int CurrentHP { get; }
        bool IsAlive { get; }

        /// <summary>
        /// Calculates and returns the initiative value for turn order.
        /// </summary>
        /// <returns>An integer representing initiative.</returns>
        int GetInitiative();

        /// <summary>
        /// Executes the combatant's turn logic.
        /// </summary>
        /// <param name="allies">List of allied combatants.</param>
        /// <param name="enemies">List of enemy combatants.</param>
        void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies);

        /// <summary>
        /// Applies damage to the combatant.
        /// </summary>
        /// <param name="amount">The amount of damage received.</param>
        void ReceiveDamage(int amount);
    }
}
