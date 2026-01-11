using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Session;
using GameWideSystems.SessionManagement.Sessions;
using Utils.DiscInteraction;
using Utils.UtilityTypes.Result;

namespace Game.GameSaveSystem
{
    public class BlobWriter
    {
        private const string BlobFileName = "blob";
        
        private GenericPathProvider _pathProvider;
        private SessionHolder _sessionHolder;
        
        public BlobWriter(GenericPathProvider pathProvider, SessionHolder sessionHolder)
        {
            _pathProvider = pathProvider;
            _sessionHolder = sessionHolder;
        }

        public UniTask<ProcedureResult> Write(SaveBlob saveBlob, CancellationToken cancellationToken)
        {
            string path = Path.Combine(_pathProvider.InProfileSavesPath(_sessionHolder.Session.InternalId), BlobFileName);
            return DiscWriting.ConvertAndWriteJson(saveBlob, path, cancellationToken);
        }
        
    }
}