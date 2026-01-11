using Configurations.BuildConfigurations;
using GameWideSystems.UIManagement.Screen;

namespace Game.GameMode.StorySelector.UI
{
    public class StorySelectorScreenBuilder : GenericUIScreenBuilder<IScreenAddressableReferenceProvider<StorySelectorScreenController>, StorySelectorScreenDependencies, StorySelectorScreenController>
    {
        public StorySelectorScreenBuilder(IBuildConfigurationsProvider buildConfigurationsProvider, IScreenAddressableReferenceProvider<StorySelectorScreenController> gameModeAddressableProvider, StorySelectorScreenDependencies dialogDependencies) : base(buildConfigurationsProvider, gameModeAddressableProvider, dialogDependencies)
        {
        }
    }
}