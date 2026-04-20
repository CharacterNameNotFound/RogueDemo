using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Utils.UtilityTypes.AssetReferencing;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public class EncounterLoader : IEncounterLoader
    {
        private IEncounterLoaderConfigProvider _encounterLoaderConfigProvider;

        public EncounterLoader(IEncounterLoaderConfigProvider encounterLoaderConfigProvider)
        {
            _encounterLoaderConfigProvider = encounterLoaderConfigProvider;
        }

        public UniTask<RequestResult<Encounter>> LoadById(string encounterId, CancellationToken cancellationToken)
        {
            string address = $"{_encounterLoaderConfigProvider.EncounterPrefix}{encounterId}";

            return address.LoadRequest<Encounter>(cancellationToken);
        }
    }
}