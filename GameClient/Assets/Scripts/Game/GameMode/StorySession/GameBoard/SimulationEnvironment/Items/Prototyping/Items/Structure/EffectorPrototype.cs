using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Structure
{
    public abstract class EffectorPrototype : MonoBehaviour
    {
        
        public abstract Effector GetEffector();
    }
}