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
    public class RegenerationHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(RegenerationHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;

        public RegenerationHeroStatusEffectHandler(IHeroStatModificator heroStatModificator)
        {
            _heroStatModificator = heroStatModificator;
        }

        public void Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime)
        {
            RegenerationHeroStatusEffect regeneration = (RegenerationHeroStatusEffect) statusEffect;

            if (HeroStatusEffectUtils.IsClearable(regeneration.RegenerationIntensity))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return;
            }

            regeneration.Cooldown -= deltaTime;

            if (regeneration.Cooldown > 0) 
                return;
            
            regeneration.Cooldown = regeneration.MaxCooldown;

            float intensity = HeroStatusEffectUtils.GetIntensity(regeneration.RegenerationIntensity);

            _heroStatModificator.Heal(intensity, TargetCalculator.IndexToHeroGroup(owner));

        }

        public void Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;
            
            if (statusEffects.TryGetValue(typeof(PoisonHeroStatusEffect), out IHeroStatusEffect poison))
            {
                PoisonHeroStatusEffect poisonEffect = (PoisonHeroStatusEffect) poison;

                float poisonIntensity = poisonEffect.PoisonIntensity.Stats[(int) StatSet.StatSetComponent.CombatBonus];
                float intensityDecrement = Mathf.Min(poisonIntensity / 2, intensity);

                intensity -= intensityDecrement;
                poisonIntensity -= intensityDecrement * 2;

                poisonEffect.PoisonIntensity.Stats[(int)StatSet.StatSetComponent.CombatBonus] = poisonIntensity;
                
                if (intensity == 0)
                {
                    return;
                }
            }
            
            if (!statusEffects.TryGetValue(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffect effect))
            {
                RegenerationHeroStatusEffect status = new RegenerationHeroStatusEffect();
                
                status.RegenerationIntensity = new StatSet();
                
                status.RegenerationIntensity.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(RegenerationHeroStatusEffect), status);
                
                return;
            }

            RegenerationHeroStatusEffect burnHeroStatusEffect = (RegenerationHeroStatusEffect) effect;
            burnHeroStatusEffect.RegenerationIntensity.Stats[(int)holderType] += Mathf.RoundToInt(intensity);
        }

        public void PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide)
        {
            RegenerationHeroStatusEffect regeneration = (RegenerationHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(regeneration.RegenerationIntensity))
            {
                HeroStatusEffectUtils.ClearUnpreservable(regeneration.RegenerationIntensity);
                
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