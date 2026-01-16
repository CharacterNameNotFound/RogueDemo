using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract
{
    public abstract class ItemComponentPrototypeGenericComponent<T> : ItemComponentPrototypeComponent where T : ItemComponent
    {
        [SerializeField] public T Value;

    }
}