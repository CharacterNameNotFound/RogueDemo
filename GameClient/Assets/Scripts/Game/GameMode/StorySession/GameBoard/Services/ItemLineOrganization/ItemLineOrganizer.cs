using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization
{
    public class ItemLineOrganizer : IItemLineOrganizer
    {
        
        public void Organize(ItemLineComponent itemLineComponent, ItemContainerComponent[] itemConfiguration, bool updatedStored)
        {
            ReorganizeInternal(itemLineComponent, itemConfiguration);

            if (updatedStored)
            {
                for (int i = 0; i < itemLineComponent.ItemContainerComponents.Length; i++)
                {
                    itemLineComponent.ItemContainerComponents[i] = itemConfiguration[i];
                }
            }
        }

        public void Restore(ItemLineComponent itemLineComponent)
        {
            ReorganizeInternal(itemLineComponent, itemLineComponent.ItemContainerComponents);
        }
        
        
        
        // iternal methods
        
        private void ReorganizeInternal(ItemLineComponent itemLineComponent, ItemContainerComponent[] itemConfiguration)
        {
            Bounds bounds = itemLineComponent.SpriteRenderer.bounds;

            Vector2 origin = new Vector2(bounds.min.x, bounds.center.y);
            Vector2 step = new Vector2(bounds.size.x / 2, 0);
            
            // item configuration can be 1122234556
            for (int i = 0; i < itemConfiguration.Length; i++)
            {
                if (itemConfiguration[i] is null)
                {
                    continue;
                }

                itemConfiguration[i].transform.position = origin + i * step + itemConfiguration[i].Size / 2f * step;
            }
            
        }
        
        
        
    }
}