using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.Data.Character;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public class GameBoardModelCreator : IGameBoardModelCreator
    {
        public GameBoardModel CrateNew(GameBoardModelCreationConfigs gameBoardModelCreationConfigs, CharacterData characterData)
        {
            ItemBoardModel playerBoard = new ItemBoardModel();
            ItemBoardModel playerStashBoard = new ItemBoardModel();
            ItemBoardModel encounterBoard = new ItemBoardModel();

            PlayerStats playerStats = new PlayerStats(gameBoardModelCreationConfigs.PlayerStats);

            StoryStats storyStats = new StoryStats(0, 0);

            HeroStats playerHeroStats = new HeroStats(characterData.StartingHp, characterData.StartingHp, 0);
            HeroStats encounterHeroStats = new HeroStats(1, 1, 0);

            Dictionary<Type, IHeroStatusEffect> playerHeroStatusEffects = new Dictionary<Type, IHeroStatusEffect>();
            Dictionary<Type, IHeroStatusEffect> encounterHeroStatusEffects = new Dictionary<Type, IHeroStatusEffect>();

            return new GameBoardModel(
                playerBoard, 
                playerStashBoard, 
                encounterBoard, 
                playerStats, 
                storyStats, 
                playerHeroStatusEffects, 
                encounterHeroStatusEffects, 
                playerHeroStats, 
                encounterHeroStats);
            
        }
        
    }
}