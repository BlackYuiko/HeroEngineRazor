using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System.Text.Json.Serialization;

namespace HeroEngine.Core.Models
{
    public class Mage : AHeroes, IAbilityUser
    {
        public int Mana { get; set; }
        public int CurrentMana { get; set; }
        public static int WeaponLevel { get; set; } = 5;

        [JsonIgnore]
        IReadOnlyList<IAbility> IAbilityUser.Abilities => this.Abilities.Cast<IAbility>().ToList();

        [JsonConstructor]
        public Mage(string name, int level) : base(name, level)
        {
            var config = ConfigManager.LoadConfig();
            int baseHP = UIConfig.Mage.MageBaseHP;
            int baseMana = UIConfig.Mage.MageBaseMana;
            int baseDamage = UIConfig.Mage.MageBaseDmg;

            MaxHP = baseHP + (int)(level * UIConfig.Mage.MageHPPerLevel * config.LevelMultiplier);
            Mana = baseMana + (int)(level * UIConfig.Mage.MageManaPerLevel * config.LevelMultiplier);
            DmgAttack = baseDamage + (int)(level * UIConfig.Mage.MageDmgPerLevel * config.LevelMultiplier);

            CurrentHP = MaxHP;
            CurrentMana = Mana;
        }

        public void AddAbility(IAbility ability)
        {
            if (ability is Ability concreteAbility)
            {
                if (!this.Abilities.Any(a => a.Name == concreteAbility.Name))
                {
                    this.Abilities.Add(concreteAbility);
                }
            }
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

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            if (this.Abilities.Any())
            {
                var castableAbility = this.Abilities.FirstOrDefault(a => CurrentMana >= a.ManaCost);

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