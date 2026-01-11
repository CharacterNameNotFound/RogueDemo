using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace Game.GameSaveSystem
{
    public interface IBlobManager
    {
        public SaveBlob Blob { get; }
        public UniTask<ProcedureResult> CreateNew(CancellationToken cancellationToken);
        public UniTask<ProcedureResult> TryLoadBlob(CancellationToken cancellationToken);
        public UniTask<ProcedureResult> ReadBlob(CancellationToken cancellationToken);
        public UniTask<ProcedureResult> WriteBlob(CancellationToken cancellationToken);

    }
}