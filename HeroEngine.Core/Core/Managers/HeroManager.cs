using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using System;

namespace HeroEngine.Core.Managers
{
    /// <summary>
    /// Provides management functionality for hero entities.
    /// </summary>
    public static class HeroManager
    {
        private static readonly HeroRepository _repository = new HeroRepository();
        private static List<AHeroes> _heroesCache = new List<AHeroes>();


        public static void Initialize()
        {
            _heroesCache = _repository.LoadAll();
        }

        /// <summary>
        /// Adds a new hero to the global hero list.
        /// </summary>
        /// <param name="hero">The hero to be added.</param>
        public static void AddHero(AHeroes hero)
        {
            _heroesCache.Add(hero);
            _repository.Add(hero);
        }

        /// <summary>
        /// Retrieves the list of all registered heroes.
        /// </summary>
        /// <returns>A list containing all heroes.</returns>
        public static List<AHeroes> GetHeroes()
        {
            if (_heroesCache.Count == 0)
            {
                Initialize();
            }
            return _heroesCache;
        }

        public static void DeleteHero(string name)
        {
            _heroesCache.RemoveAll(h => h.Name == name);
            _repository.Delete(name); // Deletes from JSON
        }

        /// <summary>
        /// Equips an ability to a specific hero and updates the JSON file.
        /// </summary>
        public static void EquipAbilityToHero(string heroName, Ability newAbility)
        {
            var hero = _heroesCache.FirstOrDefault(h => h.Name.Equals(heroName, StringComparison.OrdinalIgnoreCase));

            if (hero != null)
            {
                if (hero.Abilities == null) hero.Abilities = new List<Ability>();

                if (!hero.Abilities.Any(a => a.Name == newAbility.Name))
                {
                    hero.Abilities.Add(newAbility);
                }

                _repository.SaveAll(_heroesCache);
            }
        }
    }
}
