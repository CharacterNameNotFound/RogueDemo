using System.IO;
using UnityEngine;

namespace Game.ManagementSystems.LookUpTableManagement
{
    public class LookUpTableConfigProvider : ScriptableObject, ILookUpTableConfigProvider
    {
        [SerializeField] private string _storageFolderPathSuffix;
        
        public string GetLookUpTablesFolderPath()
        {
            return Path.Combine(Application.persistentDataPath, _storageFolderPathSuffix);
        }

        public string GetLookUpTablePath(LookUpTableGroup group)
        {
            return Path.Combine(Application.persistentDataPath, _storageFolderPathSuffix, $"{group.ToString()}.db");
        }
        
    }
}