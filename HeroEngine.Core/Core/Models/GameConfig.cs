using System.Xml.Serialization;

namespace HeroEngine.Core.Models
{
    [XmlRoot("GameConfig")]
    public class GameConfig
    {
        [XmlElement("LevelMultiplier")]
        public double LevelMultiplier { get; set; } = 1.0;

        [XmlElement("EnemyHpMultiplier")]
        public double EnemyHpMultiplier { get; set; } = 1.0;

        [XmlElement("MaxCombatRounds")]
        public int MaxCombatRounds { get; set; } = 20;

        [XmlElement("MaxHeroesPerBattle")]
        public int MaxHeroesPerBattle { get; set; } = 4;
    }
}