using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;
using System.Text.Json.Serialization;

namespace HeroEngine.Core.Models
{
    [JsonDerivedType(typeof(Warrior), typeDiscriminator: "Warrior")]
    [JsonDerivedType(typeof(Mage), typeDiscriminator: "Mage")]
    [JsonDerivedType(typeof(Rogue), typeDiscriminator: "Rogue")]

    /// <summary>
    /// Represents the base abstract class for all hero characters.
    /// Provides common behavior and ability-related actions.
    /// </summary>
    public abstract class AHeroes : ACharacters
    {
        // FIX: These must be public for System.Text.Json to map them to the constructor parameters!
        public int Level { get; set; }
        public int DmgAttack { get; set; }
        public List<Ability> Abilities { get; set; } = new List<Ability>();

        // We use [JsonConstructor] to tell the deserializer to use this specific constructor
        [JsonConstructor]
        protected AHeroes(string name, int level) : base(name)
        {
            Level = level;
        }

        /// <summary>
        /// Displays the hero information in a formatted way.
        /// </summary>
        public abstract void Presentation();

        /// <summary>
        /// Performs a basic attack against a target, dealing damage.
        /// </summary>
        protected virtual void BasicAttack(ICombatant target)
        {
            target.ReceiveDamage(DmgAttack);
            CombatLogger.AddLog($"[HERO] {Name} > Basic attack > {target.Name} > {DmgAttack} dmg  " +
                $"|  {target.Name} HP: {target.CurrentHP}/{target.MaxHP}");
        }

        /// <summary>
        /// Heals the hero by a specified amount without exceeding maximum health.
        /// </summary>
        public virtual void AbilityHeal(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
            CombatLogger.AddLog($"[HERO] {Name} > Ability > {Name} > Heal {amount}  |  {Name} HP: {CurrentHP}/{MaxHP}");
        }

        /// <summary>
        /// Applies a support effect, typically displaying a message to assist allies.
        /// </summary>
        public virtual void AbilitySupport(string msgSupport)
        {
            CombatLogger.AddLog($"[HERO] {Name} > Ability > Support all the team with a message: {msgSupport}");
        }

        /// <summary>
        /// Deals damage to a target using an ability.
        /// </summary>
        public virtual void AbilityAttack(int amount, ICombatant target)
        {
            target.ReceiveDamage(amount);
            CombatLogger.AddLog($"[HERO] {Name} > Ability > {target.Name} > {amount} dmg  " +
                $"|  {target.Name} HP: {target.CurrentHP}/{target.MaxHP}");
        }

        /// <summary>
        /// Increases the hero's effective survivability by restoring health as defense.
        /// </summary>
        public virtual void AbilityDefense(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
            CombatLogger.AddLog($"[HERO] {Name} > Ability > {Name} > Heal {amount}  |  {Name} HP: {CurrentHP}/{MaxHP}");
        }

        public override int GetInitiative()
        {
            return UIConfig.Heroes.HeroesBaseIniciative;
        }
    }
}