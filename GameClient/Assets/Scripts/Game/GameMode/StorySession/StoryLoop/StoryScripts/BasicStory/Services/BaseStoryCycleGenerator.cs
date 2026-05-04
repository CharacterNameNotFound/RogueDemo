using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services
{
    public class BaseStoryCycleGenerator
    {
        private const int EncountersPerChoice = 3;
        
        private EncounterDeckOrganizer _encounterDeckOrganizer;
        private IRNGManager _rngManager;

        public BaseStoryCycleGenerator(
            EncounterDeckOrganizer encounterDeckOrganizer, 
            IRNGManager rngManager
            )
        {
            _encounterDeckOrganizer = encounterDeckOrganizer;
            _rngManager = rngManager;
        }
        
        
        public void GenerateFirstDayEntry(BaseStoryContext baseStoryContext, BaseStoryConfigs configs)
        {
            baseStoryContext.StoryEncounters = new List<List<string>>();

            List<EncounterType> eligibleEncounterTypes = new List<EncounterType>() { EncounterType.Merchant };
            
            for (int i = 0; i < configs.StoryDayLength; i++)
            {
                baseStoryContext.StoryEncounters.Add(GetEncounterSet(eligibleEncounterTypes));
            }
            
        }


        public void AppendDay(BaseStoryContext baseStoryContext, BaseStoryConfigs configs, GameBoardModel gameBoardModel)
        {
            List<EncounterType> eligibleEncounterTypes = new List<EncounterType>() { EncounterType.Merchant };
            
            for (int i = 0; i < configs.StoryDayLength - 1; i++)
            {
                baseStoryContext.StoryEncounters.Add(GetEncounterSet(eligibleEncounterTypes));
            }

            if (IsBossCycle(gameBoardModel.StoryStats.Cycle))
            {
                int bossIndex = (gameBoardModel.StoryStats.Cycle - 1) / 4;
                baseStoryContext.StoryEncounters.Add(new List<string> {baseStoryContext.Bosses[bossIndex].EncounterId});
            }
            else
            {
                baseStoryContext.StoryEncounters.Add(GetEncounterSet(eligibleEncounterTypes));
            }
        }
        

        private List<string> GetEncounterSet(List<EncounterType> eligibleEncounterTypes)
        {
            List<string> encounters = new List<string>(EncountersPerChoice);

            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.Default);

            for (int j = 0; j < EncountersPerChoice; j++)
            {
                int encounterTypeIndex = randomProvider.Range(0, eligibleEncounterTypes.Count);

                EncounterType selectedType = eligibleEncounterTypes[encounterTypeIndex];
                
                bool encounterFound = _encounterDeckOrganizer.Draw(selectedType, true, out string encounter);

                if (!encounterFound)
                {
                    throw new Exception("Encounter deck is empty, which is unexpected");
                }
                    
                encounters.Add(encounter);
            }
            
            return encounters;
        }

        private bool IsBossCycle(int cycle)
        {
            return cycle == 1 || cycle == 5 || cycle == 9;
        }
        
        
    }
}