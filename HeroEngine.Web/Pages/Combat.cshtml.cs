using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroEngine.Web.Pages
{
    public class CombatModel : PageModel
    {
        // Property to hold the log string read directly from the .txt file
        public string LastCombatLog { get; set; } = string.Empty;

        public void OnGet()
        {
            // Requirement 6.1: Load the last combat from the .txt file on page load
            LastCombatLog = CombatLogger.ReadLastCombat();
        }

        public IActionResult OnPostSimulate()
        {
            // 1. Obtenemos héroes vivos
            var heroes = HeroManager.GetHeroes().Cast<ICombatant>().Where(h => h.IsAlive).ToList();

            if (heroes.Count == 0)
            {
                LastCombatLog = "Error: No hay héroes vivos en la taberna para luchar.";
                return Page();
            }

            // --- NUEVO: APLICAMOS EL LÍMITE DE HÉROES AQUÍ ---
            var config = ConfigManager.LoadConfig();
            bool sizeReduced = false;

            if (heroes.Count > config.MaxHeroesPerBattle)
            {
                // Recortamos la lista antes de hacer nada más
                heroes = heroes.Take(config.MaxHeroesPerBattle).ToList();
                sizeReduced = true;
            }

            // Ahora generamos los enemigos basándonos SOLAMENTE en los héroes que van a luchar
            var enemies = EnemyManager.GenerateEnemies(heroes.Count).Cast<ICombatant>().ToList();

            // Los nombres para el CSV y TXT ahora serán 100% precisos
            string heroNames = string.Join(", ", heroes.Select(h => h.Name));
            string enemyNames = string.Join(", ", enemies.Select(e => e.Name));

            string participants = $"{heroNames} VS {enemyNames}";
            if (sizeReduced)
            {
                participants += $" (Party reduced to {config.MaxHeroesPerBattle} by rules)";
            }

            // 2. Iniciamos el combate
            List<string> combatLogs = CombatService.StartCombat(heroes, enemies);

            // 3. Evaluamos resultados (Si el combate acabó por timeout, los héroes tendrán 0 de vida por el apaño que hicimos)
            bool heroesWon = heroes.Any(h => h.IsAlive);
            string finalResult = heroesWon ? "VICTORY! Heroes prevailed." : "DEFEAT! The enemies won.";

            // 4. Guardamos en el TXT
            CombatLogger.SaveCombatToFile(combatLogs, participants, finalResult);

            // --- ESTADÍSTICAS CSV ---
            // Calculamos rondas contando cuántas veces aparece "--- ROUND" en el log
            int totalRounds = combatLogs.Count(log => log.Contains("--- ROUND"));

            // Calculamos el daño total (Vida Máxima - Vida Actual de todos los participantes tras el combate)
            int damageByEnemies = heroes.Sum(h => h.MaxHP - Math.Max(0, h.CurrentHP));
            int damageByHeroes = enemies.Sum(e => e.MaxHP - Math.Max(0, e.CurrentHP));
            int totalDamage = damageByEnemies + damageByHeroes;

            // Calculamos el MVP de forma segura (el que más vida retiene del equipo ganador)
            string mvp = "None";
            if (heroesWon && heroes.Any(h => h.IsAlive))
            {
                mvp = heroes.Where(h => h.IsAlive).OrderByDescending(h => h.CurrentHP).First().Name;
            }
            else if (!heroesWon && enemies.Any(e => e.IsAlive))
            {
                mvp = enemies.Where(e => e.IsAlive).OrderByDescending(e => e.CurrentHP).First().Name;
            }

            // Creamos el objeto y lo guardamos
            var combatStats = new CombatResult
            {
                Date = DateTime.Now,
                ParticipatingHeroes = heroNames,
                ParticipatingEnemies = enemyNames,
                Result = heroesWon ? "Victory" : "Defeat",
                TotalRounds = totalRounds,
                TotalDamageDealt = totalDamage,
                MostEffectiveHero = mvp
            };

            HeroEngine.Core.Data.CsvStatsWriter.AppendCombatStats(combatStats);

            // Refrescamos la UI leyendo el TXT guardado
            LastCombatLog = CombatLogger.ReadLastCombat();

            return Page();
        }
    }
}