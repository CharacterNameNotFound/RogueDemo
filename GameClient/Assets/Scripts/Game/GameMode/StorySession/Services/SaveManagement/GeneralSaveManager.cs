using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Session;
using GameWideSystems.SessionManagement.Sessions;
using Newtonsoft.Json;
using UnityEngine;
using Utils.DiscInteraction;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.Services.SaveManagement
{
    public class GeneralSaveManager : IGeneralSaveManager
    {
        private GenericPathProvider GenericPathProvider;
        private SessionHolder SessionHolder;
        private JsonSerializerSettings JsonSerializerSettings;

        public GeneralSaveManager(
            GenericPathProvider genericPathProvider, 
            SessionHolder sessionHolder, 
            JsonSerializerSettings jsonSerializerSettings)
        {
            GenericPathProvider = genericPathProvider;
            SessionHolder = sessionHolder;
            JsonSerializerSettings = jsonSerializerSettings;
        }


        public async UniTask<ProcedureResult> WriteSave(GeneralSessionSaveData data, CancellationToken cancellationToken)
        {
            string savePath = GenericPathProvider.InProfileSavesPath(SessionHolder.Session.InternalId);
            CleanSaveFolder(savePath);
            
            // ToDo make specializedService??
            string path = Path.Combine(savePath, nameof(GeneralSessionSaveData));
            ProcedureResult result = await DiscWriting.ConvertAndWriteJson(data, path, JsonSerializerSettings, cancellationToken);

            return result;
        }

        public UniTask<RequestResult<GeneralSessionSaveData>> ReadSave(CancellationToken cancellationToken)
        {
            string savePath = GenericPathProvider.InProfileSavesPath(SessionHolder.Session.InternalId);
            string path = Path.Combine(savePath, nameof(GeneralSessionSaveData));

            return DiscReading.ReadAndConvertJson<GeneralSessionSaveData>(path, JsonSerializerSettings, cancellationToken);
        }

        public bool IsReadableSaveData(GeneralSessionSaveData saveData)
        {
            return saveData.SaveVersion.Equals(Application.version);
        }
        
        private void CleanSaveFolder(string savePath)
        {
            foreach (string file in Directory.GetFiles(savePath))
            {
                File.Delete(file);
            }
        }
        
    }
}