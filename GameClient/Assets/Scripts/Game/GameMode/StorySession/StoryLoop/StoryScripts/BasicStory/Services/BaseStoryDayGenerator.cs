using System;
using System.Collections.Generic;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterOrganization;
using Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.StoryLoop.StoryScripts.BasicStory.Services
{
    public class BaseStoryDayGenerator
    {
        private const int EncountersPerChoice = 3;
        
        private EncounterDeckOrganizer _encounterDeckOrganizer;
        private IRNGManager _rngManager;

        public BaseStoryDayGenerator(
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
        
        
        
    }
}