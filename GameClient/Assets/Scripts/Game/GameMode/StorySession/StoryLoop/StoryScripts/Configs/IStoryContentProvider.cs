using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs
{
    public interface IStoryContentProvider
    {
        public List<AssetReference> NeutralItemSets { get; }

        public List<AssetReference> EncounterSets { get; }
        public List<AssetReference> FirstBossEncounters { get; }
        public List<AssetReference> SecondBossEncounters { get; }
        public List<AssetReference> ThirdBossEncounters { get; }
    }
}