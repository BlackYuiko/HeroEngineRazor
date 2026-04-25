using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace HeroEngine.Web.Pages
{
    public class IndexModel : PageModel
    {
        public int TotalHeroes { get; set; }
        public List<AHeroes> RecentHeroes { get; set; } = new List<AHeroes>();

        public void OnGet()
        {
            var allHeroes = HeroManager.GetHeroes();
            TotalHeroes = allHeroes.Count;
            // Mostramos solo los 3 primeros como resumen
            RecentHeroes = allHeroes.Take(3).ToList();
        }
    }
}