using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Tags;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Components.Triggers;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items
{
    public class Item
    {
        public TagComponent Tags;
        public ItemBodyComponent ItemBody;
        public StatsComponent Stats;
        public CooldownComponent CooldownComponent;
        public List<TriggerComponent> CooldownTriggers = new();
        public List<TriggerComponent> ConditionTriggers = new();

        public StatusRegistryComponent StatusRegistryComponent;
    }
    
}