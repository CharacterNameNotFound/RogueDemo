using System.Linq;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Tools.Editor.ItemPrototypeTools
{
    public class ItemAddressableRegisterer : EditorWindow
    {
        private const string Prefix = "item/";
        
        private string _pathToRead;

        [MenuItem("Game/Item prototyping/Item Addressable Registration")]
        public static void Open()
        {
            ItemAddressableRegisterer window = GetWindow<ItemAddressableRegisterer>();

            window.titleContent = new GUIContent("Item Addressable Registerer");
            
            window.minSize = new Vector2(450, 100);
            window.maxSize = new Vector2(450, 100);
        }

        private void OnGUI()
        {
            _pathToRead = EditorGUILayout.TextField(new GUIContent("Path to Read"), _pathToRead);

            if (GUILayout.Button("Register"))
            {
                EditorUtility.DisplayProgressBar("Registering addressables", "Registering addressables", 0);
                Register();
                EditorUtility.ClearProgressBar();
            }
            
        }

        private void Register()
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
            
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            AddressableAssetGroup libraryGroup = settings.groups.FirstOrDefault(item => item.Name == "ItemRegistry");

            if (libraryGroup is null)
            {
                Debug.LogWarning("No addressable group \"ItemRegistry\" found, new group will be created, reconfigure it, please");
                libraryGroup = settings.CreateGroup("ItemRegistry", false, false, true, settings.DefaultGroup.Schemas);
            }
            
            
            GUID[] assetGUIDS = AssetDatabase.FindAssetGUIDs("t:TextAsset", new[] { _pathToRead });

            foreach (GUID guid in assetGUIDS)
            {
                TextAsset asset = AssetDatabase.LoadAssetByGUID<TextAsset>(guid);
                Item item = JsonConvert.DeserializeObject<Item>(asset.text, jsonSettings);
                string expectedAddress = $"{Prefix}{item.ItemId}";
                AddressableAssetEntry presentEntry = libraryGroup.GetAssetEntry(guid.ToString());

                if (presentEntry is not null && presentEntry.address == expectedAddress)
                {
                    continue;
                }
                
                AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid.ToString(), libraryGroup);
                entry.SetAddress(expectedAddress);
                
                settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
    }
}