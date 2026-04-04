using System.Threading;
using Cysharp.Threading.Tasks;
using SQLite;
using Utils.UtilityTypes.Result;

namespace Game.ManagementSystems.LookUpTableManagement
{
    public interface ILookUpTableManager
    {
        public UniTask<ProcedureResult> LoadLookUpTables(CancellationToken cancellationToken);
        public RequestResult<SQLiteAsyncConnection> GetAsyncLookUpDB(LookUpTableGroup tableGroup, CancellationToken cancellationToken);
        public RequestResult<SQLiteConnection> GetLookUpDB(LookUpTableGroup tableGroup, CancellationToken cancellationToken);
        
    }
}