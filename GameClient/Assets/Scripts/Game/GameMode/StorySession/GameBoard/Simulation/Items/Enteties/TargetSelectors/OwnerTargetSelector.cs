using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.TargetSelectors
{
    public class OwnerTargetSelector : TargetSelector
    {
        public override TargetSelector GetCopy()
        {
            return new OwnerTargetSelector();
        }
    }
}