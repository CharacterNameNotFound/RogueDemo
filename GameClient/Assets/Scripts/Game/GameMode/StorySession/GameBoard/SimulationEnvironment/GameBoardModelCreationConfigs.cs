using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Models;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment
{
    public class GameBoardModelCreationConfigs : ScriptableObject
    {
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    }
}