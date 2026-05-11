using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public interface IItemRegistry
    {
        public void Initialize(Dictionary<string, Item> items);
        public UniTask InitializeWithIds(IEnumerable<string> ids, CancellationToken cancellationToken);
        public bool TryGetById(string id, out Item item);
        public UniTask<RequestResult<Item>> GetOrLoadById(string id, CancellationToken cancellationToken);
        public UniTask<RequestResult<bool>> AppendItemById(string id, CancellationToken cancellationToken);
        public UniTask AppendItemsById(IEnumerable<string> ids, CancellationToken cancellationToken);
        public bool AppendItem(Item item);
        public List<string> GetAllRegisteredIds();
        public void CleanUp();
    }
}