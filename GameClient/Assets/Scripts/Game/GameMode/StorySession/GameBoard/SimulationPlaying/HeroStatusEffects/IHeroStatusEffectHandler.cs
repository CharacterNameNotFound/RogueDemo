using System;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Utils.UtilityTypes.AutoDictionaries;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects
{
    public interface IHeroStatusEffectHandler : IAutoDictionaryEntry<Type>
    {
        public void Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime);
        public void Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache);
        public void PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide);
        public void PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide);
        
    }
}