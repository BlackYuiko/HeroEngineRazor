using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace HeroEngine.Core.Managers
{
    public static class AbilityManager
    {
        private static readonly AbilityRepository _repository = new AbilityRepository();
        private static List<Ability> _abilitiesCache = new List<Ability>();

        public static void Initialize()
        {
            _abilitiesCache = _repository.LoadAll();
        }

        public static void AddAbility(Ability ability)
        {
            // Guardar en memoria
            _abilitiesCache.Add(ability);
            // Guardar en JSON (El repo ya comprueba si existe)
            _repository.Add(ability);
        }

        public static List<Ability> GetAbilities()
        {
            if (_abilitiesCache.Count == 0) Initialize();
            return _abilitiesCache;
        }
    }
}