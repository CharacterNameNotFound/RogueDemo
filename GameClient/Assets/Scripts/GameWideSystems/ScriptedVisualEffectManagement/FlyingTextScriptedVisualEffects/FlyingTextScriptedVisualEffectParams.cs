using UnityEngine;

namespace GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects
{
    public class FlyingTextScriptedVisualEffectParams : ScriptedVisualEffectParams
    {
        public Vector3 StartPosition;
        public Vector3 DestinationPosition;
        public float Duration;
        public float FadeAtPercentile;
        public float FadeValue;
        public string Text;
        public float FontSize;

        public FlyingTextScriptedVisualEffectParams(
            Vector3 startPosition, 
            Vector3 destinationPosition, 
            float duration, 
            float fadeAtPercentile, 
            float fadeValue, 
            string text, 
            float fontSize)
        {
            StartPosition = startPosition;
            DestinationPosition = destinationPosition;
            Duration = duration;
            FadeAtPercentile = fadeAtPercentile;
            FadeValue = fadeValue;
            Text = text;
            FontSize = fontSize;
        }
    }
}