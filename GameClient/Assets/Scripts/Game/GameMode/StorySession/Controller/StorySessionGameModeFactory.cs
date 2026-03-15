using Zenject;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeFactory : IFactory<StorySessionGameMode>
    {

        public StorySessionGameModeFactory()
        {

        }

        public StorySessionGameMode Create()
        {
            return new StorySessionGameMode();
        }
    }
}