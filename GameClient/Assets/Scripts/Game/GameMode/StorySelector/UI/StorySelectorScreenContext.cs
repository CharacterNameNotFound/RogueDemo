using System.Threading;
using Game.GameMode.StorySelector.UI.States;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;

namespace Game.GameMode.StorySelector.UI
{
    public class StorySelectorScreenContext : IUISMContext
    {
        public CancellationToken CancellationToken;
        
        public StorySelectorScreenController StorySelectorScreenController;
        
        public CharacterSelectionScreenState CharacterSelectionScreenState;
        public StorySelectionScreenState StorySelectionScreenState;

        public string StoryID;
        public string CharacterID;
        
        
        public StorySelectorScreenContext(
            CharacterSelectionScreenState characterSelectionScreenState, 
            StorySelectionScreenState storySelectionScreenState, 
            StorySelectorScreenController storySelectorScreenController, 
            CancellationToken cancellationToken)
        {
            CharacterSelectionScreenState = characterSelectionScreenState;
            StorySelectionScreenState = storySelectionScreenState;
            StorySelectorScreenController = storySelectorScreenController;
            CancellationToken = cancellationToken;
        }
    }
}