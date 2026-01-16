namespace Game.GameMode.StorySession.GameBoard.Simulation.Utilities
{
    public class StatusCounter
    {
        public bool IsTrue => _count > 0;

        private int _default = 0;
        private int _count = 0;

        public StatusCounter(int @default)
        {
            _count = @default;
            _default = @default;
        }
        
        public void Update(int count = 1)
        {
            _count += 1;
        }

        public void Reset()
        {
            _count = _default;
        }

    }
}