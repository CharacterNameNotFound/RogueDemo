using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using GameWideSystems.TooltipsManagement;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.UI.Tooltips
{
    public class TextTooltip : TooltipBase
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;
        
        public override TooltipType TooltipType => TooltipType.GenericText;
        

        public override async UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken)
        {
            await base.Show(tooltipParams, cancellationToken);
            await GenericUIEntranceRoutines.ShowInstantly(gameObject, cancellationToken);
        }

        public override async UniTask Hide(CancellationToken cancellationToken)
        {
            await GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }
        
        public override void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}