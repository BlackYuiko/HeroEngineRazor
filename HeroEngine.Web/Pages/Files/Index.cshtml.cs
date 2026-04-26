using HeroEngine.Core.Data;
using HeroEngine.Core.Managers;
using HeroEngine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HeroEngine.Web.Pages.Files
{
    public class IndexModel : PageModel
    {
        public List<CombatResult> RecentStats { get; set; } = new List<CombatResult>();

        [BindProperty]
        public GameConfig Config { get; set; }
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            RecentStats = CsvStatsWriter.GetLast10Stats();

            Config = ConfigManager.LoadConfig();
        }

        public IActionResult OnPost()
        {
            RecentStats = CsvStatsWriter.GetLast10Stats();

            if (!ModelState.IsValid)
            {
                return Page();
            }

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
                return RedirectToPage();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", "combat_stats.csv");
        }
    }
}