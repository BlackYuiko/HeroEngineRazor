using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Heroes
{
    public class DetailModel : PageModel
    {
        public AHeroes Hero { get; set; }

        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToPage("/Heroes/Index");
            }

            Hero = HeroManager.GetHeroes().FirstOrDefault(h => h.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (Hero == null)
            {
                return RedirectToPage("/Heroes/Index");
            }

            return Page();
        }
    }
}