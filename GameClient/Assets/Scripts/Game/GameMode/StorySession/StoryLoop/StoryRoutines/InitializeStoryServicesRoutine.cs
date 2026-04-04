using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Services.ItemContainers;
using Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization;

namespace Game.GameMode.StorySession.StoryLoop.StoryRoutines
{
    public class InitializeStoryServicesRoutine
    {
        private IItemContainersManager _containersManager;


        public InitializeStoryServicesRoutine(IItemContainersManager containersManager)
        {
            _containersManager = containersManager;
        }

        public async UniTask InitializePools(CancellationToken cancellationToken)
        {
            await _containersManager.Initialize(cancellationToken);

        }
        
        public void CleanUp(CancellationToken cancellationToken)
        {
            _containersManager.CleanUp();
        }
        
        
    }
}