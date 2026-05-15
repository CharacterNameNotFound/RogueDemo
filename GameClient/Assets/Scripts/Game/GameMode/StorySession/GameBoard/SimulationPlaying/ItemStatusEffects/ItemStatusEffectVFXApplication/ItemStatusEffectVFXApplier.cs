using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle;
using Game.GameMode.StorySession.GameBoard.View.Utils;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.ItemStatusEffects.ItemStatusEffectVFXApplication
{
    public class ItemStatusEffectVFXApplier : IItemStatusEffectVFXApplier
    {
        private GameBoardHolder _gameBoardHolder;

        public ItemStatusEffectVFXApplier(GameBoardHolder gameBoardHolder)
        {
            _gameBoardHolder = gameBoardHolder;
        }

        public async UniTask ApplyItemFrameParticles<T>(
            Func<int, int, CancellationToken, UniTask<(ItemFrameParticles, ItemFrameParticlesParameters)>> effectGetter, 
            int itemIndex, 
            int ownerIndex, 
            CancellationToken cancellationToken) where T : IItemStatusEffect
        {
            (ItemFrameParticles itemFrameParticles, ItemFrameParticlesParameters parameters)  = await effectGetter.Invoke(itemIndex, ownerIndex, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(ownerIndex);
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);
            ItemContainerComponent itemContainerComponent = originalItemLine.ItemContainerComponents[itemIndex];

            itemContainerComponent.VFXHolder.Register<T>(itemFrameParticles.transform);
            await itemFrameParticles.Play(parameters, cancellationToken);
        }

        public void RemoveItemFrameParticles(Type type, int itemIndex, int ownerIndex)
        {
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(ownerIndex);
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);
            ItemContainerComponent itemContainerComponent = originalItemLine.ItemContainerComponents[itemIndex];

            itemContainerComponent.VFXHolder.Unregister(type).GetComponent<ItemFrameParticles>().Return();
        }
        
            
            
    }
}