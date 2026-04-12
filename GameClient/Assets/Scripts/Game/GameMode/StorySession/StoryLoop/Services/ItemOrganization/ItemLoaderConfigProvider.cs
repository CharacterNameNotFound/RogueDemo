using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.ItemOrganization
{
    public class ItemLoaderConfigProvider : ScriptableObject, IItemLoaderConfigProvider
    {
        [field: SerializeField] public string ItemPrefix { get; private set; }
    }
}