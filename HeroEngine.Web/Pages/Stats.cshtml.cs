using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using System;


namespace HeroEngine.Web.Pages
{
    public class StatsModel : PageModel
    {
        public Dictionary<string, (int Count, double Percentage)> HeroDistribution { get; set; } = new();

        public List<AHeroes> TopHeroes { get; set; } = new();

        public Dictionary<string, int> CommonAbilities { get; set; } = new();

        public List<CombatResult> CombatHistory { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string FilterResult { get; set; } = "All";

        public void OnGet()
        {
            var allCombatants = HeroManager.GetHeroes().Cast<ICombatant>().ToList();
            var heroes = allCombatants.OfType<AHeroes>().ToList();
            int totalHeroes = heroes.Count;

            if (totalHeroes > 0)
            {
                HeroDistribution = heroes
                    .GroupBy(h => h.GetType().Name)
                    .ToDictionary(
                        g => g.Key,
                        g => (Count: g.Count(), Percentage: Math.Round((double)g.Count() / totalHeroes * 100, 2))
                    );

                TopHeroes = HeroAnalytics.GetTopHeroesByLevel(allCombatants, 3);

                CommonAbilities = heroes
                    .SelectMany(h => h.Abilities)
                    .GroupBy(a => a.Name)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .ToDictionary(g => g.Key, g => g.Count());
            }

            var allCombats = HeroEngine.Core.Data.CsvStatsWriter.GetLast10Stats();

            if (FilterResult == "Victory")
            {
                CombatHistory = allCombats.Where(c => c.Result.Equals("Victory", StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (FilterResult == "Defeat")
            {
                CombatHistory = allCombats.Where(c => c.Result.Equals("Defeat", StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                CombatHistory = allCombats;
            }
        }
    }
}