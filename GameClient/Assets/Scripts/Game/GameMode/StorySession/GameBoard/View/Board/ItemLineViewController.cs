using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemLineOrganization;
using UnityEngine;
using Utils.UtilityTypes.LifeCycle;

namespace Game.GameMode.StorySession.GameBoard.View.Board
{
    public class ItemLineViewController : MonoBehaviour, IInitializableGameObject
    {
        public ItemLineComponent PlayerItemLine;
        public ItemLineComponent InventoryItemLine;
        public ItemLineComponent EncounterItemLine;

        private List<ItemLineComponent> _itemLines;
        
        public UniTask Initialize(CancellationToken cancellationToken)
        {
            _itemLines = new List<ItemLineComponent>() { PlayerItemLine, InventoryItemLine, EncounterItemLine };
            return UniTask.CompletedTask;
        }
        
        public IEnumerable<ItemLineComponent> EnumerateItemLines()
        {
            return _itemLines;
        }

        
    }
}