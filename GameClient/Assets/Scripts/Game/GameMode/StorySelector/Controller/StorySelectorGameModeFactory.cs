using Game.GameMode.StorySelector.UI;
using GameWideSystems.GameSceneManagement;
using GameWideSystems.UIManagement;
using Zenject;

namespace Game.GameMode.StorySelector.Controller
{
    public class StorySelectorGameModeFactory : IFactory<StorySelectorGameMode>
    {
        private UIManager _uiManager;
        private ILoadingScreenManager _loadingScreenManager;
        private StorySelectorScreenBuilder _storySelectorScreenBuilder;

        public StorySelectorGameModeFactory(UIManager uiManager, ILoadingScreenManager loadingScreenManager, StorySelectorScreenBuilder storySelectorScreenBuilder)
        {
            _uiManager = uiManager;
            _loadingScreenManager = loadingScreenManager;
            _storySelectorScreenBuilder = storySelectorScreenBuilder;
        }

        public StorySelectorGameMode Create()
        {
            return new StorySelectorGameMode(
                _uiManager, 
                _loadingScreenManager, 
                _storySelectorScreenBuilder);
        }
    }
}