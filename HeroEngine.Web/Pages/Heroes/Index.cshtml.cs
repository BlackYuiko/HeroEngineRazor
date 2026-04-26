using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Heroes
{
    public class IndexModel : PageModel
    {
        public List<AHeroes> HeroesList { get; set; } = new List<AHeroes>();

        public void OnGet()
        {
            HeroesList = HeroManager.GetHeroes();
        }

        public IActionResult OnPostDelete(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                HeroManager.DeleteHero(name);
            }

            return RedirectToPage();
        }
    }
}