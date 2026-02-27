using Game.GameMode.StorySession.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.UIManagement;
using Zenject;

namespace Game.GameMode.StorySession.Controller
{
    public class StorySessionGameModeFactory : IFactory<StorySessionGameMode>
    {
        private UIManager _uiManager;
        private ILoadingScreenManager _loadingScreenManager;
        private StorySessionScreenBuilder _storySessionScreenBuilder;


        public StorySessionGameModeFactory(
            UIManager uiManager, 
            ILoadingScreenManager loadingScreenManager, 
            StorySessionScreenBuilder storySessionScreenBuilder)
        {
            _uiManager = uiManager;
            _loadingScreenManager = loadingScreenManager;
            _storySessionScreenBuilder = storySessionScreenBuilder;
        }

        public StorySessionGameMode Create()
        {
            return new StorySessionGameMode(_uiManager, _loadingScreenManager, _storySessionScreenBuilder);
        }
    }
}