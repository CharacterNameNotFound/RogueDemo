using GameWideSystems.TooltipsManagement;
using UnityEngine;

namespace Game.UI.Tooltips
{
    public class TextTooltipParams : TooltipParams
    {
        public string Text;

        public TextTooltipParams(string text, Vector2 position) : base(position)
        {
            Text = text;
        }
    }
}