using System.Threading;
using Game.GameMode.StorySelector.UI.States;
using GameWideSystems.UIManagement.Screen.StateMachineGeneric;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySelector.UI
{
    public class StorySelectorScreenContext : IUISMContext
    {
        public StorySelectorScreenController StorySelectorScreenController;
        
        public CharacterSelectionScreenState CharacterSelectionScreenState;
        public StorySelectionScreenState StorySelectionScreenState;

        public AssetReference StoryID;
        public string CharacterID;
        
        
        public StorySelectorScreenContext(
            CharacterSelectionScreenState characterSelectionScreenState, 
            StorySelectionScreenState storySelectionScreenState, 
            StorySelectorScreenController storySelectorScreenController)
        {
            CharacterSelectionScreenState = characterSelectionScreenState;
            StorySelectionScreenState = storySelectionScreenState;
            StorySelectorScreenController = storySelectorScreenController;
        }
    }
}