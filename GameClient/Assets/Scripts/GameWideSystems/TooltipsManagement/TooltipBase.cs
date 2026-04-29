using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.TooltipsManagement;
using UnityEngine;
using UnityEngine.Rendering;
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

        public virtual UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken)
        {
            float pivotX = tooltipParams.Pivot.x / Screen.width;
            float pivotY = tooltipParams.Pivot.y / Screen.height;

            GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
            transform.position = tooltipParams.Pivot;

            return UniTask.CompletedTask;
        }

        public abstract UniTask Hide(CancellationToken cancellationToken);
    }
}