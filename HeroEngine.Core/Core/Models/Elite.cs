using HeroEngine.Core.Managers;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents an elite enemy with enhanced stats compared to a minion.
    /// Acts as a mid-tier opponent in combat encounters.
    /// </summary>
    public class Elite : AEnemies
    {
        public Elite(string name) : base(name)
        {
            var config = ConfigManager.LoadConfig();

            MaxHP = (int)(UIConfig.Elite.EliteBaseHP * config.EnemyHpMultiplier);
            CurrentHP = MaxHP;
            Damage = UIConfig.Elite.EliteBaseDmg;
        }

        public override int GetInitiative()
        {
            return UIConfig.Elite.EliteBaseIniciative;
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            BasicAttack(target);
        }
    }
}
