using System.Linq;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.Tags;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Prototyping.Items.Special
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