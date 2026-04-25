using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HeroEngine.Core.Models
{
    public class Mage : AHeroes, IAbilityUser
    {
        public int Mana { get; set; }
        public int CurrentMana { get; set; }
        public static int WeaponLevel { get; set; } = 5;

        // 1. EL TRUCO MAESTRO: Implementación explícita de la interfaz.
        // Esto complace a 'IAbilityUser' sin crear una propiedad que confunda al JSON 
        // ni choque con la lista pública 'Abilities' de 'AHeroes'.
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
                // 2. Usamos directamente la lista heredada de la clase base (AHeroes)
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

        public override void Presentation()
        {
            CombatLogger.AddLog($"==={Name}'s STATS===");
            CombatLogger.AddLog($"Level: {Level}, HP: {CurrentHP}/{MaxHP}, Mana: {CurrentMana}/{Mana}, Damage: {DmgAttack}, Weapon Level: {WeaponLevel}");
            CombatLogger.AddLog(UIConfig.General.MsgAbilities);

            // 3. Leemos directamente la lista heredada para mostrar las habilidades
            if (!this.Abilities.Any())
            {
                CombatLogger.AddLog(UIConfig.General.MsgAbilitiesNone);
            }
            else
            {
                foreach (var ability in this.Abilities.OrderByDescending(a => a.Rarity).ToList())
                {
                    CombatLogger.AddLog($" - {ability.Name} [{ability.Rarity}]");
                }
            }
        }

        public override void TakeTurn(List<ICombatant> allies, List<ICombatant> enemies)
        {
            if (!enemies.Any()) return;

            var target = enemies.First();

            // 4. Comprobamos la lista heredada para la lógica del auto-batallador
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