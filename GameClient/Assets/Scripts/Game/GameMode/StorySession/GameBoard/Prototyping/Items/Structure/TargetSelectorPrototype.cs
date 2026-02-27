using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure
{
    public abstract class TargetSelectorPrototype : MonoBehaviour
    {
        public abstract Simulation.Items.Structure.TargetSelector GetTargetSelector();
    }
}