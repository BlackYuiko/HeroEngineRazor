using HeroEngine.Core.Enums;
using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using HeroEngine.UI; // Assuming UIConfig is here
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HeroEngine.Web.Pages
{
    public class AbilitiesModel : PageModel
    {
        public List<IAbility> AbilitiesList { get; set; } = new List<IAbility>();

        [BindProperty]
        public string AbilityName { get; set; }

        [BindProperty]
        public AbilityType SelectedType { get; set; }

        [BindProperty]
        public AbilityRarity SelectedRarity { get; set; }

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

            // Calculate ManaCost based on your original AbilityMenu logic
            int manaCost = (int)(UIConfig.AbilityMenu.BaseManaCost * Ability.GetRarityMultiplier(SelectedRarity));

            // Create and add the new ability
            var newAbility = new Ability(AbilityName, manaCost, SelectedType, SelectedRarity);
            AbilityManager.AddAbility(newAbility);

            return RedirectToPage();
        }
    }
}