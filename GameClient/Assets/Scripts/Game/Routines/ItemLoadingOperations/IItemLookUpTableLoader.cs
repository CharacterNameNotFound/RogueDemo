using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace Game.Routines.ItemLoadingOperations
{
    public interface IItemLookUpTableLoader
    {
        public UniTask<ProcedureResult> BuildItemLookUp(CancellationToken cancellationToken);
    }
}