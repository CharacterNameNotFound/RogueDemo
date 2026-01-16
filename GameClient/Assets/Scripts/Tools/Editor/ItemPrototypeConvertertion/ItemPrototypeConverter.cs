using System.Collections.Generic;
using System.IO;
using Game.GameMode.StorySession.GameBoard.Prototyping.Items.Abstract;
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
            _pathToWrite = EditorGUILayout.TextField(new GUIContent("Path to Write"), _pathToWrite);
            _pathToRead = EditorGUILayout.TextField(new GUIContent("Path to read"), _pathToRead);

            if (GUILayout.Button("Convert"))
            {
                Convert();
            }
            
        }

        private void Convert()
        {
            List<GameObject> foundPrefabs = new List<GameObject>();
            
            string[] prefabGUIDS = AssetDatabase.FindAssets("t:Prefab", new[] { _pathToRead });
            
            foreach (string item in prefabGUIDS)
            {
                string path = AssetDatabase.GUIDToAssetPath(item);
            
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
                if (prefab != null && prefab.CompareTag("ItemPrototype"))
                {
                    foundPrefabs.Add(prefab);
                }
            }

            if (foundPrefabs.Count == 0)
            {
                return;
            }
            
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            foreach (GameObject prefab in foundPrefabs)
            {
                Item item = new Item();
                
                ItemComponentPrototypeComponent[] itemComponents = prefab.GetComponents<ItemComponentPrototypeComponent>();

                foreach (ItemComponentPrototypeComponent itemComponent in itemComponents)
                {
                    itemComponent.WriteToItem(item);
                }

                string json = JsonConvert.SerializeObject(item, settings);
                
                File.WriteAllText(Path.Combine(_pathToWrite, prefab.name + ".json"),json);

                var a = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(item, settings));

            }
            
            
        }
        

    }
}