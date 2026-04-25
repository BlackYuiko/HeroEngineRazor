using HeroEngine.Interfaces;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents the base abstract class for all combat characters.
    /// Implements shared properties and behaviors defined by ICombatant.
    /// </summary>
    public abstract class ACharacters : ICombatant
    {
        public string Name { get; protected set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; protected set; }

        protected ACharacters(string name)
        {
            Name = name;
        }

        public bool IsAlive => CurrentHP > 0;

        public virtual void ReceiveDamage(int amount)
        {
            CurrentHP -= amount;
            if (CurrentHP < 0) CurrentHP = 0;
        }

        public abstract int GetInitiative();

        public abstract void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies);
    }
}
