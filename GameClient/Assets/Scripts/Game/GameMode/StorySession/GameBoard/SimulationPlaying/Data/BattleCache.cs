namespace Game.GameMode.StorySession.GameBoard.SimulationPlaying.Data
{
    public class BattleCache
    {
        public BattleSideCache[] BattleSideCache;
        
        public BattleCache(BattleSideCache player, BattleSideCache encounter)
        {
            BattleSideCache = new BattleSideCache[2];

            BattleSideCache[(int)OwnerIndex.Player] = player;
            BattleSideCache[(int)OwnerIndex.Encounter] = encounter;
        }
        
        
        public BattleSideCache GetPlayer()
        {
            return Get(OwnerIndex.Player);
        }
        
        public BattleSideCache GetEncounter()
        {
            return Get(OwnerIndex.Encounter);
        }
        
        public BattleSideCache Get(OwnerIndex index)
        {
            return BattleSideCache[(int) index];
        }

        public BattleSideCache Get(int index)
        {
            return BattleSideCache[index];
        }
        
    }
}