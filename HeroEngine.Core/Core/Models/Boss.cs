using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;
namespace HeroEngine.Core.Models
{
    public class Boss : AEnemies
    {
        /// <summary>
        /// Represents a boss enemy with high health, damage, and combat impact.
        /// Serves as a high-difficulty challenge in combat scenarios.
        /// </summary>
        public Boss(string name) : base(name)
        {
            MaxHP = UIConfig.Boss.BossBaseHP;
            CurrentHP = MaxHP;
            Damage = UIConfig.Boss.BossBaseDmg;
        }

        public override int GetInitiative()
        {
            return UIConfig.Boss.BossBaseIniciative;
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            BasicAttack(target);
        }
    }
}
