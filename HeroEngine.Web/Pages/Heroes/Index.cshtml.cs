using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HeroEngine.Web.Pages.Heroes
{
    public class IndexModel : PageModel
    {
        public List<AHeroes> HeroesList { get; set; } = new List<AHeroes>();

        public void OnGet()
        {
            HeroesList = HeroManager.GetHeroes();
        }
    }
}