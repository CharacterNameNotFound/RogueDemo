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
    public class BurnHeroStatusEffectHandler : IHeroStatusEffectHandler
    {
        public Type AutoDictionaryKey => typeof(BurnHeroStatusEffect);

        private IHeroStatModificator _heroStatModificator;
        private IHeroStatusDisplayManager _heroStatusDisplayManager;
        private GameBoardHolder _gameBoardHolder;
        private IItemStatGetter _statGetter;

        public BurnHeroStatusEffectHandler(
            IHeroStatModificator heroStatModificator, 
            IHeroStatusDisplayManager heroStatusDisplayManager, 
            IItemStatGetter statGetter, 
            GameBoardHolder gameBoardHolder)
        {
            _heroStatModificator = heroStatModificator;
            _heroStatusDisplayManager = heroStatusDisplayManager;
            _statGetter = statGetter;
            _gameBoardHolder = gameBoardHolder;
        }

        public UniTask Update(IHeroStatusEffect statusEffect, BattleSideCache battleSideCache, int owner, float deltaTime, CancellationToken cancellationToken)
        {
            BurnHeroStatusEffect burnStatus = (BurnHeroStatusEffect) statusEffect;
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(owner), _gameBoardHolder.GameBoardComponent);

            if (HeroStatusEffectUtils.IsClearable(burnStatus.BurnIntensity.ItemValues))
            {
                battleSideCache.HeroStatusEffects.Remove(statusEffect.GetType());
                
                return _heroStatusDisplayManager.RemoveItemEffectIcon<BurnHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
            }

            burnStatus.Cooldown -= deltaTime;

            if (burnStatus.Cooldown > 0)
            {
                float percentile = (burnStatus.MaxCooldown - burnStatus.Cooldown) / burnStatus.MaxCooldown;
                return _heroStatusDisplayManager.UpdateEffectIcon<BurnHeroStatusEffect>(percentile, heroGroupToHeroStatusDisplay, cancellationToken);
            }
            
            burnStatus.Cooldown = burnStatus.MaxCooldown;

            float intensity = _statGetter.GetStatValue(burnStatus.BurnIntensity, ItemStatType.Burn);
            
            float currentBattleIntensity = burnStatus.BurnIntensity.ItemValues.Stats[(int) StatSet.StatSetComponent.CombatBonus];
            currentBattleIntensity = Mathf.FloorToInt(currentBattleIntensity * 0.9f);
            burnStatus.BurnIntensity.ItemValues.Stats[(int)StatSet.StatSetComponent.CombatBonus] = currentBattleIntensity;

            _heroStatModificator.DealDamage(intensity, TargetCalculator.IndexToHeroGroup(owner));
            
            intensity = _statGetter.GetStatValue(burnStatus.BurnIntensity, ItemStatType.Burn);
            return _heroStatusDisplayManager.UpdateEffectIcon<BurnHeroStatusEffect>(intensity.ToString(), 0, heroGroupToHeroStatusDisplay, cancellationToken);
        }

        public UniTask Apply(int target, float intensity, StatSet.StatSetComponent holderType, BattleCache battleCache, CancellationToken cancellationToken)
        {
            Dictionary<Type, IHeroStatusEffect> statusEffects = battleCache.Get(target).HeroStatusEffects;
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(TargetCalculator.IndexToHeroGroup(target), _gameBoardHolder.GameBoardComponent);
            
            if (!statusEffects.TryGetValue(typeof(BurnHeroStatusEffect), out IHeroStatusEffect effect))
            {
                BurnHeroStatusEffect status = new BurnHeroStatusEffect();
                
                status.BurnIntensity = new ItemStatEntry(new StatSet(), new StatSet(1,1,1,1, 1));
                
                status.BurnIntensity.ItemValues.Stats[(int)holderType] = Mathf.RoundToInt(intensity);
                statusEffects.Add(typeof(BurnHeroStatusEffect), status);
                
                return _heroStatusDisplayManager.AddEffectIcon<BurnHeroStatusEffect>(intensity.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);
            }

            BurnHeroStatusEffect burnHeroStatusEffect = (BurnHeroStatusEffect) effect;
            burnHeroStatusEffect.BurnIntensity.ItemValues.Stats[(int)holderType] += Mathf.RoundToInt(intensity);

            float statValue = _statGetter.GetStatValue(burnHeroStatusEffect.BurnIntensity, ItemStatType.Burn);

            return _heroStatusDisplayManager.UpdateEffectIcon<BurnHeroStatusEffect>(statValue.ToString(), heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        public UniTask PostBattlePlayerReset(IHeroStatusEffect item, BattleSideCache playerSide, CancellationToken cancellationToken)
        {
            BurnHeroStatusEffect burn = (BurnHeroStatusEffect) item;

            if (HeroStatusEffectUtils.IsPreservable(burn.BurnIntensity.ItemValues))
            {
                HeroStatusEffectUtils.ClearUnpreservable(burn.BurnIntensity);
                
                return UniTask.CompletedTask;
            }
            
            playerSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Player, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<BurnHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        public UniTask PostBattleEncounterReset(IHeroStatusEffect item, BattleSideCache encounterSide, CancellationToken cancellationToken)
        {
            encounterSide.HeroStatusEffects.Remove(item.GetType());
            
            HeroStatusEffectDisplay heroGroupToHeroStatusDisplay = GameBoardComponentShortcuts.HeroGroupToHeroStatusDisplay(HeroGroup.Encounter, _gameBoardHolder.GameBoardComponent);
            return _heroStatusDisplayManager.RemoveItemEffectIcon<BurnHeroStatusEffect>(heroGroupToHeroStatusDisplay, cancellationToken);
        }
        
        
    }
}