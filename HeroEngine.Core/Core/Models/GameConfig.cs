using System.Xml.Serialization;

namespace HeroEngine.Core.Models
{
    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        [XmlElement("LevelMultiplier")]
        public double LevelMultiplier { get; set; } = 1.0; // 1.0 = Stats normales por nivel

        [XmlElement("EnemyHpMultiplier")]
        public double EnemyHpMultiplier { get; set; } = 1.0; // 1.0 = Vida base de UIConfig

        [XmlElement("MaxCombatRounds")]
        public int MaxCombatRounds { get; set; } = 20;

        [XmlElement("MaxHeroesPerBattle")]
        public int MaxHeroesPerBattle { get; set; } = 4;
    }
}