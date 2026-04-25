using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents a Mage hero class focused on magical abilities and high damage output.
    /// Relies on abilities rather than basic attacks for effectiveness.
    /// </summary>
    public class Mage : AHeroes, IAbilityUser
    {
        public int Mana { get; set; }

        public int CurrentMana { get; set; }
        private static int WeaponLevel { get; set; } = 5;

        private List<IAbility> abilities = new();

        public IReadOnlyList<IAbility> Abilities => abilities;

        public Mage(string name, int level) : base(name, level)
        {
            int baseHP = UIConfig.Mage.MageBaseHP;
            int baseMana = UIConfig.Mage.MageBaseMana;
            int baseDamage = UIConfig.Mage.MageBaseDmg;

            MaxHP = baseHP + (level * UIConfig.Mage.MageHPPerLevel);
            Mana = baseMana + (level * UIConfig.Mage.MageManaPerLevel);
            DmgAttack = baseDamage + (level * UIConfig.Mage.MageDmgPerLevel);

            CurrentHP = MaxHP;
            CurrentMana = Mana;
        }
        public void AddAbility(IAbility ability)
        {
            bool alreadyHasAbility = abilities.Any(a => a.Name == ability.Name);

            if (alreadyHasAbility)
            {
                CombatLogger.AddLog($"{Name} already has the ability {ability.Name}.");
                return;
            }

            abilities.Add(ability);
            CombatLogger.AddLog($"\n{ability.Name} assigned to {Name}.");
        }

        public void UseAbility(IAbility ability, ICombatant target)
        {
            if (CurrentMana < ability.ManaCost)
            {
                CombatLogger.AddLog(UIConfig.General.MsgNoMana);
                return;
            }

            CurrentMana -= ability.ManaCost;
            if (CurrentMana < 0)
                CurrentMana = 0;

            ability.Use(this, target);
            CombatLogger.AddLog($" {Name}'s new Mana: {CurrentMana}/{Mana}");
        }

        public override void Presentation()
        {
            CombatLogger.AddLog($"==={Name}'s STATS===");
            CombatLogger.AddLog($"Level: {Level}, HP: {CurrentHP}/{MaxHP}, Mana: {CurrentMana}/{Mana}, Damage: {DmgAttack}, Weapon Level: {WeaponLevel}");
            CombatLogger.AddLog(UIConfig.General.MsgAbilities);

            if (!abilities.Any())
            {
                CombatLogger.AddLog(UIConfig.General.MsgAbilitiesNone);
            }
            else
            {
                foreach (var ability in abilities.OrderByDescending(a => a.Rarity).ToList())
                {
                    CombatLogger.AddLog($" - {ability.Name} [{ability.Rarity}]");
                }
            }
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();
            CombatLogger.AddLog($"{Name}'s turn.");

            // LÓGICA AUTOMÁTICA (AUTO-BATTLER) EN LUGAR DE CONSOLE.READLINE
            if (this is IAbilityUser abilityUser && abilityUser.Abilities.Any())
            {
                var castableAbility = abilityUser.Abilities.FirstOrDefault(a => CurrentMana >= a.ManaCost);

                if (castableAbility != null)
                {
                    UseAbility(castableAbility, target);
                    return;
                }
            }
            
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
