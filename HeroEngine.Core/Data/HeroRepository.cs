using HeroEngine.Core.Models;
using System.Text.Json;

namespace HeroEngine.Core.Data
{
    /// <summary>
    /// Handles the persistence of Hero objects using JSON format.
    /// </summary>
    public class HeroRepository
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public HeroRepository()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        private string FilePath => PathConfig.GetFilePath("heroes.json");

        /// <summary>
        /// Reads all heroes from the JSON file.
        /// </summary>
        public List<AHeroes> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new List<AHeroes>();
            }

            string json = File.ReadAllText(FilePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<AHeroes>();

            return JsonSerializer.Deserialize<List<AHeroes>>(json, _jsonOptions) ?? new List<AHeroes>();
        }

        /// <summary>
        /// Overwrites the JSON file with the provided list of heroes.
        /// </summary>
        public void SaveAll(IEnumerable<AHeroes> heroes)
        {
            string json = JsonSerializer.Serialize(heroes, _jsonOptions);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Adds a new hero to the file if it doesn't already exist.
        /// </summary>
        public void Add(AHeroes hero)
        {
            var heroes = LoadAll();

            if (!heroes.Any(h => h.Name.Equals(hero.Name, System.StringComparison.OrdinalIgnoreCase)))
            {
                heroes.Add(hero);
                SaveAll(heroes);
            }
        }

        /// <summary>
        /// Deletes a hero from the file by name.
        /// </summary>
        public void Delete(string name)
        {
            var heroes = LoadAll();
            var heroToRemove = heroes.FirstOrDefault(h => h.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (heroToRemove != null)
            {
                heroes.Remove(heroToRemove);
                SaveAll(heroes);
            }
        }
    }
}