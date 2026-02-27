using GameWideSystems.UIManagement;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.StorySession.UI
{
    public class StorySessionScreenController : UIScreen<IScreenParams, StorySessionScreenDependencies>
    {
        public override ScreenType ScreenType => ScreenType.Screen;
        public override ScreenHolderType ScreenHolderType => ScreenHolderType.Game;
    }
}