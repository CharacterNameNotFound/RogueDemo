using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.ItemStatSets;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure
{
    public abstract class StatRegistererPrototype : MonoBehaviour
    {
        public abstract void AppendStats(ItemStatSet ItemStats);
    }
}