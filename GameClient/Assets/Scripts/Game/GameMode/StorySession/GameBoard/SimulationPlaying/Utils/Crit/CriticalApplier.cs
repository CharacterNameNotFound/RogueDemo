using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Enteties.Special.ItemStatSets;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Utilities;
using Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Utils.Crit
{
    public class CriticalApplier : ICriticalApplier
    {
        private IItemStatGetter _itemStatGetter;
        private IRNGManager _rngManager;
        private ICriticalConfigsProvider _configsProvider;

        public CriticalApplier(IItemStatGetter itemStatGetter, IRNGManager rngManager, ICriticalConfigsProvider configsProvider)
        {
            _itemStatGetter = itemStatGetter;
            _rngManager = rngManager;
            _configsProvider = configsProvider;
        }

        public float TryApply(float value, int index, int owner, BattleCache battleCache, out bool isCrit)
        {
            Item item = CacheShortcuts.GetItem(index, owner, battleCache);

            
            float critChance = _itemStatGetter.GetStatValue(item,
                ItemStatType.CriticalChance);

            if (critChance == 0)
            {
                isCrit = false;
                return value;
            }
            
            // rolling crit
            IRNGProvider randomProvider = _rngManager.GetRandomProvider(RNGGroup.Battle);
            int roll = randomProvider.Range(0, 100);
            
            if (roll > critChance)
            {
                isCrit = false;
                return value;
            }


            float additionalMult = _itemStatGetter.GetStatValue(item, ItemStatType.CriticalPower);

            float mult = _configsProvider.BasicCripMultiplier + additionalMult / 100f;

            isCrit = true;
            return value * mult;
        }
        
        
    }
}