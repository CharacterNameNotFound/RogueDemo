using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Facades;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying
{
    public class SimulationViewUpdater : ISimulationViewUpdater
    {
        private GameBoardHolder _gameBoardHolder;
        private IItemStatGetter _itemStatGetter;
        private IItemRenderingFacade _itemRenderingFacade;

        public SimulationViewUpdater(
            GameBoardHolder gameBoardHolder, 
            IItemStatGetter itemStatGetter, 
            IItemRenderingFacade itemRenderingFacade)
        {
            _gameBoardHolder = gameBoardHolder;
            _itemStatGetter = itemStatGetter;
            _itemRenderingFacade = itemRenderingFacade;
        }

        public void RenderChargeValues(List<ItemCache> playerItems, List<ItemCache> encounterItems)
        {
            ItemLineComponent playerItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine;
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            
            UpdateCharge(playerItems, playerItemLine);
            UpdateCharge(encounterItems, encounterItemLine);
            
        }

        
        
        // it can be optimized by passing item charge level through ItemCache, so we can avoid calling recalculation here.
        private void UpdateCharge(List<ItemCache> items, ItemLineComponent itemLine)
        {
            foreach (ItemCache item in items)
            {
                if (item.Item.ItemStats.IsPassiveItem)
                {
                    continue;
                }
                
                
                ItemContainerComponent itemContainer = itemLine.ItemContainerComponents[item.Index];

                float maxCharge = _itemStatGetter.GetStatValue(itemContainer.StoredItem, ItemStatType.MaxCharge);
                float charge = itemContainer.StoredItem.ItemStats.CurrentCharge;

                _itemRenderingFacade.UpdateCharge(itemContainer, charge / maxCharge);
            }
            
        }
        
        
    }
}