using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public interface IHeroStatusEffectHandler : IAutoDictionaryEntry<Type>
    {
        public UniTask Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime, CancellationToken cancellationToken);
        public UniTask Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache, CancellationToken cancellationToken);
        public UniTask PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide, CancellationToken cancellationToken);
        public UniTask PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide, CancellationToken cancellationToken);
        
    }
}