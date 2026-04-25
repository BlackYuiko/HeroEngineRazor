using HeroEngine.Core.Enums;
using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic; // Asegúrate de tener este para List<>

namespace HeroEngine.Web.Pages 
{
    public class AbilitiesModel : PageModel
    {
        public List<Ability> AbilitiesList { get; set; } = new List<Ability>();

        [BindProperty] public string AbilityName { get; set; }
        [BindProperty] public AbilityType SelectedType { get; set; }
        [BindProperty] public AbilityRarity SelectedRarity { get; set; }

        public void OnGet()
        {
            AbilitiesList = AbilityManager.GetAbilities();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(AbilityName))
            {
                AbilitiesList = AbilityManager.GetAbilities();
                return Page();
            }

            int manaCost = (int)(UIConfig.AbilityMenu.BaseManaCost * Ability.GetRarityMultiplier(SelectedRarity));

            var newAbility = new Ability(AbilityName, manaCost, SelectedType, SelectedRarity);

            AbilityManager.AddAbility(newAbility);

            return RedirectToPage();
        }
    }
}