using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.TooltipsManagement;
using Utils.UtilityTypes.ObjectPooling;

namespace Game.UI.Tooltips
{
    public abstract class TooltipBase : PoolableGameObject, ITooltip
    {
        public abstract TooltipType TooltipType { get; }

        public override void OnPooled()
        {
            gameObject.SetActive(false);
        }

        public abstract override void Dispose();

        public abstract UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken);

        public abstract UniTask Hide(CancellationToken cancellationToken);
    }
}