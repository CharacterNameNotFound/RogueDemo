using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using ModestTree;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Heroes.StatusEffects
{
    public static class HeroStatusEffectUtils
    {
        private const int BaseIndex = (int) StatSet.StatSetComponent.BaseValue;
        private const int PersistantIndex = (int) StatSet.StatSetComponent.PersistantBonus;
        
        
        public static bool IsPreservable(StatSet statSet)
        {
            return statSet.Stats[BaseIndex] > 0 || statSet.Stats[PersistantIndex] > 0;
        }

        public static void ClearUnpreservable(StatSet statSet)
        {
            for (int i = PersistantIndex + 1;  i < statSet.Stats.Length;  i++)
            {
                statSet.Stats[i] = 0;
            }
            
            return;
        }

        public static bool IsClearable(StatSet statSet)
        {
            for (int i = 0; i < statSet.Stats.Length; i++)
            {
                if (statSet.Stats[i] == 0)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public static float GetIntensity(StatSet statSet)
        {
            float result = 0;

            for (int i = 0; i < statSet.Stats.Length; i++)
            {
                result += statSet.Stats[i];
            }

            return result;
        }
    }
}