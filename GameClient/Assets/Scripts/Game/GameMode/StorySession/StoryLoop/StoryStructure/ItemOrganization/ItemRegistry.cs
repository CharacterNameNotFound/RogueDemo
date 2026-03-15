using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public class ItemRegistry
    {
        private Dictionary<string, Item> _items = new();

        public void Initialize(Dictionary<string, Item> items)
        {
            _items = items;
        }
        
        public bool GetByID(string id, out Item result)
        {
            return _items.TryGetValue(id, out result);
        }

        public void CleanUp()
        {
            
        }
    }
}