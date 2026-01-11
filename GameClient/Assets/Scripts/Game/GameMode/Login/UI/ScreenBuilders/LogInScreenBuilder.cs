using Configurations.BuildConfigurations;
using Game.GameMode.Login.UI.Data;
using Game.GameMode.Login.UI.Screens;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.Login.UI.ScreenBuilders
{
    public class LogInScreenBuilder : GenericUIScreenBuilder<IScreenAddressableReferenceProvider<LogInScreenController>, LogInScreenDependencies, LogInScreenController>
    {
        public LogInScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, IScreenAddressableReferenceProvider<LogInScreenController> gameModeAddressableProvider, LogInScreenDependencies screenDependencies) 
            : base(buildConfigurationsProvider, gameModeAddressableProvider, screenDependencies)
        {
        }
    }
}