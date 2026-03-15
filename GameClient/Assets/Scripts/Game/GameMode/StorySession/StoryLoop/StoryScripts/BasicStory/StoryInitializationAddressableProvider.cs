using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory
{
    public class StoryInitializationAddressableProvider : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _gameBoardPrefab;

        public AssetReference GameBoardPrefab => _gameBoardPrefab;

    }
}