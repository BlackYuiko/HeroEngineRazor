using HeroEngine.Core.Managers; // NECESARIO para leer el XML
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using HeroEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // Limpiamos los logs de combates anteriores
            CombatLogger.ClearLog();

            // 1. CARGAMOS LA CONFIGURACIÓN DEL JUEGO (XML)
            var config = ConfigManager.LoadConfig();

            // 2. APLICAMOS LÍMITE DE HÉROES
            if (heroes.Count > config.MaxHeroesPerBattle)
            {
                CombatLogger.AddLog($"[SYSTEM] Party size reduced to {config.MaxHeroesPerBattle} heroes due to game rules.");
                heroes = heroes.Take(config.MaxHeroesPerBattle).ToList();
            }

            var allCombatants = heroes.Concat(enemies).ToList();
            int round = 1;

            CombatLogger.AddLog(UIConfig.Combat.CombatStarts);

            // 3. APLICAMOS EL LÍMITE DE RONDAS AL BUCLE WHILE
            while (heroes.Any(h => h.IsAlive) && enemies.Any(e => e.IsAlive) && round <= config.MaxCombatRounds)
            {
                CombatLogger.AddLog(UIConfig.Combat.CombatLines);
                CombatLogger.AddLog($"--- ROUND {round} / {config.MaxCombatRounds} ---"); // Ahora muestra el límite
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

            /// 4. LÓGICA DE RESOLUCIÓN DEL COMBATE (Para saber por qué terminó)
            if (round > config.MaxCombatRounds && heroes.Any(h => h.IsAlive) && enemies.Any(e => e.IsAlive))
            {
                CombatLogger.AddLog($"[SYSTEM] Combat reached the maximum of {config.MaxCombatRounds} rounds! The heroes ran out of time and were DEFEATED.");

                // Forzamos la caída de los héroes por agotamiento de tiempo.
                // Así la página web y el CSV lo interpretarán correctamente como una derrota.
                foreach (var hero in heroes.Where(h => h.IsAlive))
                {
                    hero.ReceiveDamage(999999); // Un daño masivo para asegurar que IsAlive pase a false
                }
            }
            else
            {
                CombatLogger.AddLog(UIConfig.Combat.CombatFinished);
            }

            // Devolvemos todo el registro a la web
            return CombatLogger.GetCurrentLog();
        }
    }
}