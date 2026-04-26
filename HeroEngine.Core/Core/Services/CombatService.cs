using HeroEngine.Core.Managers;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;

namespace HeroEngine.Core.Services
{
    /// <summary>
    /// Handles the core combat loop logic between heroes and enemies.
    /// Responsible for turn order, round progression, and combat termination.
    /// </summary>
    public static class CombatService
    {
        /// <summary>
        /// Starts and manages a full combat encounter between heroes and enemies.
        /// Executes turn-based logic until one team is completely defeated or max rounds are reached.
        /// </summary>
        public static List<string> StartCombat(List<ICombatant> heroes, List<ICombatant> enemies)
        {
            CombatLogger.ClearLog();

            var config = ConfigManager.LoadConfig();

            if (heroes.Count > config.MaxHeroesPerBattle)
            {
                CombatLogger.AddLog($"[SYSTEM] Party size reduced to {config.MaxHeroesPerBattle} heroes due to game rules.");
                heroes = heroes.Take(config.MaxHeroesPerBattle).ToList();
            }

            var allCombatants = heroes.Concat(enemies).ToList();
            int round = 1;

            CombatLogger.AddLog(UIConfig.Combat.CombatStarts);

            while (heroes.Any(h => h.IsAlive) && enemies.Any(e => e.IsAlive) && round <= config.MaxCombatRounds)
            {
                CombatLogger.AddLog(UIConfig.Combat.CombatLines);
                CombatLogger.AddLog($"--- ROUND {round} / {config.MaxCombatRounds} ---");
                CombatLogger.AddLog(UIConfig.Combat.CombatLines);

                var turnOrder = allCombatants
                    .Where(c => c.IsAlive)
                    .OrderByDescending(c => c.GetInitiative())
                    .ToList();

                foreach (var combatant in turnOrder)
                {
                    if (!combatant.IsAlive) continue;

                    var allies = heroes.Contains(combatant)
                        ? heroes.Where(h => h.IsAlive).ToList<ICombatant>()
                        : enemies.Where(e => e.IsAlive).ToList<ICombatant>();

                    var targets = heroes.Contains(combatant)
                        ? enemies.Where(e => e.IsAlive).ToList<ICombatant>()
                        : heroes.Where(h => h.IsAlive).ToList<ICombatant>();

                    combatant.TakeTurn(allies, targets);

                    if (!heroes.Any(h => h.IsAlive) || !enemies.Any(e => e.IsAlive))
                        break;
                }

                round++;
            }

            if (round > config.MaxCombatRounds && heroes.Any(h => h.IsAlive) && enemies.Any(e => e.IsAlive))
            {
                CombatLogger.AddLog($"[SYSTEM] Combat reached the maximum of {config.MaxCombatRounds} rounds! The heroes ran out of time and were DEFEATED.");

                foreach (var hero in heroes.Where(h => h.IsAlive))
                {
                    hero.ReceiveDamage(999999);
                }
            }
            else
            {
                CombatLogger.AddLog(UIConfig.Combat.CombatFinished);
            }

            return CombatLogger.GetCurrentLog();
        }
    }
}