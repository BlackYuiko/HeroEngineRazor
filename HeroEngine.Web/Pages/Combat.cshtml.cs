using HeroEngine.Core.Managers;
using HeroEngine.Core.Services;
using HeroEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace HeroEngine.Web.Pages
{
    public class CombatModel : PageModel
    {
        public List<string> CombatLogs { get; set; } = new List<string>();

        public void OnGet()
        {
            // Carga inicial de la página
        }

        public IActionResult OnPostSimulate()
        {
            // 1. Obtenemos los héroes y los casteamos a ICombatant
            var heroes = HeroManager.GetHeroes().Cast<ICombatant>().Where(h => h.IsAlive).ToList();

            if (heroes.Count == 0)
            {
                CombatLogs.Add("Error: No hay héroes vivos en la taberna para luchar.");
                return Page();
            }

            // 2. Usamos tu EnemyManager para generar la cantidad adecuada de enemigos
            // Tenemos que castearlos a ICombatant para pasarlos al CombatService
            var enemies = EnemyManager.GenerateEnemies(heroes.Count).Cast<ICombatant>().ToList();

            // 3. Iniciamos el combate
            CombatLogs = CombatService.StartCombat(heroes, enemies);

            return Page();
        }
    }
}