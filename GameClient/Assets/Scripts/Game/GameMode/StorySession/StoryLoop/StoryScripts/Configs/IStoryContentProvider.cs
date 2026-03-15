using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs
{
    public interface IStoryContentProvider
    {
        public List<AssetReference> NeutralItemSets { get; }

        public List<AssetReference> EncounterGroups { get; }
        public List<AssetReference> BossEncounters { get; }
    }
}