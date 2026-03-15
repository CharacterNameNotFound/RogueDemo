using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.ItemStatSets;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure
{
    public abstract class StatRegistererPrototype : MonoBehaviour
    {
        public abstract void AppendStats(ItemStatSet ItemStats);
    }
}