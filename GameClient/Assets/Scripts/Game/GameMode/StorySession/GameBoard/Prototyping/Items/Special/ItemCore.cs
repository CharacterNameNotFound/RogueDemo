using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Special
{
    public class ItemCore : MonoBehaviour
    {
        public string ItemId;
        public string ItemName;
        public ItemRarity ItemRarity;
        public ItemCore UpgradedItem;
        public ItemCore DowngradedItem;
    }
}