using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.PlayerStashEncounter
{
    public interface IPlayerStashController
    {
        public UniTask Initialize(CancellationToken cancellationToken);
        public void CleanUp();


    }
}