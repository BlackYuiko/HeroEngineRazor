using HeroEngine.Core.Data;
using HeroEngine.Core.Models;

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
            _abilitiesCache.Add(ability);
            _repository.Add(ability);
        }

        public static List<Ability> GetAbilities()
        {
            if (_abilitiesCache.Count == 0) Initialize();
            return _abilitiesCache;
        }
    }
}