using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract
{
    public abstract class ItemComponentPrototypeComponent : MonoBehaviour
    {
        public abstract void WriteToItem(Item item);
    }
}