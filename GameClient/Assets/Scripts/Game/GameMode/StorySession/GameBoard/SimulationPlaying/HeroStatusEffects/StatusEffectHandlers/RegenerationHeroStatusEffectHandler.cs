using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectDisplaying;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.GameBoard.View.Utils;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.HeroStatusEffects.StatusEffectHandlers
{
    public class RegenerationHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(RegenerationHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;
        private IHeroStatusDisplayManager _heroStatusDisplayManager;
        private GameBoardHolder _gameBoardHolder;
        private IItemStatGetter _statGetter;

        public RegenerationHeroStatusEffectHandler(
            IHeroStatModificator heroStatModificator, 
            IHeroStatusDisplayManager heroStatusDisplayManager, 
            GameBoardHolder gameBoardHolder, 
            IItemStatGetter statGetter)
        {
            _heroStatModificator = heroStatModificator;
            _heroStatusDisplayManager = heroStatusDisplayManager;
            _gameBoardHolder = gameBoardHolder;
            _statGetter = statGetter;
        }

        public UniTask Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime, CancellationToken cancellationToken)
        {
            RegenerationHeroStatusEffect regeneration = (RegenerationHeroStatusEffect) statusEffect;

            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(owner), _gameBoardHolder.GameBoardComponent);
            
            if (HeroStatusEffectUtils.IsClearable(regeneration.RegenerationIntensity.ItemValues))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return _heroStatusDisplayManager.RemoveItemEffectIcon<RegenerationHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
            }

            regeneration.Cooldown -= deltaTime;

            if (regeneration.Cooldown > 0) 
            {
                float percentile = (regeneration.MaxCooldown - regeneration.Cooldown) / regeneration.MaxCooldown;
                return _heroStatusDisplayManager.UpdateEffectIcon<RegenerationHeroStatusEffect>(percentile, heroGroupToHeroStatusDisplay, cancellationToken);
            }
            
            regeneration.Cooldown = regeneration.MaxCooldown;

            float intensity = _statGetter.GetStatValue(regeneration.RegenerationIntensity, ItemStatType.Regeneration);

            _heroStatModificator.Heal(intensity, TargetCalculator.IndexToHeroGroup(owner));
            return _heroStatusDisplayManager.UpdateEffectIcon<RegenerationHeroStatusEffect>(intensity.ToString(), 0, heroGroupToHeroStatusDisplay, cancellationToken);
        }

        public UniTask Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache, CancellationToken cancellationToken)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(target), _gameBoardHolder.GameBoardComponent);
            
            if (statusEffects.TryGetValue(typeof(PoisonHeroStatusEffect), out IHeroStatusEffect poison))
            {
                PoisonHeroStatusEffect poisonEffect = (PoisonHeroStatusEffect) poison;

                float poisonIntensity = poisonEffect.PoisonIntensity.ItemValues.Stats[(int) StatSet.StatSetComponent.CombatBonus];
                float intensityDecrement = Mathf.Min(poisonIntensity / 2, intensity);

                intensity -= intensityDecrement;
                poisonIntensity -= intensityDecrement * 2;

                poisonEffect.PoisonIntensity.ItemValues.Stats[(int)StatSet.StatSetComponent.CombatBonus] = poisonIntensity;
                
                if (intensity == 0)
                {
                    return UniTask.CompletedTask;
                }
            }
            
            if (!statusEffects.TryGetValue(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffect effect))
            {
                RegenerationHeroStatusEffect status = new RegenerationHeroStatusEffect();
                
                status.RegenerationIntensity = new ItemStatEntry(new StatSet(), new StatSet(1,1,1,1, 1));
                
                status.RegenerationIntensity.ItemValues.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(RegenerationHeroStatusEffect), status);
                
                return _heroStatusDisplayManager.AddEffectIcon<RegenerationHeroStatusEffect>(intensity.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);
            }

            RegenerationHeroStatusEffect regenerationHeroStatusEffect = (RegenerationHeroStatusEffect) effect;
            regenerationHeroStatusEffect.RegenerationIntensity.ItemValues.Stats[(int)holderType] += Mathf.RoundToInt(intensity);
            
            float statValue = _statGetter.GetStatValue(regenerationHeroStatusEffect.RegenerationIntensity, ItemStatType.Poison);

            return _heroStatusDisplayManager.UpdateEffectIcon<RegenerationHeroStatusEffect>(statValue.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);

        }

        public UniTask PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide, CancellationToken cancellationToken)
        {
            RegenerationHeroStatusEffect regeneration = (RegenerationHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(regeneration.RegenerationIntensity.ItemValues))
            {
                HeroStatusEffectUtils.ClearUnpreservable(regeneration.RegenerationIntensity);
                
                return UniTask.CompletedTask;
            }

            playerSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Player, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<RegenerationHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        public UniTask PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide, CancellationToken cancellationToken)
        {
            encounterSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Encounter, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<RegenerationHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        
    }
}