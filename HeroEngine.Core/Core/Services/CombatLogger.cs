using HeroEngine.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeroEngine.Core.Services
{
    public static class CombatLogger
    {
        // This list stores the current combat messages in memory
        private static readonly List<string> CurrentCombatLog = new();
        private const string CombatSeparator = "================ COMBAT START ================";

        private static string FilePath => PathConfig.GetFilePath("combat_log.txt");

        // FIX: Ensure this method exists for AHeroes and other classes to call
        public static void AddLog(string message)
        {
            CurrentCombatLog.Add(message);
        }

        public static List<string> GetCurrentLog()
        {
            return CurrentCombatLog;
        }

        public static void ClearLog()
        {
            CurrentCombatLog.Clear();
        }

        public static void SaveCombatToFile(List<string> logs, string participants, string finalResult)
        {
            var sb = new StringBuilder();
            sb.AppendLine(CombatSeparator);
            sb.AppendLine($"Date/Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Participants: {participants}");
            sb.AppendLine("----------------------------------------------");

            foreach (var log in logs)
            {
                sb.AppendLine(log);
            }

            sb.AppendLine("----------------------------------------------");
            sb.AppendLine($"Result: {finalResult}");
            sb.AppendLine("==============================================\n");

            File.AppendAllText(FilePath, sb.ToString());
        }

        public static string ReadLastCombat()
        {
            if (!File.Exists(FilePath)) return "No previous combat records found.";

            string fullHistory = File.ReadAllText(FilePath);
            var combats = fullHistory.Split(new[] { CombatSeparator }, StringSplitOptions.RemoveEmptyEntries);

            return combats.Length > 0 ? CombatSeparator + combats.Last() : "No records found.";
        }
    }
}