using Game.GameMode.StorySession.GameBoard.Simulation.Items.Effectors;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure
{
    public abstract class EffectorPrototype : MonoBehaviour
    {
        
        public abstract Effector GetEffector();
    }
}