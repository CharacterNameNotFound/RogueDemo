using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public class ItemContainersPoolConfig : ScriptableObject
    {
        [field: SerializeField] public int SmallItemsCount { get; private set; }
        [field: SerializeField] public int MediumItemsCount { get; private set; }
        [field: SerializeField] public int LargeItemsCount { get; private set; }
        
        [field: SerializeField] public AssetReference SmallItemContainerRef { get; private set; }
        [field: SerializeField] public AssetReference MediumItemContainerRef { get; private set; }
        [field: SerializeField] public AssetReference LargeItemContainerRef { get; private set; }
        
        
    }
}