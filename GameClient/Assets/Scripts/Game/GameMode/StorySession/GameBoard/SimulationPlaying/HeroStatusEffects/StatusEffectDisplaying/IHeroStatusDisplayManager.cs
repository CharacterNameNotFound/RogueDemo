using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying
{
    public interface IHeroStatusDisplayManager
    {
        public UniTask Initialize(
            HeroStatusEffectDisplay playerDisplay, 
            HeroStatusEffectDisplay encounterDisplay,
            CancellationToken cancellationToken);

        public UniTask AddEffectIcon<T>(
            string text,
            HeroStatusEffectDisplay heroStatusEffectDisplay,
            CancellationToken cancellationToken) where T : IHeroStatusEffect;

        public UniTask UpdateEffectIcon<T>(
            string text,
            float progressPercentile,
            HeroStatusEffectDisplay heroStatusEffectDisplay,
            CancellationToken cancellationToken) where T : IHeroStatusEffect;
        
        public UniTask UpdateEffectIcon<T>(
            float percentile, 
            HeroStatusEffectDisplay heroStatusEffectDisplay, 
            CancellationToken cancellationToken) where T : IHeroStatusEffect;

        public UniTask UpdateEffectIcon<T>(
            string toString, 
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay, 
            CancellationToken heroStatusEffectDisplay);
        
        public UniTask RemoveItemEffectIcon<T>(
            HeroStatusEffectDisplay heroStatusEffectDisplay,
            CancellationToken cancellationToken) where T : IHeroStatusEffect;

        public void Clear();


    }
}