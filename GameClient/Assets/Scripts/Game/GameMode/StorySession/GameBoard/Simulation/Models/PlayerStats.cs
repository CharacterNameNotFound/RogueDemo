using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Models
{
    [Serializable]
    public class PlayerStats
    {
        public int Karma;
        public int Coins;
        public int Experience;
        public int ExperienceRequired;

        public PlayerStats(int karma, int coins, int experience, int experienceRequired)
        {
            Karma = karma;
            Coins = coins;
            Experience = experience;
            ExperienceRequired = experienceRequired;
        }

        public PlayerStats(PlayerStats stats)
        {
            Karma = stats.Karma;
            Coins = stats.Coins;
            Experience = stats.Experience;
            ExperienceRequired = stats.ExperienceRequired;
        }
        
    }
}