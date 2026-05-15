namespace Game.GameMode.StorySession.StoryLoop.Services.EncounterPlaying.Encounters
{
    public enum EncounterType
    {
        /// <summary>
        /// trader sells items
        /// </summary>
        Merchant,
        
        /// <summary>
        /// Mostly one time, sometimes chained events
        /// </summary>
        Story,
        
        /// <summary>
        /// Fight enemy, includes bosses
        /// </summary>
        Battle,
        
        /// <summary>
        /// Provides free stuff
        /// </summary>
        Gift, 
    }
}