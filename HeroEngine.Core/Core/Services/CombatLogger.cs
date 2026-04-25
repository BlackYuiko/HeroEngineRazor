using System.Collections.Generic;

namespace HeroEngine.Core.Services
{
    public static class CombatLogger
    {
        private static List<string> _logs = new List<string>();

        public static void AddLog(string message)
        {
            _logs.Add(message);
        }

        public static List<string> GetLogs()
        {
            // Devolvemos una copia de la lista
            return new List<string>(_logs);
        }

        public static void ClearLogs()
        {
            _logs.Clear();
        }
    }
}