using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Items.Components 
{
    [Serializable]
    public class ItemBodyComponent : ItemComponent
    {

        public ItemBodyComponent()
        {
            
        }
        
        public override ItemComponent GetCopy()
        {
            return new ItemBodyComponent();
        }
    }
}