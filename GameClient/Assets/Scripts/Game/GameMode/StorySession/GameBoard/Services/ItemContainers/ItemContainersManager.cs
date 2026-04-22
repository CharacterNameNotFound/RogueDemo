using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.Services.ItemContainers
{
    public class ItemContainersManager : IItemContainersManager
    {
        private ItemContainersPoolConfig _config;
        private IPooledObjectHostProvider _pooledObjectHostProvider;
        
        private ItemContainerPool _smallPool;
        private ItemContainerPool _mediumPool;
        private ItemContainerPool _largePool;

        public ItemContainersManager(ItemContainersPoolConfig config, 
            IPooledObjectHostProvider pooledObjectHostProvider)
        {
            _config = config;
            _pooledObjectHostProvider = pooledObjectHostProvider;
        }

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            List<UniTask> asyncWork = new List<UniTask>();
            
            _smallPool = new ItemContainerPool(
                new List<ItemContainerComponent>(_config.SmallItemsCount),
                new AddressablePoolEntityProvider<ItemContainerComponent>(_config.SmallItemContainerRef),
                _pooledObjectHostProvider);

            asyncWork.Add(_smallPool.ExtendBy(_config.SmallItemsCount, cancellationToken));
            
            _mediumPool = new ItemContainerPool(
                new List<ItemContainerComponent>(_config.MediumItemsCount),
                new AddressablePoolEntityProvider<ItemContainerComponent>(_config.MediumItemContainerRef),
                _pooledObjectHostProvider);

            asyncWork.Add(_mediumPool.ExtendBy(_config.MediumItemsCount, cancellationToken));
            
            _largePool = new ItemContainerPool(
                new List<ItemContainerComponent>(_config.LargeItemsCount),
                new AddressablePoolEntityProvider<ItemContainerComponent>(_config.LargeItemContainerRef),
                _pooledObjectHostProvider);

            asyncWork.Add(_largePool.ExtendBy(_config.LargeItemsCount, cancellationToken));

            await UniTask.WhenAll(asyncWork).AttachExternalCancellation(cancellationToken);
            

        }

        public UniTask<ItemContainerComponent> GetBySize(int itemItemSize, CancellationToken cancellationToken)
        {
            return itemItemSize switch
            {
                1 => _smallPool.GetObject(cancellationToken),
                2 => _mediumPool.GetObject(cancellationToken),
                3 => _largePool.GetObject(cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(itemItemSize), itemItemSize, null)
            };
        }

        public void Return(ItemContainerComponent itemContainerComponent)
        {
            switch (itemContainerComponent.Size)
            {
                case 1:
                    _smallPool.ReturnToPool(itemContainerComponent);
                    break;
                case 2:
                    _mediumPool.ReturnToPool(itemContainerComponent);
                    break;
                case 3:
                    _largePool.ReturnToPool(itemContainerComponent);
                    break;
            }
            
        }

        public void CleanUp()
        {
            _smallPool.ReleaseAll();
            _smallPool = null;
            
            _mediumPool.ReleaseAll();
            _mediumPool = null;
            
            _largePool.ReleaseAll();
            _largePool = null;

        }
        
        
    }
}