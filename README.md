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
* **Index (`/`)**: A dynamic dashboard summarizing the game state and showing recently registered heroes.
* **Heroes Directory (`/Heroes`)**: A listing of all active heroes in the tavern.
* **Hero Details (`/Heroes/Detail/{name}`)**: Deep dive into a hero's stats and equipped abilities.
* **Hero Recruitment (`/Heroes/Create`)**: Form-based creation using Razor Tag Helpers and DataAnnotations for validation.
* **Combat Arena (`/Combat`)**: Web-based execution of the polymorphic auto-battler, displaying real-time text logs.
* **File Management (`/Files`)**: A portal to manage and review the persistence layer.

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
    "type": "Warrior",
    "name": "Aldric",
    "level": 3,
    "maxHp": 150,
    "currentHp": 150,
    "abilities": [
      { "name": "Thunder Smash", "type": "Attack", "rarity": "Legendary", "manaCost": 40 }
    ]
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
<GameConfig>
  <LevelMultiplier>1.15</LevelMultiplier>
  <CriticalHitChance>0.20</CriticalHitChance>
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