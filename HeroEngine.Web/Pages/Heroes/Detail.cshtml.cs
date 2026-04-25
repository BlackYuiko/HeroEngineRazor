using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace HeroEngine.Web.Pages.Heroes
{
    public class DetailModel : PageModel
    {
        public AHeroes Hero { get; set; }

        // The "name" parameter comes directly from the URL
        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToPage("/Heroes/Index");
            }

            // Find the hero by name
            Hero = HeroManager.GetHeroes().FirstOrDefault(h => h.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (Hero == null)
            {
                return RedirectToPage("/Heroes/Index"); // Redirect if not found
            }

            return Page();
        }
    }
}