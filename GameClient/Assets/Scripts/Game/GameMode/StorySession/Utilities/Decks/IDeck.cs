using System.Collections.Generic;
using GameWideSystems.RNGManagement;

namespace Game.GameMode.StorySession.Utilities.Decks
{
    public interface IDeck<T>
    {
        /// <summary>
        ///  Used by serialization pipeline to provide fallback rng, in case deck need to reshuffle, it may make sense just strip shuffling on fallback as it is not expected behaviour 
        /// </summary>
        public void SetFallbackRNGProvider(IRNGProvider fallBackShuffler);
        public bool Draw(bool isReturnedImmediate, out T result);
        public void Return(T item);
        public void AppendToActive(List<T> items);
        public void ShuffleInAll(IRNGProvider rngProvider);
        public void Shuffle(IRNGProvider rngProvider);
    }
}