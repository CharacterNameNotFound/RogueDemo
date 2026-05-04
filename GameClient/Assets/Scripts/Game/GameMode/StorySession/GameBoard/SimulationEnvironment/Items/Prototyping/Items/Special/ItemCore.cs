using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Special
{
    public class ItemCore : MonoBehaviour
    {
        public string ItemId;
        public string ItemName;
        public ItemRarity ItemRarity;
        public int ItemSize = 1;
        public ItemCore UpgradedItem;
        public ItemCore DowngradedItem;
        public AssetReferenceSprite ItemImage;
    }
}