using HeroEngine.Core.Data;
using HeroEngine.Core.Managers; // Añadido para el XML
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace HeroEngine.Web.Pages.Files
{
    public class IndexModel : PageModel
    {
        // --- PROPIEDADES CSV ---
        public List<CombatResult> RecentStats { get; set; } = new List<CombatResult>();

        // --- PROPIEDADES XML ---
        [BindProperty]
        public GameConfig Config { get; set; }
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            // 1. Cargar estadísticas CSV
            RecentStats = CsvStatsWriter.GetLast10Stats();

            // 2. Cargar configuración XML
            Config = ConfigManager.LoadConfig();
        }

        // --- NUEVO: MÉTODO PARA GUARDAR XML ---
        public IActionResult OnPost()
        {
            // Recargamos los stats para que la tabla siga viéndose tras guardar
            RecentStats = CsvStatsWriter.GetLast10Stats();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Guardamos la configuración en el XML
            ConfigManager.SaveConfig(Config);
            StatusMessage = "Game configuration successfully saved to game_config.xml!";

            return Page();
        }

        /// <summary>
        /// Handler to trigger the download of the CSV file.
        /// </summary>
        public IActionResult OnGetDownloadCsv()
        {
            string filePath = PathConfig.GetFilePath("combat_stats.csv");

            if (!System.IO.File.Exists(filePath))
            {
                return RedirectToPage(); // Return if file doesn't exist yet
            }

            // Read the file as bytes and return it as a downloadable file
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", "combat_stats.csv");
        }
    }
}