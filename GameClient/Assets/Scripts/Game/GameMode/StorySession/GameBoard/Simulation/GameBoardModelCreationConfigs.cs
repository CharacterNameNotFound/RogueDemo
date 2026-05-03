using Game.GameMode.StorySession.GameBoard.Simulation.Models;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation
{
    public class GameBoardModelCreationConfigs : ScriptableObject
    {
        [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    }
}