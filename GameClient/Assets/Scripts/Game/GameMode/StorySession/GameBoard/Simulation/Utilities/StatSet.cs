using System;

namespace Game.GameMode.StorySession.GameBoard.Simulation.Utilities
{
    [Serializable]
    public class StatSet
    {
        private const int StatCount = 4;
        
        public float[] Stats;

        public StatSet()
        {
            Stats = new float[StatCount];
        }

        public StatSet(StatSet statSet)
        {
            Stats = new float[StatCount];
            
            for (int i = 0; i < StatCount; i++)
            {
                Stats[i] = statSet.Stats[i];
            }
        }

        public StatSet(float[] statSet)
        {
            Stats = new float[StatCount];

            for (int i = 0; i < StatCount; i++)
            {
                Stats[i] = statSet[i];
            }
        }
        
        public StatSet(float baseValue, float consumableBonus = 0, float auraBonus = 0, float combatBonus = 0)
        {
            Stats = new float[StatCount];
            
            Stats[0] = baseValue;
            Stats[1] = consumableBonus;
            Stats[2] = auraBonus;
            Stats[3] = combatBonus;
        }

        public StatSet GetCopy()
        {
            return new StatSet(this);
        }
    }
}