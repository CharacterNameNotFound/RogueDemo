using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Facades
{
    public interface IItemRenderingFacade
    {
        public void UpdateCharge(ItemContainerComponent item, float charge);
    }
}