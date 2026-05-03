using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters.Battles;
using GameWideSystems.RNGManagement;
using Utils.UtilityTypes.AssetReferencing;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services
{
    public class BaseStoryBossSelector
    {
        private IRNGManager _rngManager;
        private IEncounterRegistry _encounterRegistry;

        public BaseStoryBossSelector(
            IRNGManager rngManager, 
            IEncounterRegistry encounterRegistry)
        {
            _rngManager = rngManager;
            _encounterRegistry = encounterRegistry;
        }

        public async UniTask SelectBosses(BaseStoryConfigs baseStoryConfigs, BaseStoryContext baseStoryContext, CancellationToken cancellationToken)
        {
            IRNGProvider rngGenerator = _rngManager.GetRandomProvider(RNGGroup.Default);

            baseStoryContext.Bosses = new BattleEncounter[3];

            int firstBossKey = rngGenerator.Range(0, baseStoryConfigs.FirstBossEncounters.Count);
            int secondBossKey = rngGenerator.Range(0, baseStoryConfigs.SecondBossEncounters.Count);
            int thirdBossKey = rngGenerator.Range(0, baseStoryConfigs.ThirdBossEncounters.Count);

            BattleEncounter firstBoss = await baseStoryConfigs.FirstBossEncounters[firstBossKey].Load<BattleEncounter>(cancellationToken);
            BattleEncounter secondBoss = await baseStoryConfigs.SecondBossEncounters[secondBossKey].Load<BattleEncounter>(cancellationToken);
            BattleEncounter thirdBoss = await baseStoryConfigs.ThirdBossEncounters[thirdBossKey].Load<BattleEncounter>(cancellationToken);

            baseStoryContext.Bosses[0] = firstBoss;
            baseStoryContext.Bosses[1] = secondBoss;
            baseStoryContext.Bosses[2] = thirdBoss;

            _encounterRegistry.Append(firstBoss);
            _encounterRegistry.Append(secondBoss);
            _encounterRegistry.Append(thirdBoss);
        }
        
    }
}