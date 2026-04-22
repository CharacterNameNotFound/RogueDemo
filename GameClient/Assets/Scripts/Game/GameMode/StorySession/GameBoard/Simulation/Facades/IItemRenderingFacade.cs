using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Facades
{
    public interface IItemRenderingFacade
    {
        public void UpdateCharge(ItemContainerComponent item, float charge);
    }
}