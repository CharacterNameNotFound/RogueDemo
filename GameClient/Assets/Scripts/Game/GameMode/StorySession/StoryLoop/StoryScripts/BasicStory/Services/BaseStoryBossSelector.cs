using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameMode.StorySession.GameBoard.Simulation.Encounters;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
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

            baseStoryContext.Bosses = new string[3];

            int firstBossKey = rngGenerator.Range(0, baseStoryConfigs.FirstBossEncounters.Count);
            int secondBossKey = rngGenerator.Range(0, baseStoryConfigs.SecondBossEncounters.Count);
            int thirdBossKey = rngGenerator.Range(0, baseStoryConfigs.ThirdBossEncounters.Count);

            Encounter firstBoss = await baseStoryConfigs.FirstBossEncounters[firstBossKey].Load<Encounter>(cancellationToken);
            Encounter secondBoss = await baseStoryConfigs.FirstBossEncounters[secondBossKey].Load<Encounter>(cancellationToken);
            Encounter thirdBoss = await baseStoryConfigs.FirstBossEncounters[thirdBossKey].Load<Encounter>(cancellationToken);

            baseStoryContext.Bosses[0] = firstBoss.EncounterId;
            baseStoryContext.Bosses[1] = secondBoss.EncounterId;
            baseStoryContext.Bosses[2] = thirdBoss.EncounterId;

            _encounterRegistry.Append(firstBoss);
            _encounterRegistry.Append(secondBoss);
            _encounterRegistry.Append(thirdBoss);
        }
        
    }
}