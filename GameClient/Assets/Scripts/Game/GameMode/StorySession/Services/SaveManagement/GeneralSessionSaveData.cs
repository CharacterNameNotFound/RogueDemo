using System;

namespace Game.GameMode.StorySession.Services.SaveManagement
{
    [Serializable]
    public class GeneralSessionSaveData
    {
        public string ScenarioId;
        public string CharacterId;
        public string SaveVersion;

        public GeneralSessionSaveData(string scenarioId, string characterId, string saveVersion)
        {
            ScenarioId = scenarioId;
            SaveVersion = saveVersion;
            CharacterId = characterId;
        }
    }
}