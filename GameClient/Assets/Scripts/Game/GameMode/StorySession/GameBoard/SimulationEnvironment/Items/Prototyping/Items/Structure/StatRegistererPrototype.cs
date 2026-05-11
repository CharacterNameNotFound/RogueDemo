using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure
{
    public abstract class StatRegistererPrototype : MonoBehaviour
    {
        public abstract void AppendStats(ItemStatSet ItemStats);
    }
}