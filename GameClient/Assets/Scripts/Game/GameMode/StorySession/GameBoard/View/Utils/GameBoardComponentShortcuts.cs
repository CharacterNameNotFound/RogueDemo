using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.View.Utils
{
    public static class GameBoardComponentShortcuts
    {
        public static ItemLineComponent HeroGroupToItemLineComponent(HeroGroup heroGroup, GameBoardComponent gameBoard)
        {
            return heroGroup switch
            {
                HeroGroup.Player => gameBoard.ItemLineViewController.PlayerItemLine,
                HeroGroup.Encounter => gameBoard.ItemLineViewController.EncounterItemLine,
                _ => throw new ArgumentOutOfRangeException(nameof(heroGroup), heroGroup, null)
            };
        }
        
        public static HeroStatusEffectDisplay HeroGroupToHeroStatusDisplay(HeroGroup heroGroup, GameBoardComponent gameBoard)
        {
            return heroGroup switch
            {
                HeroGroup.Player => gameBoard.PlayerBoard.HeroStatusEffectDisplay,
                HeroGroup.Encounter => gameBoard.EncounterBoard.BattleView.HeroStatusEffectDisplay,
                _ => throw new ArgumentOutOfRangeException(nameof(heroGroup), heroGroup, null)
            };
        }
        
    }
}