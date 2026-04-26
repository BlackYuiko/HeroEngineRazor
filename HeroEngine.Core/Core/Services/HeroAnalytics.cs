using HeroEngine.Core.Enums;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using System.Text.RegularExpressions;

namespace HeroEngine.Core.Services
{
    public static class HeroAnalytics
    {
        /// <summary>
        /// Retorna los 'n' héroes de nivel más alto.
        /// </summary>
        public static List<AHeroes> GetTopHeroesByLevel(List<ICombatant> combatants, int n)
        {
            return combatants
                .OfType<AHeroes>()
                .OrderByDescending(h => h.Level)
                .Take(n)
                .ToList();
        }

        /// <summary>
        /// Retorna todas las habilidades de una rareza dada de todos los héroes.
        /// </summary>
        public static List<Ability> GetAbilitiesByRarity(List<ICombatant> combatants, AbilityRarity rarity)
        {
            return combatants
                .OfType<AHeroes>()
                .SelectMany(h => h.Abilities)
                .Where(a => a.Rarity == rarity)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Héroes que tienen al menos 'min' habilidades.
        /// </summary>
        public static List<AHeroes> GetHeroesWithAbilityCount(List<ICombatant> combatants, int min)
        {
            return combatants
                .OfType<AHeroes>()
                .Where(h => h.Abilities != null && h.Abilities.Count >= min)
                .ToList();
        }

        /// <summary>
        /// Diccionario con el daño medio por clase de héroe.
        /// </summary>
        public static Dictionary<string, double> GetAverageDamagePerClass(List<ICombatant> combatants)
        {
            return combatants
                .OfType<AHeroes>()
                .GroupBy(h => h.GetType().Name)
                .ToDictionary(
                    group => group.Key,
                    group => group.Average(h => h.DmgAttack)
                );
        }

        /// <summary>
        /// Búsqueda de héroes por expresión regular sobre el nombre.
        /// </summary>
        public static List<AHeroes> SearchHeroesByName(List<ICombatant> combatants, string pattern)
        {
            return combatants
                .OfType<AHeroes>()
                .Where(h => Regex.IsMatch(h.Name, pattern, RegexOptions.IgnoreCase))
                .ToList();
        }
    }
}