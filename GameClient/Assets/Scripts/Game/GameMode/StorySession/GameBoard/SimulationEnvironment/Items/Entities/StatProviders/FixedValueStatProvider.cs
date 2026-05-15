using Game.GameMode.StorySession.GameBoard.Services.ItemStatGetting;
using Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.Structure;

namespace Game.GameMode.StorySession.GameBoard.SimulationEnvironment.Items.Entities.StatProviders
{
    public class FixedValueStatProvider : StatProvider
    {
        public float Value;

        public FixedValueStatProvider(float value)
        {
            Value = value;
        }
        
        public override StatProvider GetCopy()
        {
            return new FixedValueStatProvider(Value);
        }

        public override float GetValue(Item item, IItemStatGetter statGetter)
        {
            return Value;
        }
    }
}