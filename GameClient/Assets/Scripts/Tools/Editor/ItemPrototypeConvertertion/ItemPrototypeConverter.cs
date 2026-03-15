using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Special;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.StatRegisterers;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Structure;
using Game.GameMode.StorySession.GameBoard.Simulation.Items;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.ItemPrototypeConvertertion
{
    public class ItemPrototypeConverter : EditorWindow
    {
        private string _pathToWrite;
        private string _pathToRead;

        [MenuItem("Game/Item prototyping/Item prototype converter")]
        public static void Open()
        {
            ItemPrototypeConverter window = GetWindow<ItemPrototypeConverter>();

            window.titleContent = new GUIContent("Item prototype converter");
            
            window.minSize = new Vector2(450, 100);
            window.maxSize = new Vector2(450, 100);
        }

        private void OnGUI()
        {
            _pathToRead = EditorGUILayout.TextField(new GUIContent("Path to Read"), _pathToRead);
            _pathToWrite = EditorGUILayout.TextField(new GUIContent("Path to Write"), _pathToWrite);

            if (GUILayout.Button("Convert"))
            {
                Convert();
            }
            
        }

        private void Convert()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
            
            string[] prefabGUIDS = AssetDatabase.FindAssets("t:Prefab", new[] { _pathToRead });
            
            foreach (string item in prefabGUIDS)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
            
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
                if (prefab != null && prefab.CompareTag($"ItemPrototype"))
                {
                    string savePath = _pathToWrite + path.Substring(_pathToRead.Length);
                    ProcessPrefab(prefab, savePath, settings);
                }
            }
            
            AssetDatabase.Refresh();
        }

        private void ProcessPrefab(GameObject prefab, string path, JsonSerializerSettings settings)
        {
            Item item = new Item();

            TriggerPrototype[] itemComponents = prefab.GetComponents<TriggerPrototype>();

            foreach (TriggerPrototype itemComponent in itemComponents)
            {
                item.Triggers.Add(itemComponent.GetTrigger());
            }
            
            if (prefab.TryGetComponent(out ItemCore itemCore))
            {
                item.ItemID = itemCore.ItemId;
                item.ItemName = itemCore.ItemName;
                item.ItemRarity = itemCore.ItemRarity;
                item.UpgradedItemId = itemCore.UpgradedItem?.ItemId;
                item.DowngradedItemId = itemCore.DowngradedItem?.ItemId;
                item.ItemSpriteRuntimeKey = itemCore.ItemImage.RuntimeKey;
                item.ItemSize = itemCore.ItemSize;
            }
            
            if (prefab.TryGetComponent(out UniversalStatRegisterer stats))
            {
                stats.AppendStats(item.ItemStats);
            }
                
            if (prefab.TryGetComponent(out TagListPrototype tagList))
            {
                item.Tags = tagList.GetTagList();
            }

            string json = JsonConvert.SerializeObject(item, settings);

            path = path.Substring(0, path.LastIndexOf('/'));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(Path.Combine(path, prefab.name + ".json"),json);
            
            
        }
        
        

    }
}