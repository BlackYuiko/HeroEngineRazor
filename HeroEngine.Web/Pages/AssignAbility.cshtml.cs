using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

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
            LoadData(); // Reload lists for the dropdowns

            var hero = EligibleHeroes.FirstOrDefault(h => ((AHeroes)h).Name == SelectedHeroName);
            var ability = AvailableAbilities.FirstOrDefault(a => a.Name == SelectedAbilityName);

            if (hero != null && ability != null)
            {
                hero.AddAbility(ability);
                StatusMessage = $"Successfully assigned {ability.Name} to {((AHeroes)hero).Name}!";
            }
            else
            {
                StatusMessage = "Error assigning ability. Please check your selections.";
            }

            return Page();
        }

        private void LoadData()
        {
            // Only fetch heroes that implement IAbilityUser (e.g., Mage)
            EligibleHeroes = HeroManager.GetHeroes().OfType<IAbilityUser>().ToList();
            AvailableAbilities = AbilityManager.GetAbilities();
        }
    }
}