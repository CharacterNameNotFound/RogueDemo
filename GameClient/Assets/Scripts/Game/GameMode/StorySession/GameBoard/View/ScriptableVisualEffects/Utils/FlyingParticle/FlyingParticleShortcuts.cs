using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.GameBoard.View.Utils;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingParticleScriptedVisualEffect;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.Utils.FlyingParticle
{
    public class FlyingParticleShortcuts : IFlyingParticleShortcuts
    {
        private GameBoardHolder _gameBoardHolder;
        private IStoryVisualEffectManager _storyVisualEffectManager;
        private FlyingParticleConfigs _flyingParticleConfigs;

        public FlyingParticleShortcuts(
            GameBoardHolder gameBoardHolder, 
            IStoryVisualEffectManager storyVisualEffectManager, 
            FlyingParticleConfigs flyingParticleConfigs)
        {
            _gameBoardHolder = gameBoardHolder;
            _storyVisualEffectManager = storyVisualEffectManager;
            _flyingParticleConfigs = flyingParticleConfigs;
        }


        public UniTask PlayHasteParticle(
            int itemOriginIndex, 
            int itemOriginOwner,
            int itemDestinationIndex, 
            int itemDestinationOwner,
            CancellationToken cancellationToken)
        {
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(itemOriginOwner);
            HeroGroup destinationOwner = TargetCalculator.IndexToHeroGroup(itemDestinationOwner);
            
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);
            ItemLineComponent destinationItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(destinationOwner, _gameBoardHolder.GameBoardComponent);

            FlyingParticleParameters flyingParticleParameters = FlyingParticleParametersBuilder.BuildParameters(
                originalItemLine.ItemContainerComponents[itemOriginIndex], 
                destinationItemLine.ItemContainerComponents[itemDestinationIndex], _flyingParticleConfigs);

            flyingParticleParameters.Color = _flyingParticleConfigs.HasteColor;
            
            
            return _storyVisualEffectManager.PlayFlyingParticleVFX(flyingParticleParameters, cancellationToken);
        }

        public UniTask PlaySlowParticle(
            int itemOriginIndex, 
            int itemOriginOwner,
            int itemDestinationIndex, 
            int itemDestinationOwner,
            CancellationToken cancellationToken)
        {
            HeroGroup originalOwner = TargetCalculator.IndexToHeroGroup(itemOriginOwner);
            HeroGroup destinationOwner = TargetCalculator.IndexToHeroGroup(itemDestinationOwner);
            
            ItemLineComponent originalItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(originalOwner, _gameBoardHolder.GameBoardComponent);
            ItemLineComponent destinationItemLine = GameBoardComponentShortcuts.HeroGroupToItemLineComponent(destinationOwner, _gameBoardHolder.GameBoardComponent);

            FlyingParticleParameters flyingParticleParameters = FlyingParticleParametersBuilder.BuildParameters(
                originalItemLine.ItemContainerComponents[itemOriginIndex], 
                destinationItemLine.ItemContainerComponents[itemDestinationIndex], _flyingParticleConfigs);
            
            flyingParticleParameters.Color = _flyingParticleConfigs.SlowColor;
            
            
            return _storyVisualEffectManager.PlayFlyingParticleVFX(flyingParticleParameters, cancellationToken);
        }



    }
}