using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Special
{
    public class StatListPrototype : MonoBehaviour
    {
        public ItemStatSet ItemStatSet;

        public ItemStatSet GetItemStatSet()
        {
            return ItemStatSet;
        }

    }
}