using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    
    // ToDo add scrolling? For sure need some masking
    public class HeroStatusEffectDisplay : MonoBehaviour
    {
        public SpriteRenderer DisplayRenderer;
        
        private Vector3 _localStartPoint;
        private Vector3 _step;
        
        // ToDo: search optimization
        public List<HeroStatusEffectIcon> StatusEffectIcons = new();

        public void Initialize(Vector3 localStartPoint, Vector3 step)
        {
            _localStartPoint = localStartPoint;
            _step = step;
        }
        
        public void UpdateLine()
        {
            for (int i = 0; i < StatusEffectIcons.Count; i++)
            {
                StatusEffectIcons[i].transform.localPosition = _localStartPoint + i * _step;
            }
        }
        
    }
}