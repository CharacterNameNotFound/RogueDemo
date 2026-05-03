using TMPro;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Board.Elements
{
    public class HpBarComponent : MonoBehaviour
    {
        private static readonly int HpPropertyId = Shader.PropertyToID("_Hp");
        
        [SerializeField] private SpriteRenderer _hpBar;
        [SerializeField] private TMP_Text _hpText;

        public void UpdateHpBar(float hp, float maxHp)
        {
            _hpBar.material.SetFloat(HpPropertyId, hp / maxHp);

            int intHp = Mathf.CeilToInt(hp);
            int intMaxHp = Mathf.CeilToInt(maxHp);
            
            _hpText.text = $"{intHp}/{intMaxHp}";
        }
        
        
    }
}