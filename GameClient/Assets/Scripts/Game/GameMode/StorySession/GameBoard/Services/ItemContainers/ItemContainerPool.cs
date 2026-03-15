using System.Collections.Generic;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public class ItemContainerPool  : GameObjectPool<ItemContainerComponent>
    {
        public ItemContainerPool(List<ItemContainerComponent> pool, IPoolEntityBuilder<ItemContainerComponent> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
        
    }
}