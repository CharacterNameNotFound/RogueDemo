using System;
using System.Collections.Generic;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemLineSaveLoad
{
    [Serializable]
    public class ItemLineSaveData
    {
        public List<(int index, string itemID)> PlayerLine;
        public List<(int index, string itemID)> StashLine;

        public ItemLineSaveData(
            List<(int index, string itemID)> playerLine, 
            List<(int index, string itemID)> stashLine)
        {
            PlayerLine = playerLine;
            StashLine = stashLine;
        }
    }
}