using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public interface IEncounterLoader
    {
        public UniTask<RequestResult<Encounter>> LoadById(string encounterId, CancellationToken cancellationToken);
    }
}