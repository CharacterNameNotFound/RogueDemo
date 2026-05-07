using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Views
{
    public class HeroStatusEffectIcon : MonoBehaviour, IPoolableEntity
    {
        private static readonly int UndrawnPart = Shader.PropertyToID("_UndrawnPart");

        
        [field: SerializeField] private SpriteRenderer _icon;
        [field: SerializeField] private SpriteRenderer _firm;
        [field: SerializeField] private TMP_Text _text;

        public Type StatusEffect;
        
        public void Initialize(Sprite sprite, string text, Type statusEffect)
        {
            _icon.sprite = sprite;

            StatusEffect = statusEffect;
            
            UpdateProgress(text, 0);
        }

        public void UpdateProgress(string text, float percentile)
        {
            _text.text = text;
            _firm.material.SetFloat(UndrawnPart, percentile);
        }
        
        public void UpdateProgress(float percentile)
        {
            _firm.material.SetFloat(UndrawnPart, percentile);
        }
        
        public void UpdateProgress(string text)
        {
            _text.text = text;
        }
        
        
        public void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }

        
    }
}