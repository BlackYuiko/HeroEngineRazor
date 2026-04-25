using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Heroes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string HeroName { get; set; }
        [BindProperty]
        public int HeroClassType { get; set; }
        [BindProperty]
        public int HeroLevel { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(HeroName) || HeroLevel < 1)
            {
                return Page();
            }

            switch (HeroClassType)
            {
                case 1: HeroManager.AddHero(new Warrior(HeroName, HeroLevel)); break;
                case 2: HeroManager.AddHero(new Mage(HeroName, HeroLevel)); break;
                case 3: HeroManager.AddHero(new Rogue(HeroName, HeroLevel)); break;
            }

            // After saving to the file/manager, redirect back to the list
            return RedirectToPage("/Heroes/Index");
        }
    }
}