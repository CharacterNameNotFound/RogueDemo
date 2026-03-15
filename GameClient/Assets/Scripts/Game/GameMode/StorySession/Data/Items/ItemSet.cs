using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.StorySession.Data.Items
{
    public class ItemSet : ScriptableObject
    {
        public string ItemSetId;
        public List<TextAsset> TextAssets;
    }
}