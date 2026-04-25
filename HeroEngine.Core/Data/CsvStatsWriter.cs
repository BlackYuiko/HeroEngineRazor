using HeroEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeroEngine.Core.Data
{
    /// <summary>
    /// Handles appending and manually parsing combat statistics in a CSV format.
    /// </summary>
    public static class CsvStatsWriter
    {
        private static string FilePath => PathConfig.GetFilePath("combat_stats.csv");

        private const string CsvHeader = "Date,Heroes,Enemies,Result,TotalRounds,TotalDamageDealt,MostEffectiveHero";

        public static void AppendCombatStats(CombatResult result)
        {
            bool fileExists = File.Exists(FilePath);

            using StreamWriter writer = new StreamWriter(FilePath, append: true);

            if (!fileExists)
            {
                writer.WriteLine(CsvHeader);
            }

            string EscapeCsv(string text)
            {
                if (string.IsNullOrEmpty(text)) return "None";
                return text.Contains(',') ? $"\"{text}\"" : text;
            }

            string line = $"{result.Date:yyyy-MM-dd HH:mm:ss},{EscapeCsv(result.ParticipatingHeroes)},{EscapeCsv(result.ParticipatingEnemies)},{result.Result},{result.TotalRounds},{result.TotalDamageDealt},{EscapeCsv(result.MostEffectiveHero)}";

            writer.WriteLine(line);
        }

        public static List<CombatResult> GetLast10Stats()
        {
            var statsList = new List<CombatResult>();

            if (!File.Exists(FilePath))
            {
                return statsList;
            }

            string[] lines = File.ReadAllLines(FilePath);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                // FIX: Use our manual parser instead of a simple .Split(',')
                string[] columns = ParseCsvLine(lines[i]);

                if (columns.Length >= 7)
                {
                    // Use TryParse to avoid app crashes if data is corrupted
                    int.TryParse(columns[4], out int rounds);
                    int.TryParse(columns[5], out int damage);

                    statsList.Add(new CombatResult
                    {
                        Date = DateTime.Parse(columns[0]),
                        ParticipatingHeroes = columns[1],
                        ParticipatingEnemies = columns[2],
                        Result = columns[3],
                        TotalRounds = rounds,
                        TotalDamageDealt = damage,
                        MostEffectiveHero = columns[6]
                    });
                }
            }

            return statsList.OrderByDescending(s => s.Date).Take(10).ToList();
        }

        /// <summary>
        /// Manually parses a CSV line, respecting commas that are inside quotes.
        /// </summary>
        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var currentField = new StringBuilder();

            foreach (char c in line)
            {
                if (c == '\"')
                {
                    // Toggle quote state (don't add the quote to the final string)
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    // If we see a comma and we are NOT inside quotes, it's a column separator
                    result.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    // Otherwise, add the character to the current field
                    currentField.Append(c);
                }
            }

            // Add the very last field
            result.Add(currentField.ToString());

            return result.ToArray();
        }
    }
}