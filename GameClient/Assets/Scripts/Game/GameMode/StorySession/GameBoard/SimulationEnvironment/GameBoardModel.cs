using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public class GameBoardModel
    {
        public ItemBoardModel PlayerBoard;
        public ItemBoardModel PlayerStashBoard;
        public ItemBoardModel EncounterBoard;

        public HeroStats PlayerHeroStats;
        public HeroStats EncounterHeroStats;
        
        public Dictionary<Type, IHeroStatusEffect> PlayerHeroStatusEffect;
        public Dictionary<Type, IHeroStatusEffect> EncounterHeroStatusEffect;

        public PlayerStats PlayerStats;

        public StoryStats StoryStats;

        public GameBoardModel(
            ItemBoardModel playerBoard, 
            ItemBoardModel playerStashBoard, 
            ItemBoardModel encounterBoard, 
            PlayerStats playerStats, 
            StoryStats storyStats, 
            Dictionary<Type, IHeroStatusEffect> playerHeroStatusEffect,
            Dictionary<Type, IHeroStatusEffect> encounterHeroStatusEffect,
            HeroStats playerHeroStats, 
            HeroStats encounterHeroStats)
        {
            PlayerBoard = playerBoard;
            PlayerStashBoard = playerStashBoard;
            EncounterBoard = encounterBoard;
            PlayerStats = playerStats;
            StoryStats = storyStats;
            PlayerHeroStatusEffect = playerHeroStatusEffect;
            EncounterHeroStatusEffect = encounterHeroStatusEffect;
            PlayerHeroStats = playerHeroStats;
            EncounterHeroStats = encounterHeroStats;
        }
        
    }
}