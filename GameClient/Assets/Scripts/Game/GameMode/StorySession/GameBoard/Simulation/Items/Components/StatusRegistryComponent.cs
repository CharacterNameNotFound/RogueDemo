using System;
using Game.GameMode.StorySession.GameBoard.Simulation.Utilities;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components
{
    public class StatusRegistryComponent : ItemComponent
    {
        // for now having to use dictionary for status effects feels pricey
        public StatusCounter Hasted = new(0);

        public StatusRegistryComponent()
        {
            
        }
        
        public override ItemComponent GetCopy()
        {
            return new StatusRegistryComponent();
        }
    }
}