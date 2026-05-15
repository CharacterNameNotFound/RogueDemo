using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.BoardModelManipulation;
using Game.GameMode.StorySession.GameBoard.Services.HeroModification;
using Game.GameMode.StorySession.GameBoard.Services.HeroStatsDrawing;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Facades;
using Game.GameMode.StorySession.GameBoard.View;
using Game.GameMode.StorySession.GameBoard.View.Board;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Utilities;
using Game.Utilities.Shuffling;
using GameWideSystems.RNGManagement;
using UnityEngine;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles.Routines
{
    public class BattleEncounterRoutines : IBattleEncounterRoutines
    {
        private IItemPresenter _itemPresenter;
        private GameBoardHolder _gameBoardHolder;
        private IBoardModelManipulator _boardModelManipulator;
        private IHeroStatModificator _heroStatModificator;
        private IGameBoardModelHolder _gameBoardModelHolder;
        private IHeroesHpDrawer _heroesHpDrawer;
        private IItemRenderingFacade _itemRenderingFacade;
        private IObtainableItemExclusionListBuilder _obtainableItemExclusionList;
        private IRNGManager _rngManager;

        public BattleEncounterRoutines(
            IItemPresenter itemPresenter, 
            GameBoardHolder gameBoardHolder, 
            IBoardModelManipulator boardModelManipulator, 
            IHeroStatModificator heroStatModificator, 
            IGameBoardModelHolder gameBoardModelHolder, 
            IHeroesHpDrawer heroesHpDrawer, 
            IItemRenderingFacade itemRenderingFacade, 
            IRNGManager rngManager, 
            IObtainableItemExclusionListBuilder obtainableItemExclusionList)
        {
            _itemPresenter = itemPresenter;
            _gameBoardHolder = gameBoardHolder;
            _boardModelManipulator = boardModelManipulator;
            _heroStatModificator = heroStatModificator;
            _gameBoardModelHolder = gameBoardModelHolder;
            _heroesHpDrawer = heroesHpDrawer;
            _itemRenderingFacade = itemRenderingFacade;
            _rngManager = rngManager;
            _obtainableItemExclusionList = obtainableItemExclusionList;
        }

        public async UniTask ShowElements(BattleEncounter encounter, CancellationToken cancellationToken)
        {
            Sprite portrait = await encounter.Portrait.Load<Sprite>(cancellationToken);
            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.BattleView.Render(portrait);
            _gameBoardHolder.GameBoardComponent.EncounterBoard.SwitchToView(EncounterBoard.BoardType.Battle);
        }

        public UniTask PreBattleReset(float opponentHealth, CancellationToken cancellationToken)
        {
            SetOpponentHp(opponentHealth);
            return UniTask.CompletedTask;
        }

        public async UniTask LoadItemsUpdateViews(IEnumerable<string> items, CancellationToken cancellationToken)
        {
            _gameBoardHolder.GameBoardComponent.GameBoardInteractables.BattleScreenButton.gameObject.SetActive(false);
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            await _itemPresenter.ShowItems(encounterItemLine, items, cancellationToken);

            _boardModelManipulator.UpdateLine(ItemBoardGroup.Encounter);
            
        }

        public UniTask HideAll(CancellationToken cancellationToken)
        {
            
            // opponent items are "ephemeral"
            _itemPresenter.RemoveEncounterItemsImmediate(true);

            
            _gameBoardHolder.GameBoardComponent.EncounterBoard.HideCurrentView();
            _gameBoardHolder.GameBoardComponent.EncounterBoard.BattleView.Release();
            
            return UniTask.CompletedTask;
        }

        public UniTask PostBattleReset(CancellationToken cancellationToken)
        {
            _heroStatModificator.PostBattleReset();
            foreach (ItemContainerComponent item in _gameBoardHolder.GameBoardComponent.ItemLineViewController.PlayerItemLine.ItemContainerComponents)
            {
                if (item is null)
                {
                    continue;
                }
                
                _itemRenderingFacade.UpdateCharge(item, 1);
            }
            
            return UniTask.CompletedTask;
        }

        public async UniTask PresentLoot(List<string> items, CancellationToken cancellationToken)
        {
            _itemPresenter.RemoveEncounterItemsImmediate(true);
            
            HashSet<string> exclusionList = await _obtainableItemExclusionList.BuildIgnoredListIds(_gameBoardModelHolder.GameBoardModel, cancellationToken);

            List<string> spawnableLoot = items.Except(exclusionList).ToList();

            if (spawnableLoot.Count == 0)
            {
                return;
            }
            
            spawnableLoot.ShuffleListDurstenfeld(_rngManager.GetRandomProvider(RNGGroup.Encounter));
            
            ItemLineComponent encounterItemLine = _gameBoardHolder.GameBoardComponent.ItemLineViewController.EncounterItemLine;
            await _itemPresenter.ShowItems(encounterItemLine, new []{ spawnableLoot.First() }, cancellationToken);
        }

        private void SetOpponentHp(float health)
        {
            _gameBoardModelHolder.GameBoardModel.EncounterHeroStats.MaxHp = health;
            _gameBoardModelHolder.GameBoardModel.EncounterHeroStats.Hp = health;
            
            _heroesHpDrawer.UpdateHeroesHpBars();
        }
        
    }
}