using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;
using System.Collections.Generic;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents a basic enemy unit with low health and damage.
    /// Used as the primary disposable combatant in encounters.
    /// </summary>
    public class Minion : AEnemies
    {
        public Minion(string name) : base(name)
        {
            MaxHP = UIConfig.Minion.MinionBaseHP;
            CurrentHP = MaxHP;
            Damage = UIConfig.Minion.MinionBaseDmg;
        }

        public override int GetInitiative()
        {
            return UIConfig.Minion.MinionBaseIniciative;
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            BasicAttack(target);
        }
    }
}
