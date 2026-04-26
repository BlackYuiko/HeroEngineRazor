using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace HeroEngine.Web.Pages
{
    public class CombatModel : PageModel
    {
        public string LastCombatLog { get; set; } = string.Empty;

        public void OnGet()
        {
            LastCombatLog = CombatLogger.ReadLastCombat();
        }

        public IActionResult OnPostSimulate()
        {
            var heroes = HeroManager.GetHeroes().Cast<ICombatant>().Where(h => h.IsAlive).ToList();

            if (heroes.Count == 0)
            {
                LastCombatLog = "You cant start de combat because all Heroes are dead!";
                return Page();
            }

            var config = ConfigManager.LoadConfig();
            bool sizeReduced = false;

            if (heroes.Count > config.MaxHeroesPerBattle)
            {
                heroes = heroes.Take(config.MaxHeroesPerBattle).ToList();
                sizeReduced = true;
            }

            var enemies = EnemyManager.GenerateEnemies(heroes.Count).Cast<ICombatant>().ToList();

            string heroNames = string.Join(", ", heroes.Select(h => h.Name));
            string enemyNames = string.Join(", ", enemies.Select(e => e.Name));

            string participants = $"{heroNames} VS {enemyNames}";
            if (sizeReduced)
            {
                participants += $" (Party reduced to {config.MaxHeroesPerBattle} by rules)";
            }

            List<string> combatLogs = CombatService.StartCombat(heroes, enemies);

            bool heroesWon = heroes.Any(h => h.IsAlive);
            string finalResult = heroesWon ? "VICTORY! Heroes prevailed." : "DEFEAT! The enemies won.";

            CombatLogger.SaveCombatToFile(combatLogs, participants, finalResult);

            int totalRounds = combatLogs.Count(log => log.Contains("--- ROUND"));

            int damageByEnemies = heroes.Sum(h => h.MaxHP - Math.Max(0, h.CurrentHP));
            int damageByHeroes = enemies.Sum(e => e.MaxHP - Math.Max(0, e.CurrentHP));
            int totalDamage = damageByEnemies + damageByHeroes;

            string mvp = "None";
            if (heroesWon && heroes.Any(h => h.IsAlive))
            {
                mvp = heroes.Where(h => h.IsAlive).OrderByDescending(h => h.CurrentHP).First().Name;
            }
            else if (!heroesWon && enemies.Any(e => e.IsAlive))
            {
                mvp = enemies.Where(e => e.IsAlive).OrderByDescending(e => e.CurrentHP).First().Name;
            }

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

            LastCombatLog = CombatLogger.ReadLastCombat();

            return Page();
        }
    }
}