using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Prototyping.Items.Structure
{
    public abstract class EffectorPrototype : MonoBehaviour
    {
        
        public abstract Effector GetEffector();
    }
}