using System;

namespace HeroEngine.Core.Models
{
    /// <summary>
    /// Represents the statistical outcome of a single combat session.
    /// </summary>
    public class CombatResult
    {
        public DateTime Date { get; set; }
        public string ParticipatingHeroes { get; set; } = string.Empty;
        public string ParticipatingEnemies { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty; // Victory / Defeat
        public int TotalRounds { get; set; }
        public int TotalDamageDealt { get; set; }
        public string MostEffectiveHero { get; set; } = string.Empty;
    }
}