# 🛡️ HeroEngine Web — The Kingdom's Gate

## 📖 Overview & Continuation

**HeroEngine Web** is the second phase of the Bytecroft Heroes Adventure. Continuing from the foundational Object-Oriented Programming (OOP) console project, the system has now been modernized and migrated into a fully functional **ASP.NET Core Web Application using Razor Pages**. 

While the core combat mechanics, polymorphic design, and hero hierarchy remain intact, this new iteration introduces dynamic web interfaces, robust file-based data persistence (JSON, CSV, XML, TXT), and advanced data analytics powered by LINQ.

---

## 🧱 Project Structure

The solution has been split into two main projects to enforce a clean separation of concerns: the core library and the web presentation layer.

```text
Solution:
├── HeroEngine.Core (Class Library)
│   ├── Core
│   │   ├── Managers (AbilityManager, EnemyManager, HeroManager, ConfigManager)
│   │   ├── Models
│   │   │   ├── Enums (AbilityRarity, AbilityType)
│   │   │   ├── ACharacters, AHeroes, AEnemies, Ability
│   │   │   ├── Warrior, Rogue, Minions, Elite, Boss
│   │   │   ├── CombatResult, GameConfig
│   │   ├── Services (CombatLogger, CombatService, HeroAnalytics)
│   ├── Interfaces (ICombatant, IAbility, IAbilityUser)
│   ├── Data (AbilityRepository, CsvStatsWriter, HeroRepository, PathConfig)
│   └── UI (UIConfig)
│
└── HeroEngine.Web (ASP.NET Core Web App)
    ├── Pages
    │   ├── Shared (_Layout.cshtml)
    │   ├── Files (Index.cshtml)
    │   ├── Heroes (Index.cshtml, Create.cshtml, Detail.cshtml)
    │   ├── Index.cshtml 
    │   ├── Combat.cshtml
    │   └── Stats.cshtml 
    └── Data (Generated Files)
        ├── heroes.json
        ├── abilities.json
        ├── combat_stats.csv
        ├── game_config.xml
        └── combat_log.txt
```

---

## 🌐 Chapter 5 — The Kingdom's Gate (Razor Pages)

### Objective
Expose the HeroEngine functionalities through a responsive web interface using ASP.NET Core Razor Pages.

### Key Features
* **Index (`/`)**: A dynamic dashboard summarizing the game state, total hero count, and showing recently registered heroes with quick actions.
* **Heroes Directory (`/Heroes`)**: A complete listing of all active heroes in the tavern, including functionality to permanently delete them.
* **Hero Details (`/Heroes/Detail/{name}`)**: Deep dive into a specific hero's stats, level, and their currently equipped abilities.
* **Hero Recruitment (`/Heroes/Create`)**: Form-based creation using Razor Tag Helpers and DataAnnotations for robust validation.
* **Abilities Directory (`/Abilities`)**: A catalog displaying all available abilities in the game, categorized by type, rarity, and mana cost.
* **Assign Ability (`/Abilities/Assign`)**: Dynamic functionality allowing the Council to strategically equip specific abilities to their recruited heroes.
* **Grimoire of Knowledge (`/Stats`)**: A comprehensive analytics dashboard visualizing class distributions, top-tier heroes, most used abilities, and filterable combat histories.
* **Combat Arena (`/Combat`)**: Web-based execution of the polymorphic auto-battler, displaying real-time text logs of the fights.
* **File Management (`/Files`)**: A portal to manage, review, and interact with the game's persistence layer (JSON, CSV, XML).

---

## 🗄️ Chapter 6 — The Realm's Archives (File Persistence)

### Objective
Ensure all game data, combat histories, and configurations survive between application restarts using different file formats.

### Implementations

#### 1. Plain Text (`.txt`) — Combat Logs
* Appends every round's detailed actions into `combat_log.txt`.
* Read and displayed dynamically on the `/Combat` page.

#### 2. JSON (`.json`) — Heroes & Abilities
* Uses `System.Text.Json` to serialize polymorphic hero classes and their abilities.
* *Example of `heroes.json`:*
```json
[
  {
    "$type": "Mage",
    "mana": 130,
    "currentMana": 130,
    "level": 1,
    "dmgAttack": 25,
    "abilities": 
    [
      {
        "name": "Rayo Destructor",
        "manaCost": 60,
        "type": 0,
        "rarity": 2
      }
    ],
    "name": "Hugo",
    "maxHP": 115,
    "currentHP": 115,
    "isAlive": true
  }
]
```

#### 3. CSV (`.csv`) — Combat Statistics
* Records match metadata: Date, Participants, Result (Victory/Defeat), Total Rounds, Total Damage, and MVP.
* Custom manual parser implemented in `CsvStatsWriter` without third-party libraries.

#### 4. XML (`.xml`) — Game Configuration
* Replaces hard-coded magic numbers with a dynamic XML configuration file parsed via `XmlSerializer` / `XDocument`.
* *Example of `game_config.xml`:*
```xml
<?xml version="1.0" encoding="utf-8"?>
<GameConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <LevelMultiplier>1</LevelMultiplier>
  <EnemyHpMultiplier>1</EnemyHpMultiplier>
  <MaxCombatRounds>20</MaxCombatRounds>
  <MaxHeroesPerBattle>4</MaxHeroesPerBattle>
</GameConfig>
```

---

## 📊 Chapter 7 — Grimoire of Knowledge (Advanced Data Management)

### Objective
Utilize generic collections and LINQ to provide advanced analytical tools to the Bytecroft Council.

### Key Features
* **`HeroAnalytics` Service**: 
  * `GetTopHeroesByLevel(n)`
  * `GetAverageDamagePerClass()`
  * `GetAbilitiesByRarity(rarity)`
  * `SearchHeroesByName(Regex pattern)`
* **Stats Dashboard (`/Stats`)**: A comprehensive visual page displaying:
  * Hero distribution by class (Percentages).
  * Top 3 highest-level heroes.
  * Most frequently equipped abilities.
  * A filterable combat history table (by Victory or Defeat).

---

## 🚀 How to Run

1. Open the solution (`HeroEngine.sln`) in Visual Studio 2022.
2. Ensure **`HeroEngine.Web`** is set as the **Startup Project** (Right-click the project in Solution Explorer -> *Set as Startup Project*).
3. Build the solution (`Ctrl + Shift + B`) to restore NuGet packages and project references.
4. Run the application (`F5` or `Ctrl + F5`).
5. The browser will open automatically at `https://localhost:<port>/`. 
6. *Note: Data files (`.json`, `.xml`, etc.) will be automatically generated in the `HeroEngine.Web/Data` folder upon the first execution or hero creation.*

---

## 🏆 Final Result
This project successfully evolves a console-based combat simulator into a **full-stack .NET web application**. It demonstrates a strong grasp of OOP architecture, web routing, state management, diverse file I/O operations, and advanced LINQ querying, providing a complete and extensible foundation for any C# RPG engine.