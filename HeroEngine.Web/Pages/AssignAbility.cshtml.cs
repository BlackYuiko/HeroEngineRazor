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
            // 1. Cargamos los datos para tener las listas actualizadas
            LoadData();

            // 2. Buscamos el héroe y la habilidad usando los nombres seleccionados
            // Buscamos en la caché global del Manager para asegurar que es el objeto correcto
            var hero = HeroManager.GetHeroes().FirstOrDefault(h => h.Name == SelectedHeroName);

            // IMPORTANTE: Buscamos la habilidad como clase 'Ability' (concreta) para el Manager
            var ability = AbilityManager.GetAbilities().FirstOrDefault(a => a.Name == SelectedAbilityName);

            if (hero != null && ability != null)
            {
                // 3. ¡ESTA ES LA LÍNEA CLAVE! 
                // En lugar de hero.AddAbility, usamos el Manager que ya creamos antes
                // porque el Manager sabe que después de añadir, debe llamar a SaveAll()
                HeroManager.EquipAbilityToHero(hero.Name, ability);

                StatusMessage = $"¡Habilidad {ability.Name} asignada permanentemente a {hero.Name}!";
            }
            else
            {
                StatusMessage = "Error: No se pudo encontrar el héroe o la habilidad.";
            }

            // Recargamos datos para que los desplegables sigan funcionando
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            // Obtenemos solo los héroes que pueden usar habilidades
            EligibleHeroes = HeroManager.GetHeroes().OfType<IAbilityUser>().ToList();
            AvailableAbilities = AbilityManager.GetAbilities().Cast<IAbility>().ToList();
        }
    }
}