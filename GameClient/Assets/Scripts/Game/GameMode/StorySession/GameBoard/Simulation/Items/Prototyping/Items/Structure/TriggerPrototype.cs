using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure
{
    public abstract class TriggerPrototype : MonoBehaviour
    {
        public abstract Trigger GetTrigger();
    }
}