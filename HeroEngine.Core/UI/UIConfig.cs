using HeroEngine.Core.Managers;
using System;

namespace HeroEngine.UI
{
    /// <summary>
    /// Central configuration class that contains all UI-related text constants and game balancing values.
    /// Used across menus, combat, abilities, heroes, and enemies.
    /// </summary>
    public static class UIConfig
    {
        /// <summary>
        /// General UI messages and shared constants used across the application.
        /// </summary>
        public static class General
        {
            public const string ErrorNumberInput = "Error! You must introduce a number in the list.";
            public const string MsgAbilities = "Abilities:";
            public const string MsgAbilitiesNone = " - None";
            public const string MsgNoMana = "Not enough mana!";
            public const string NoHeroesToCombat = "You can not start the combat if you have no heroes!";
            public const string HeroesIsDead = "Sorry! All your Heroes are dead";

            public const string MenuChooseAttack = "1. Basic attack";
            public const string MenuChooseAbility2 = "2. Ability";
            public const string MenuChooseAction = "Choose action [Default: 1]: ";

        }

        /// <summary>
        /// Main menu UI texts displayed in Program.cs.
        /// </summary>
        public static class MainMenu
        {
            public const string Welcome = "======WELCOME TO THE ADVENTURE OF THE HEROS OF BYTECROFT======\n";
            public const string MenuHeroEngine = "----HeroEngine----";
            public const string MenuCreateHero = "1. Create a Hero";
            public const string MenuHeroStats = "2. List all Heroes' stats";
            public const string MenuCreateAbility = "3. Create an Ability";
            public const string MenuAbilityStats = "4. List all Abilities";
            public const string MenuAsignAbilityToHero = "5. Asign ability to a Hero";
            public const string MenuCombat = "6. Combat";
            public const string MenuExit = "0. Exit";
            public const string MenuChooseOption = "Choose an option: ";
            public const string GoodBye = "\nGood Bye!";
        }

        /// <summary>
        /// UI texts related to hero creation, listing, and management.
        /// </summary>
        public static class HeroMenu
        {
            public const string MenuWarrior = "\n1. Warrior";
            public const string MenuMage = "2. Mage";
            public const string MenuRogue = "3. Rogue";
            public const string MenuChooseType = "Choose your Hero's type: ";
            public const string MenuChooseName = "Choose your Hero's name: ";
            public const string MenuChooseLevel = "Choose your Hero's level from 1 to 3 (Default: 1): ";

            public const string NoHeroesToList = "No heroes created yet.";

            public const string ChooseHeroToAsignAbility = "Choose a Hero to asign an ability: ";
            public const string NoHeroesOrHabilities = "Heroes or Abilities needed";
        }

        /// <summary>
        /// Hero-related configuration values such as stats and gameplay constants.
        /// </summary>
        public static class Heroes
        {
            public const string HeroAlreadyExist = "A Hero with this name already exists.";

            public const int HeroesBaseIniciative = 10;
        }

        /// <summary>
        /// Configuration values for Warrior hero class.
        /// </summary>
        public static class Warrior
        {
            public const string BattleCry = "'FOR THE BYTECROFT!'";

            public const int WarriorHPPerLevel = 25;
            public const int WarriorArmorPerLevel = 5;
            public const int WarriorDmgPerLevel = 15;
            public const int WarriorBaseHP = 100;
            public const int WarriorBaseArmor = 10;
            public const int WarriorBaseDmg = 20;
        }

        /// <summary>
        /// Configuration values for Mage hero class.
        /// </summary>
        public static class Mage
        {
            public const int MageHPPerLevel = 15;
            public const int MageManaPerLevel = 30;
            public const int MageDmgPerLevel = 10;

            public const int MageBaseHP = 100;
            public const int MageBaseMana = 100;
            public const int MageBaseDmg = 15;
        }

        /// <summary>
        /// Configuration values for Rogue hero class.
        /// </summary>
        public static class Rogue
        {
            public const int RogueHPPerLevel = 20;
            public const int RogueDmgPerLevel = 15;
            public const double RogueDmgMultiplierPerLevel = 0.1;

            public const int RogueBaseHP = 100;
            public const int RogueBaseDmg = 25;
            public const double RogueBaseDmgMultiplier = 1.3;
        }

        /// <summary>
        /// UI texts and configuration for ability system menus.
        /// </summary>
        public static class AbilityMenu
        {
            public const string MenuAbilityName = "Choose your Ability's name: ";
            public const string MenuAbilityAttack = "\n1. Attack";
            public const string MenuAbilityDefense = "2. Defense";
            public const string MenuAbilityHealing = "3. Healing";
            public const string MenuAbilitySupport = "4. Support";
            public const string MenuAbilityType = "Choose your Ability's type: ";
            public const string MenuAbilityCommon = "\n1. Common";
            public const string MenuAbilityRare = "2. Rare";
            public const string MenuAbilityEpic = "3. Epic";
            public const string MenuAbilityLegendary = "4. Legendary";
            public const string MenuAbilityRarity = "Choose your Ability's rarity: ";
            public const string NoAbilitiesToList = "No abilities created yet.";
            public const string HeroCantHabilities = "This hero can not use habilities.";
            public const string ChooseAbilityToAsign = "Choose the ability you want to asign: ";
            public const string NameAlreadyExist = "An ability with this name already exists.";
            public const string CantUseHabilities = "This hero cannot use abilities.";
            public const string NoAbilitiesEquiped = "No abilities equipped.";
            public const string aChooseHability = "Choose ability: ";
            public const int BaseManaCost = 30;
        }

        /// <summary>
        /// Configuration values for ability scaling, effects, and damage calculations.
        /// </summary>
        public static class AbilityUI
        {
            public const int BaseHeal = 30;
            public const int BaseDefense = 30;
            public const int BaseAttack = 30;
            public const string MsgSupport = "WE WIN THIS!";

            public const double RarityCommonMultiplier = 1.0;
            public const double RarityRareMultiplier = 1.5;
            public const double RarityEpicMultiplier = 2.0;
            public const double RarityLegendaryMultiplier = 3.0;
            public const double RarityBaseMultiplier = 1.0;
        }

        /// <summary>
        /// Configuration for enemy naming and generation labels.
        /// </summary>
        public static class Enemy
        {
            public const string EnemyMinion = "Minion";
            public const string EnemyElite = "Elite Boss";
            public const string EnemyBoss = "Bug Primordial";
        }

        /// <summary>
        /// Configuration values for Minion enemy type.
        /// </summary>
        public static class Minion
        {
            public const int MinionBaseHP = 30;
            public const int MinionBaseDmg = 10;
            public const int MinionBaseIniciative = 5;
        }

        /// <summary>
        /// Configuration values for Elite enemy type.
        /// </summary>
        public static class Elite
        {
            public const int EliteBaseHP = 100;
            public const int EliteBaseDmg = 30;
            public const int EliteBaseIniciative = 8;
        }

        /// <summary>
        /// Configuration values for Boss enemy type.
        /// </summary>
        public static class Boss
        {
            public const int BossBaseHP = 200;
            public const int BossBaseDmg = 50;
            public const int BossBaseIniciative = 15;
        }

        /// <summary>
        /// Configuration for combat system messages and UI output.
        /// </summary>
        public static class Combat
        {
            public const string CombatStarts = "Combat starts!\n";
            public const string CombatPreesEnter = "\nPress ENTER for next turn...";
            public const string CombatFinished = "Combat finished.";
            public const string CombatLines = "==============================================";
            public const string CombatBattleLog = "          BATTLE LOG - ROUND {0} ";
        }
    }
}