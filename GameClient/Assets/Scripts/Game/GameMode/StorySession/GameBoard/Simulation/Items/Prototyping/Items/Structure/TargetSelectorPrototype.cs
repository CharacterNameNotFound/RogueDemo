using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure
{
    public abstract class TargetSelectorPrototype : MonoBehaviour
    {
        public abstract TargetSelector GetTargetSelector();
    }
}