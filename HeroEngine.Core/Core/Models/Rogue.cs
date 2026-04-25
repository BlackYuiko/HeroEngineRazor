using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents a Rogue hero class focused on high damage and crit.
    /// </summary>
    public class Rogue : AHeroes
    {
        private double DmgMultiplier { get; set; }
        private static int HiddenWeapons { get; set; } = 3;

        public Rogue(string name, int level) : base(name, level)
        {
            int baseHP = UIConfig.Rogue.RogueBaseHP;
            int baseDamage = UIConfig.Rogue.RogueBaseDmg;
            double baseCritDamage = UIConfig.Rogue.RogueBaseDmgMultiplier;

            MaxHP = baseHP + (level * UIConfig.Rogue.RogueHPPerLevel);
            DmgAttack = baseDamage + (level * UIConfig.Rogue.RogueDmgPerLevel);
            DmgMultiplier = baseCritDamage + (level * UIConfig.Rogue.RogueDmgMultiplierPerLevel);

            CurrentHP = MaxHP;
        }

        public override void Presentation()
        {
            CombatLogger.AddLog($"{Name}' turn.");
            CombatLogger.AddLog($"Level: {Level}, HP: {CurrentHP}/{MaxHP}, Damage: {DmgAttack} DmgMultiplier: {Math.Round(DmgMultiplier, 2)}, Hidden Weapons: {HiddenWeapons}");
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            BasicAttack(target);
        }

        public override void ReceiveDamage(int dmgReceived)
        {
            CurrentHP -= dmgReceived;
            if (CurrentHP < 0)
                CurrentHP = 0;
        }
    }
}
