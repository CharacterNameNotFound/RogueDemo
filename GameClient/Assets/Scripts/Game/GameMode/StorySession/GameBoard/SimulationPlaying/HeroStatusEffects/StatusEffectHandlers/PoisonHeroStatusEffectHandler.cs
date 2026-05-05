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
    public class PoisonHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(PoisonHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;

        public PoisonHeroStatusEffectHandler(IHeroStatModificator heroStatModificator)
        {
            _heroStatModificator = heroStatModificator;
        }

        public void Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime)
        {
            PoisonHeroStatusEffect poison = (PoisonHeroStatusEffect) statusEffect;

            if (HeroStatusEffectUtils.IsClearable(poison.PoisonIntensity))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return;
            }

            poison.Cooldown -= deltaTime;

            if (poison.Cooldown > 0) 
                return;
            
            poison.Cooldown = poison.MaxCooldown;

            float intensity = HeroStatusEffectUtils.GetIntensity(poison.PoisonIntensity);

            _heroStatModificator.DealDamage(intensity, TargetCalculator.IndexToHeroGroup(owner));

        }

        public void Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;

            if (statusEffects.TryGetValue(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffect regen))
            {
                RegenerationHeroStatusEffect regenEffect = (RegenerationHeroStatusEffect) regen;

                float regenerationIntensity = regenEffect.RegenerationIntensity.Stats[(int) StatSet.StatSetComponent.CombatBonus];
                float intensityDecrement = Mathf.Min(regenerationIntensity / 2, intensity);

                intensity -= intensityDecrement;
                regenerationIntensity -= intensityDecrement * 2;

                regenEffect.RegenerationIntensity.Stats[(int)StatSet.StatSetComponent.CombatBonus] = regenerationIntensity;
                
                if (intensity == 0)
                {
                    return;
                }
            }
            
            if (!statusEffects.TryGetValue(typeof(PoisonHeroStatusEffect), out IHeroStatusEffect effect))
            {
                PoisonHeroStatusEffect status = new PoisonHeroStatusEffect();
                
                status.PoisonIntensity = new StatSet();
                
                status.PoisonIntensity.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(PoisonHeroStatusEffect), status);
                
                return;
            }

            PoisonHeroStatusEffect burnHeroStatusEffect = (PoisonHeroStatusEffect) effect;
            burnHeroStatusEffect.PoisonIntensity.Stats[(int)holderType] += Mathf.RoundToInt(intensity);
        }
        
        public void PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide)
        {
            PoisonHeroStatusEffect poison = (PoisonHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(poison.PoisonIntensity))
            {
                HeroStatusEffectUtils.ClearUnpreservable(poison.PoisonIntensity);
                
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