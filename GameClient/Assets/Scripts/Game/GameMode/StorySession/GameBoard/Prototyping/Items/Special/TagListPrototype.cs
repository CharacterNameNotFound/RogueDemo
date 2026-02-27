using System.Collections.Generic;
using System.Linq;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Special.Tags;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Special
{
    public class TagListPrototype : MonoBehaviour
    {
        public ItemTag[] ItemTags;

        public TagList GetTagList()
        {
            return new TagList(ItemTags.Distinct().ToList());
        }
        
    }
}