using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public interface IItemLoader
    {
        public UniTask<RequestResult<Item>> LoadById(string itemId, CancellationToken cancellationToken);
    }
}