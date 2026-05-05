using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectHandlers
{
    public class BurnHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(BurnHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;

        public BurnHeroStatusEffectHandler(IHeroStatModificator heroStatModificator)
        {
            _heroStatModificator = heroStatModificator;
        }

        public void Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime)
        {
            BurnHeroStatusEffect burnStatus = (BurnHeroStatusEffect) statusEffect;

            if (HeroStatusEffectUtils.IsClearable(burnStatus.BurnIntensity))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return;
            }

            burnStatus.Cooldown -= deltaTime;

            if (burnStatus.Cooldown > 0) 
                return;
            
            burnStatus.Cooldown = burnStatus.MaxCooldown;

            float intensity = HeroStatusEffectUtils.GetIntensity(burnStatus.BurnIntensity);
            
            float currentBattleIntensity = burnStatus.BurnIntensity.Stats[(int) StatSet.StatSetComponent.CombatBonus];
            currentBattleIntensity = Mathf.FloorToInt(currentBattleIntensity * 0.9f);
            burnStatus.BurnIntensity.Stats[(int)StatSet.StatSetComponent.CombatBonus] = currentBattleIntensity;

            _heroStatModificator.DealDamage(intensity, TargetCalculator.IndexToHeroGroup(owner));

        }

        public void Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;
            
            if (!statusEffects.TryGetValue(typeof(BurnHeroStatusEffect), out IHeroStatusEffect effect))
            {
                BurnHeroStatusEffect status = new BurnHeroStatusEffect();
                
                status.BurnIntensity = new StatSet();
                
                status.BurnIntensity.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(BurnHeroStatusEffect), status);
                
                return;
            }

            BurnHeroStatusEffect burnHeroStatusEffect = (BurnHeroStatusEffect) effect;
            burnHeroStatusEffect.BurnIntensity.Stats[(int)holderType] += Mathf.RoundToInt(intensity);
        }
        
        public void PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide)
        {
            BurnHeroStatusEffect burn = (BurnHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(burn.BurnIntensity))
            {
                HeroStatusEffectUtils.ClearUnpreservable(burn.BurnIntensity);
                
                return;
            }

            playerSide.HeroStatusEffects.Remove(item.GetType());
        }
        
        public void PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide)
        {
            encounterSide.HeroStatusEffects.Remove(item.GetType());
        }
        
        
    }
}