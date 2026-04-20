using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.Services.SaveManagement
{
    public interface IGeneralSaveManager
    {
        public UniTask<ProcedureResult> WriteSave(GeneralSessionSaveData data, CancellationToken cancellationToken);
        public UniTask<RequestResult<GeneralSessionSaveData>> ReadSave(CancellationToken cancellationToken);
        public bool IsReadableSaveData(GeneralSessionSaveData saveData);
    }
}