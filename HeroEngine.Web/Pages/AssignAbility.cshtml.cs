using HeroEngine.Core.Managers;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages
{
    public class AssignAbilityModel : PageModel
    {
        public List<IAbilityUser> EligibleHeroes { get; set; } = new List<IAbilityUser>();
        public List<IAbility> AvailableAbilities { get; set; } = new List<IAbility>();

        [BindProperty]
        public string SelectedHeroName { get; set; }

        [BindProperty]
        public string SelectedAbilityName { get; set; }

        public string StatusMessage { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPost()
        {
            LoadData();

            var hero = HeroManager.GetHeroes().FirstOrDefault(h => h.Name == SelectedHeroName);

            var ability = AbilityManager.GetAbilities().FirstOrDefault(a => a.Name == SelectedAbilityName);

            if (hero != null && ability != null)
            {
                HeroManager.EquipAbilityToHero(hero.Name, ability);

                StatusMessage = $"¡Habilidad {ability.Name} asignada permanentemente a {hero.Name}!";
            }
            else
            {
                StatusMessage = "Error: No se pudo encontrar el héroe o la habilidad.";
            }

            LoadData();
            return Page();
        }

        private void LoadData()
        {
            EligibleHeroes = HeroManager.GetHeroes().OfType<IAbilityUser>().ToList();
            AvailableAbilities = AbilityManager.GetAbilities().Cast<IAbility>().ToList();
        }
    }
}