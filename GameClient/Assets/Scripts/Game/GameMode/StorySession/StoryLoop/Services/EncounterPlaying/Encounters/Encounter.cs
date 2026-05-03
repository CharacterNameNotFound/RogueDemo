using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using GameWideSystems.LocalizationWrapper;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters
{
    public abstract class Encounter : ScriptableObject
    {
        
        [field: Header("Encounter configs")]
        [field: SerializeField] public string EncounterId { get; protected set; }
        [field: SerializeField] public LocalizedLineKey Name { get; protected set; }
        [field: SerializeField] public LocalizedLineKey Description { get; protected set; }
        [field: SerializeField] public AssetReferenceSprite Portrait { get; protected set; }

        [field: Space(10)]
        [field: SerializeField] public bool IsRepeatable { get; private set; } = true;

        public abstract EncounterType EncounterType { get; }
        
        public abstract UniTask Play(GameBoardModel gameBoardModel, CancellationToken cancellationToken);
        
        public virtual string[] GetPreloadedItemIds()
        {
            return Array.Empty<string>();
        }
        
        public virtual bool CanMoveItem(ItemContainerComponent itemContainer)
        {
            return true;
        }
        
        public virtual bool CanSellItem(object itemContainer)
        {
            return true;
        }

        public UniTask PreItemMove(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public virtual bool CanPurchase(Item item)
        {
            return false;
        }
    }
}