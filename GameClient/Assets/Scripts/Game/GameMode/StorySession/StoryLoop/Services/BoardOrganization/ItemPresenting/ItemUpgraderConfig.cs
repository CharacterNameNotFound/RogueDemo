using UnityEngine;

namespace Game.GameMode.StorySession.StoryLoop.Services.BoardOrganization.ItemPresenting
{
    public class ItemUpgraderConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 UpgradedItemPosition { get; private set; }
        [field: SerializeField] public Vector3 TargetItemPosition { get; private set; }
        
        [field: SerializeField] public Vector3 PreUpgradeMovementRotation { get; private set; }
        [field: SerializeField] public float IncreasedItemSize { get; private set; }
        
        [field: SerializeField] public float PreUpgradeMovementTime { get; private set; }
        [field: SerializeField] public float ItemClashTime { get; private set; }
        [field: SerializeField] public float HalfRotationTime { get; private set; }
        [field: SerializeField] public float ItemReturnTime { get; private set; }
        
        
        
    }
}