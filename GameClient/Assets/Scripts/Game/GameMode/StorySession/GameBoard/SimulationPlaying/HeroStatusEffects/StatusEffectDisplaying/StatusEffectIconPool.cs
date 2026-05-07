using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying
{
    public class StatusEffectIconPool : GameObjectPool<HeroStatusEffectIcon>
    {
        public StatusEffectIconPool(List<HeroStatusEffectIcon> pool, IPoolEntityBuilder<HeroStatusEffectIcon> entityBuilder, IPooledObjectHostProvider pooledObjectHostProvider) : base(pool, entityBuilder, pooledObjectHostProvider)
        {
        }
    }
}