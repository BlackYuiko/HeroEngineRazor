using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using HeroEngine.Core.Models;

namespace HeroEngine.Core.Data
{
    /// <summary>
    /// Handles the persistence of Ability objects using JSON format.
    /// </summary>
    public class AbilityRepository
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public AbilityRepository()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        private string FilePath => PathConfig.GetFilePath("abilities.json");

        /// <summary>
        /// Reads all abilities from the JSON file.
        /// </summary>
        public List<Ability> LoadAll()
        {
            if (!File.Exists(FilePath)) return new List<Ability>();

            string json = File.ReadAllText(FilePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<Ability>();

            return JsonSerializer.Deserialize<List<Ability>>(json, _jsonOptions) ?? new List<Ability>();
        }

        /// <summary>
        /// Overwrites the JSON file with the provided list of abilities.
        /// </summary>
        public void SaveAll(IEnumerable<Ability> abilities)
        {
            string json = JsonSerializer.Serialize(abilities, _jsonOptions);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Adds a new ability to the global library if it doesn't exist.
        /// </summary>
        public void Add(Ability ability)
        {
            var abilities = LoadAll();

            if (!abilities.Any(a => a.Name.Equals(ability.Name, System.StringComparison.OrdinalIgnoreCase)))
            {
                abilities.Add(ability);
                SaveAll(abilities);
            }
        }
    }
}