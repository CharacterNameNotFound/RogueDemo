using System.Collections.Generic;
using Game.GameMode.StorySession.StoryLoop.StoryRoutines.DataProviders;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class BaseStoryConfigs : ScriptableObject, IDeckBuildingConfigs, IStoryContentProvider
    {
        [field: SerializeField] public List<AssetReference> NeutralItemSets { get; private set; }
        [field: SerializeField] public List<AssetReference> EncounterGroups { get; private set; }
        [field: SerializeField] public List<AssetReference> BossEncounters { get; private set; }

        [field: SerializeField] public int CardCopiesCount { get; private set; } = 2;



    }
}