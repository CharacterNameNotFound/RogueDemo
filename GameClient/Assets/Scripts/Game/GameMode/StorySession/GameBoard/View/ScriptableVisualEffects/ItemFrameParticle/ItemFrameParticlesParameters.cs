using GameWideSystems.ScriptedVisualEffectManagement;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects.ItemFrameParticle
{
    public class ItemFrameParticlesParameters : ScriptedVisualEffectParams
    {
        public Vector3 ShapeScale;
        public Color Color;

        public ItemFrameParticlesParameters(Vector3 shapeScale, Color color)
        {
            ShapeScale = shapeScale;
            Color = color;
        }
        
    }
}