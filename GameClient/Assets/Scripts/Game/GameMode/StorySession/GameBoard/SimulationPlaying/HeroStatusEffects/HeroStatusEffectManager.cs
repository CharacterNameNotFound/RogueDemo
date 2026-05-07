using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public class HeroStatusEffectManager : IHeroStatusEffectManager
    {
        private IHeroStatusEffectHandlerRegistry _handlerRegistry;

        public HeroStatusEffectManager(IHeroStatusEffectHandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void Update(BattleSideCache battleSideCache, int owner, float deltaTime, CancellationToken cancellationToken)
        {
            // ToDo optimize
            foreach (IHeroStatusEffect item in battleSideCache.HeroStatusEffects.Values.ToArray())
            {
                UpdatedStatus(item, battleSideCache, owner, deltaTime, cancellationToken);
            }
        }

        public async UniTask PostBattleReset(BattleCache battleCache, CancellationToken cancellationToken)
        {
            
            BattleSideCache playerSide = battleCache.GetPlayer();
            
            // resetting player effects
            // ToDo optimize
            foreach (IHeroStatusEffect item in playerSide.HeroStatusEffects.Values.ToArray())
            {
                _handlerRegistry.Get(item.GetType(), out IHeroStatusEffectHandler statusHandler);

                await statusHandler.PostBattlePlayerReset(item, playerSide, cancellationToken);
            }
            
            BattleSideCache encounterSide = battleCache.GetEncounter();
            
            // resetting encounter effects
            foreach (IHeroStatusEffect item in encounterSide.HeroStatusEffects.Values.ToArray())
            {
                _handlerRegistry.Get(item.GetType(), out IHeroStatusEffectHandler statusHandler);

                await statusHandler.PostBattleEncounterReset(item, encounterSide, cancellationToken);
            }

            return;
        }

        private void UpdatedStatus(
            IHeroStatusEffect statusEffect, 
            BattleSideCache battleSideCache, 
            int owner,
            float deltaTime,
            CancellationToken cancellationToken)
        {
            _handlerRegistry.Get(statusEffect.GetType(), out IHeroStatusEffectHandler statusHandler);

            statusHandler.Update(statusEffect, battleSideCache, owner, deltaTime, cancellationToken);
        }
    }
}