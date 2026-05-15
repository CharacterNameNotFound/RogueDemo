using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.GameBoard.View.Utils;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    public class ItemFrameParticleShortcuts : IItemFrameParticleShortcuts
    {
        private GameBoardHolder _gameBoardHolder;
        private IStoryVisualEffectManager _storyVisualEffectManager;
        private ItemFrameParticlesConfigs _configs;

        public ItemFrameParticleShortcuts(
            GameBoardHolder gameBoardHolder, 
            IStoryVisualEffectManager storyVisualEffectManager, 
            ItemFrameParticlesConfigs configs)
        {
            _gameBoardHolder = gameBoardHolder;
            _storyVisualEffectManager = storyVisualEffectManager;
            _configs = configs;
        }

        public async UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> GetHasteParticles(int index, int owner, CancellationToken cancellationToken)
        {
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(owner);
            
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);

            ItemContainerComponent itemContainerComponent = originalItemLine.ItemContainerComponents[index];

            Vector3 size = ItemContainerToEffectScale(itemContainerComponent);
            
            ItemFrameParticlesParameters parameters = new ItemFrameParticlesParameters(size, _configs.HasteColor);

            return (await _storyVisualEffectManager.GetItemFameParticleSystem(parameters, cancellationToken), parameters);
        }

        public async UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)> GetSlowParticles(int index, int owner, CancellationToken cancellationToken)
        {
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(owner);
            
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);

            ItemContainerComponent itemContainerComponent = originalItemLine.ItemContainerComponents[index];

            Vector3 size = ItemContainerToEffectScale(itemContainerComponent);
            
            ItemFrameParticlesParameters parameters = new ItemFrameParticlesParameters(size, _configs.SlowColor);

            return (await _storyVisualEffectManager.GetItemFameParticleSystem(parameters, cancellationToken), parameters);
        }

        private Vector3 ItemContainerToEffectScale(ItemContainerComponent itemContainerComponent)
        {
            Vector3 size = itemContainerComponent.ItemRenderer.bounds.size;

            float sizeX = size.x * _configs.ShapeSizeAdjustment.x;
            // In particle shape Y and Z are "swapped"
            float sizeZ = size.y * _configs.ShapeSizeAdjustment.y;

            return new Vector3(sizeX, 1, sizeZ);
        }
        
    }
}