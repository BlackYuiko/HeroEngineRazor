using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using System.Xml.Serialization;

namespace HeroEngine.Core.Managers
{
    public static class ConfigManager
    {
        private static string FilePath => PathConfig.GetFilePath("game_config.xml");

        public static GameConfig LoadConfig()
        {
            if (!File.Exists(FilePath))
            {
                var defaultConfig = new GameConfig();
                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamReader reader = new StreamReader(FilePath))
            {
                return (GameConfig)serializer.Deserialize(reader);
            }
        }

        public static void SaveConfig(GameConfig config)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}