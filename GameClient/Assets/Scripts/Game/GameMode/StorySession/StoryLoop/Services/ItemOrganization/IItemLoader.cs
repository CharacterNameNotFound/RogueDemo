using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Game.GameMode.StorySession.GameBoard.Simulation.Items.Enteties;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public interface IItemLoader
    {
        public UniTask<RequestResult<Item>> LoadById(string itemId, CancellationToken cancellationToken);
    }
}