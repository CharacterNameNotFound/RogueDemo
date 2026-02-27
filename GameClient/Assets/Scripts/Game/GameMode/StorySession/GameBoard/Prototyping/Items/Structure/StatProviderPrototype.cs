using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure
{
    public abstract class StatProviderPrototype : MonoBehaviour
    {
        public abstract StatProvider GetStatProvider();
    }
}