using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils
{
    public static class TargetCalculator
    {
        public static int GetTargetId(int owner, int targetModifier)
        {
            return (owner + targetModifier) % 2;
        }

        public static HeroGroup IndexToHeroGroup(int owner)
        {
            return (HeroGroup) (owner % 2);
        }
        
    }
}