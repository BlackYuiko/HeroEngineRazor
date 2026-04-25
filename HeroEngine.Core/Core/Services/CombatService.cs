using HeroEngine.Core.Models;
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
        /// Executes turn-based logic until one team is completely defeated.
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="enemies"></param>
        public static List<string> StartCombat(List<ICombatant> heroes, List<ICombatant> enemies)
        {
            // Limpiamos los logs de combates anteriores
            CombatLogger.ClearLogs();

            var allCombatants = heroes.Concat(enemies).ToList();
            int round = 1;

            CombatLogger.AddLog(UIConfig.Combat.CombatStarts);

            while (heroes.Any(h => h.IsAlive) && enemies.Any(e => e.IsAlive))
            {
                CombatLogger.AddLog(UIConfig.Combat.CombatLines);
                // Asumiendo que CombatBattleLog devuelve un string formateado o se le puede hacer string.Format
                CombatLogger.AddLog($"--- ROUND {round} ---");
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

                // ELIMINADO: Console.WriteLine(UIConfig.Combat.CombatPreesEnter);
                // ELIMINADO: Console.ReadLine(); // ¡En la web esto colgaría la página!
                round++;
            }

            CombatLogger.AddLog(UIConfig.Combat.CombatFinished);

            // Devolvemos todo el registro a la web
            return CombatLogger.GetLogs();
        }
    }
}
