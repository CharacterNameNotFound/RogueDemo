using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
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
    public class PoisonHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(PoisonHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;
        private IHeroStatusDisplayManager _heroStatusDisplayManager;
        private GameBoardHolder _gameBoardHolder;
        private IItemStatGetter _statGetter;
        
        public PoisonHeroStatusEffectHandler(
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
            PoisonHeroStatusEffect poison = (PoisonHeroStatusEffect) statusEffect;
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(owner), _gameBoardHolder.GameBoardComponent);

            if (HeroStatusEffectUtils.IsClearable(poison.PoisonIntensity.ItemValues))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return _heroStatusDisplayManager.RemoveItemEffectIcon<PoisonHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
            }

            poison.Cooldown -= deltaTime;

            if (poison.Cooldown > 0) 
            {
                float percentile = (poison.MaxCooldown - poison.Cooldown) / poison.MaxCooldown;
                return _heroStatusDisplayManager.UpdateEffectIcon<PoisonHeroStatusEffect>(percentile, heroGroupToHeroStatusDisplay, cancellationToken);
            }
            
            poison.Cooldown = poison.MaxCooldown;

            float intensity = _statGetter.GetStatValue(poison.PoisonIntensity, ItemStatType.Poison);

            _heroStatModificator.DealDamage(intensity, TargetCalculator.IndexToHeroGroup(owner));
            return _heroStatusDisplayManager.UpdateEffectIcon<PoisonHeroStatusEffect>(intensity.ToString(), 0, heroGroupToHeroStatusDisplay, cancellationToken);
        }

        public UniTask Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache, CancellationToken cancellationToken)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(target), _gameBoardHolder.GameBoardComponent);

            if (statusEffects.TryGetValue(typeof(RegenerationHeroStatusEffect), out IHeroStatusEffect regen))
            {
                RegenerationHeroStatusEffect regenEffect = (RegenerationHeroStatusEffect) regen;

                float regenerationIntensity = regenEffect.RegenerationIntensity.ItemValues.Stats[(int) StatSet.StatSetComponent.CombatBonus];
                float intensityDecrement = Mathf.Min(regenerationIntensity / 2, intensity);

                intensity -= intensityDecrement;
                regenerationIntensity -= intensityDecrement * 2;

                regenEffect.RegenerationIntensity.ItemValues.Stats[(int)StatSet.StatSetComponent.CombatBonus] = regenerationIntensity;
                
                if (intensity == 0)
                {
                    return UniTask.CompletedTask;
                }
            }
            
            if (!statusEffects.TryGetValue(typeof(PoisonHeroStatusEffect), out IHeroStatusEffect effect))
            {
                PoisonHeroStatusEffect status = new PoisonHeroStatusEffect();
                
                status.PoisonIntensity = new ItemStatEntry(new StatSet(), new StatSet(1,1,1,1, 1));
                
                status.PoisonIntensity.ItemValues.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(PoisonHeroStatusEffect), status);
                
                return _heroStatusDisplayManager.AddEffectIcon<PoisonHeroStatusEffect>(intensity.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);
            }

            PoisonHeroStatusEffect poisonStatus = (PoisonHeroStatusEffect) effect;
            poisonStatus.PoisonIntensity.ItemValues.Stats[(int)holderType] += Mathf.RoundToInt(intensity);
            
            float statValue = _statGetter.GetStatValue(poisonStatus.PoisonIntensity, ItemStatType.Poison);

            return _heroStatusDisplayManager.UpdateEffectIcon<PoisonHeroStatusEffect>(statValue.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        public UniTask PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide, CancellationToken cancellationToken)
        {
            PoisonHeroStatusEffect poison = (PoisonHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(poison.PoisonIntensity.ItemValues))
            {
                HeroStatusEffectUtils.ClearUnpreservable(poison.PoisonIntensity);
                
                return UniTask.CompletedTask;
            }

            playerSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Player, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<PoisonHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        public UniTask PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide, CancellationToken cancellationToken)
        {
            encounterSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Encounter, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<PoisonHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
    }
}