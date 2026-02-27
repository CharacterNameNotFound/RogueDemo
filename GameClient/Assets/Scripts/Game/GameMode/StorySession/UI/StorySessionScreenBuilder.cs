using Configurations.BuildConfigurations;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.StorySession.UI
{
    public class StorySessionScreenBuilder : GenericUIScreenBuilder<IScreenAddressableReferenceProvider<StorySessionScreenController>, StorySessionScreenDependencies, StorySessionScreenController>
    {
        public StorySessionScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, IScreenAddressableReferenceProvider<StorySessionScreenController> gameModeAddressableProvider, StorySessionScreenDependencies dialogDependencies) : base(buildConfigurationsProvider, gameModeAddressableProvider, dialogDependencies)
        {
        }
    }
}