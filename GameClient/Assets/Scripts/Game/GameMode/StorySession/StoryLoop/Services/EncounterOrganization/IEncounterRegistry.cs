using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization
{
    public interface IEncounterRegistry
    {
        public void Initialize(List<Encounter> encounters);
        public UniTask InitializeWithIds(IEnumerable<string> ids, CancellationToken cancellationToken);
        public bool TryGetById(string id, out Encounter item);
        public UniTask<RequestResult<Encounter>> GetOrLoadById(string id, CancellationToken cancellationToken);
        public UniTask<RequestResult<bool>> AppendEncounterById(string id, CancellationToken cancellationToken);
        public UniTask AppendEncountersById(IEnumerable<string> ids, CancellationToken cancellationToken);
        public bool Append(Encounter encounter);
        public void AppendRange(IEnumerable<Encounter> encounters);
        public List<string> GetAllRegisteredIds();
        public void CleanUp();

    }
}