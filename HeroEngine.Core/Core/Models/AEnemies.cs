using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents the base abstract class for all enemy characters.
    /// Provides shared enemy behavior such as basic attacks and initiative calculation.
    /// </summary>
    public abstract class AEnemies : ACharacters
    {
        protected int Damage { get; set; }

        protected AEnemies(string name) : base(name) {}

        public override int GetInitiative()
        {
            return Damage;
        }

        /// <summary>
        /// Performs a basic attack against a target, dealing damage.
        /// </summary>
        /// <param name="target">The target receiving the attack.</param>
        protected virtual void BasicAttack(ICombatant target)
        {
            target.ReceiveDamage(Damage);
            CombatLogger.AddLog($"[ENEMY] {Name} > Basic attack > {target.Name} > {Damage} dmg  " +
                $"|  {target.Name} HP: {target.CurrentHP}/{target.MaxHP}");
            
        }

    }
}
