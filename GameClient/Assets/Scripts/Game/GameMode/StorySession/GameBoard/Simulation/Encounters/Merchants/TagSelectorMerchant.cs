using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties.Special.Tags;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Encounters.Merchants
{
    public class TagSelectorMerchant : MerchantEncounter
    {
        [field: Space(20)]
        [field: Header("Tag Selector Merchant configs")]
        [field: Tooltip("If checked items containing all tags simultaneously will be selected")]
        [field: SerializeField] public bool OnlyExactTagSequence { get; set; }
        [field: SerializeField] public ItemSelectionGroup IncludedGroups { get; set; }
        [field: SerializeField] public List<ItemTag> FilterTags { get; set; }
        
        protected override Item GetItems()
        {
            throw new System.NotImplementedException();
        }
    }
}