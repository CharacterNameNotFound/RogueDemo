using System.Collections.Generic;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines.DataProviders;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStoryConfigs : ScriptableObject, 
        IItemDeckBuildingConfigs, 
        IStoryContentProvider, 
        IEncounterDeckBuildingConfigs, 
        IStoryGenericConfigs
    {
        [field: Header("Story content configs")]
        [field: SerializeField] public List<AssetReference> NeutralItemSets { get; private set; }
        [field: SerializeField] public List<AssetReference> EncounterSets { get; private set; }
        [field: SerializeField] public List<AssetReference> FirstBossEncounters { get; private set; }
        [field: SerializeField] public List<AssetReference> SecondBossEncounters { get; private set; }
        [field: SerializeField] public List<AssetReference> ThirdBossEncounters { get; private set; }

        [field: Space(15)]
        [field: Header("Item deck building configs")]
        [field: SerializeField] public int CardCopiesCount { get; private set; } = 2;

        [field: Space(15)]
        [field: Header("Generic story configs")]
        [field: SerializeField] public int StoryDayLength { get; private set; }
        [field: SerializeField] public int StoryDayCount { get; private set; }
    }
}