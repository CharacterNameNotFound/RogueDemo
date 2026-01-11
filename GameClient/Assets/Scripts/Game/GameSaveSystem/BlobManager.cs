using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Session;
using Utils.UtilityTypes.Result;

namespace Game.GameSaveSystem
{
    public class BlobManager : IBlobManager
    {
        private BlobReader _blobReader;
        private BlobWriter _blobWriter;
        private GenericPathProvider _genericPathProvider;

        private SaveBlob _saveBlob;
        
        public BlobManager(BlobReader blobReader, BlobWriter blobWriter, GenericPathProvider genericPathProvider)
        {
            _blobReader = blobReader;
            _blobWriter = blobWriter;
            _genericPathProvider = genericPathProvider;
        }

        public SaveBlob Blob => _saveBlob;

        public UniTask<ProcedureResult> CreateNew(CancellationToken cancellationToken)
        {
            _saveBlob = new SaveBlob();
            
            return ProcedureResultBuilder.Success().AsUniTask();
        }

        public UniTask<ProcedureResult> TryLoadBlob(CancellationToken cancellationToken)
        {
            return ProcedureResultBuilder.Success().AsUniTask();
        }
        
        public UniTask<ProcedureResult> ReadBlob(CancellationToken cancellationToken)
        {
            return ProcedureResultBuilder.Success().AsUniTask();
        }

        public UniTask<ProcedureResult> WriteBlob(CancellationToken cancellationToken)
        {
            return _blobWriter.Write(_saveBlob, cancellationToken);
        }
        
    }
}