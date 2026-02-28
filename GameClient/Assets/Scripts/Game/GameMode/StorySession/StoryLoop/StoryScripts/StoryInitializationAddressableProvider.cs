using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts
{
    public class StoryInitializationAddressableProvider : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _gameBoardPrefab;

        public AssetReference GameBoardPrefab => _gameBoardPrefab;

    }
}