using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents a Warrior hero class focused on high durability and melee combat.
    /// Specializes in strong basic attacks and frontline combat roles.
    /// </summary>
    public class Warrior : AHeroes
    {
        public int Armor;

        public Warrior(string name, int level) : base(name, level) 
        {
            var config = ConfigManager.LoadConfig();

            int baseHP = UIConfig.Warrior.WarriorBaseHP;
            int baseArmor = UIConfig.Warrior.WarriorBaseArmor;
            int baseDamage = UIConfig.Warrior.WarriorBaseDmg;

            MaxHP = baseHP + (int)(level * UIConfig.Warrior.WarriorHPPerLevel * config.LevelMultiplier);
            Armor = baseArmor + (level * UIConfig.Warrior.WarriorArmorPerLevel);
            DmgAttack = baseDamage + (int)(level * UIConfig.Warrior.WarriorDmgPerLevel * config.LevelMultiplier);

            CurrentHP = MaxHP;
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            BasicAttack(target);
        }

        public override void ReceiveDamage(int dmgReceived)
        {
            int netDmg = Math.Max(0, dmgReceived - Armor);
            CurrentHP -= netDmg;

            if (CurrentHP < 0)
                CurrentHP = 0;
        }
    }
}
