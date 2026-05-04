using System;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities
{
    [Serializable]
    public class StatSet
    {
        // used for enumeration
        public enum StatSetComponent
        {
            None = -1,
            BaseValue, // base value original to object
            PersistantBonus, // permanent bonuses
            AuraBonus, // semi-permanent bonuses
            CombatBonus, // combat only bonus
            Special, // just in case buffer
        }
        
        private const int StatCount = 5;
        
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
        
        public StatSet(float baseValue, float consumableBonus = 0, float auraBonus = 0, float combatBonus = 0, float special = 0)
        {
            Stats = new float[StatCount];
            
            Stats[0] = baseValue;
            Stats[1] = consumableBonus;
            Stats[2] = auraBonus;
            Stats[3] = combatBonus;
            Stats[4] = special;
        }

        public StatSet GetCopy()
        {
            return new StatSet(this);
        }

        public void CopyUpgradeConsistentStats(StatSet originalItem)
        {
            Stats[(int)StatSetComponent.PersistantBonus] = originalItem.Stats[(int)StatSetComponent.PersistantBonus];
            Stats[(int)StatSetComponent.AuraBonus] = originalItem.Stats[(int)StatSetComponent.AuraBonus];
        }
        
    }
}