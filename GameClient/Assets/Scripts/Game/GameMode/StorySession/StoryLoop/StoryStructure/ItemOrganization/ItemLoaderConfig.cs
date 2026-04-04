using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.StoryStructure.ItemOrganization
{
    public class ItemLoaderConfig : ScriptableObject, IItemLoaderConfigProvider
    {
        [field: SerializeField] public string ItemPrefix { get; private set; }
    }
}