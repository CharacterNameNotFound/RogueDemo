using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemLineOrganization
{
    public class ItemLineBuffer
    {
        public ItemContainerComponent[] ItemBuffer;

        public ItemLineBuffer()
        {
            ItemBuffer = new ItemContainerComponent[ItemLineComponent.MaxItemCapacity];
        }

        public ItemContainerComponent[] CopyFrom(ItemLineComponent original)
        {
            return CopyFrom(original.ItemContainerComponents);
        } 
        
        public ItemContainerComponent[] CopyFrom(ItemContainerComponent[] original)
        {
            for (int i = 0; i < original.Length; i++)
            {
                ItemBuffer[i] = original[i];
            }

            return ItemBuffer;
        }

        public void CopyInto(ItemLineComponent original)
        {
            for (int i = 0; i < original.ItemContainerComponents.Length; i++)
            {
                ItemBuffer[i] = original.ItemContainerComponents[i];
            }
        }
        
        public void ClearBuffer()
        {
            for (int i = 0; i < ItemBuffer.Length; i++)
            {
                ItemBuffer[i] = null;
            }
        }
        
    }
}