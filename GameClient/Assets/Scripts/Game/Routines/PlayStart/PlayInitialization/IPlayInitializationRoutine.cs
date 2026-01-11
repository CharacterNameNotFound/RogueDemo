using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace Game.Routines.PlayStart.PlayInitialization
{
    public interface IPlayInitializationRoutine
    {
        public UniTask<ProcedureResult> InitializeSession(CancellationToken cancellationToken);

    }
}