using System.Collections.Generic;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.Utilities
{
    public interface IDeck<T>
    {
        public void AppendToActive(List<T> items);
        public void Shuffle(IRNGProvider rngProvider);
    }
}