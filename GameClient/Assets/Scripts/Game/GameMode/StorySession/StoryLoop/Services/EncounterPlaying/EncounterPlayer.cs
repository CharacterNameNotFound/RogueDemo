using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Battle;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Merchant;
using Game.GameMode.StorySession.StoryLoop.StoryScripts.Configs;
using Utils.UtilityTypes.Result;

namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying
{
    public class EncounterPlayer : IEncounterPlayer
    {
        private IEncounterRegistry _encounterRegistry;
        private IMerchantEncounterPlayer _merchantEncounterPlayer;
        private IBattleEncounterPlayer _battleEncounterPlayer;

        public EncounterPlayer(IEncounterRegistry encounterRegistry, IMerchantEncounterPlayer merchantEncounterPlayer, IBattleEncounterPlayer battleEncounterPlayer)
        {
            _encounterRegistry = encounterRegistry;
            _merchantEncounterPlayer = merchantEncounterPlayer;
            _battleEncounterPlayer = battleEncounterPlayer;
        }

        public async UniTask PlayEncounter(string encounterId, IStoryContext storyContext, CancellationToken cancellationToken)
        {
            RequestResult<Encounter> requestResult = await _encounterRegistry.GetOrLoadById(encounterId, cancellationToken);

            if (requestResult.IsFailure())
            {
                throw requestResult.Exception;
            }

            Encounter encounter = requestResult.GetValue();

            switch (encounter.EncounterType)
            {
                case EncounterType.Merchant:
                    await _merchantEncounterPlayer.Play(encounter, storyContext, cancellationToken);
                    break;
                case EncounterType.Battle:
                    await _battleEncounterPlayer.Play(encounter, storyContext, cancellationToken);
                    break;
                case EncounterType.Story:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
    }
}