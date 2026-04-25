using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.View.Board.Views;
using Game.GameMode.StorySession.StoryLoop.StoryScripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters
{
    public class BattleEncounter : Encounter
    {
        [field: Header("Battle encounter configs")]
        [field: SerializeField] public AssetReferenceSprite VerticalPortrait { get; protected set; }
        
        [field: Space(10)]
        [field: SerializeField] public int CoinReward { get; set; }
        [field: SerializeField] public int ExperienceReward { get; set; }
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public List<AssetReference> Items { get; set; } = new(12);
        
        public override EncounterType EncounterType => EncounterType.Battle;
        
        public override UniTask Play(IStoryContext storyContext, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanMoveItem(ItemContainerComponent itemContainer)
        {
            return false;
        }
    }
}