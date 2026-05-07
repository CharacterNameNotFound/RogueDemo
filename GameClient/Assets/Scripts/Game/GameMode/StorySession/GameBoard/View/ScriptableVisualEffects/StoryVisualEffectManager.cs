using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.ScriptedVisualEffectManagement;
using GameWideSystems.ScriptedVisualEffectManagement.FlyingTextScriptedVisualEffects;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.ScriptableVisualEffects
{
    public class StoryVisualEffectManager : IStoryVisualEffectManager
    {
        private FlyingTextScriptedVisualEffectRegisterer _flyingTextRegisterer;
        private IScriptedVisualEffectManager _scriptedVisualEffectManager;
        private StoryVisualEffectManagerConfigs _storyVisualEffectManagerConfigs;

        public StoryVisualEffectManager(
            FlyingTextScriptedVisualEffectRegisterer flyingTextRegisterer, 
            IScriptedVisualEffectManager scriptedVisualEffectManager, 
            StoryVisualEffectManagerConfigs storyVisualEffectManagerConfigs)
        {
            _flyingTextRegisterer = flyingTextRegisterer;
            _scriptedVisualEffectManager = scriptedVisualEffectManager;
            _storyVisualEffectManagerConfigs = storyVisualEffectManagerConfigs;
        }

        public UniTask Initialize(CancellationToken cancellationToken)
        {
            return _flyingTextRegisterer.Register(cancellationToken);
        }

        public async UniTask PlayFlyingText(
            Transform parent, 
            Vector3 startingPosition, 
            Vector3 destinationPosition, 
            string text, 
            float fontSize,
            CancellationToken cancellationToken)
        {
            FlyingTextScriptedVisualEffectParams configs = 
                new FlyingTextScriptedVisualEffectParams(
                    startingPosition, 
                    destinationPosition,
                    _storyVisualEffectManagerConfigs.Duration,
                    _storyVisualEffectManagerConfigs.FadeAtPercentile,
                    _storyVisualEffectManagerConfigs.FadeValue,
                    text,
                    fontSize);


            await _scriptedVisualEffectManager.PlayOnParent<FlyingTextScriptedVisualEffect>(parent, configs, cancellationToken);
        }

        public void Clear()
        {
            
        }
        
    }
}