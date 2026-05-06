using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad
{
    public interface IItemLineLoader
    {
        public UniTask Load(ItemLineSaveData playerSaveData, CancellationToken cancellationToken);
    }
}