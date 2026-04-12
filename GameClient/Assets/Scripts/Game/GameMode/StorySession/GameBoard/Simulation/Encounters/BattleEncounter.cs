using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Encounters
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
        
    }
}