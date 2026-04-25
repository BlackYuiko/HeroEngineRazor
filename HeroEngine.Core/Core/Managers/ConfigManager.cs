using HeroEngine.Core.Data;
using HeroEngine.Core.Models;
using System.IO;
using System.Xml.Serialization;

namespace HeroEngine.Core.Managers
{
    public static class ConfigManager
    {
        // Ruta al archivo XML
        private static string FilePath => PathConfig.GetFilePath("game_config.xml");

        public static GameConfig LoadConfig()
        {
            // Si el archivo no existe, creamos uno con los valores por defecto
            if (!File.Exists(FilePath))
            {
                var defaultConfig = new GameConfig();
                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            // Deserializamos el XML a nuestro objeto C#
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamReader reader = new StreamReader(FilePath))
            {
                return (GameConfig)serializer.Deserialize(reader);
            }
        }

        public static void SaveConfig(GameConfig config)
        {
            // Serializamos el objeto C# de vuelta a XML
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}