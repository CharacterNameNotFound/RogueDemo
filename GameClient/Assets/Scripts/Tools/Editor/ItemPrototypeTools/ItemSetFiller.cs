using Game.GameMode.StorySession.Data.Items;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.ItemPrototypeTools
{
    public class ItemSetFiller : EditorWindow
    {
        private ItemSet _itemSet;
        private const string Prefix = "item";
        private bool _clearBeforeAppending;
        
        private string _pathToRead;

        [MenuItem("Game/Item prototyping/Item Set Filler")]
        public static void Open()
        {
            ItemSetFiller window = GetWindow<ItemSetFiller>();

            window.titleContent = new GUIContent("Item Set Filler");
            
            window.minSize = new Vector2(450, 100);
            window.maxSize = new Vector2(450, 100);
        }

        private void OnGUI()
        {
            _itemSet = (ItemSet) EditorGUILayout.ObjectField(_itemSet, typeof(ItemSet), false);
            _pathToRead = EditorGUILayout.TextField(new GUIContent("Path to Read"), _pathToRead);
            _clearBeforeAppending = EditorGUILayout.Toggle("Clear before appending", _clearBeforeAppending);
            
            if (GUILayout.Button("Assign"))
            {
                EditorUtility.DisplayProgressBar("Assigning items", "Assigning items", 0);
                Register();
                EditorUtility.ClearProgressBar();
            }
            
        }

        private void Register()
        {
            if (_itemSet is null)
            {
                Debug.LogWarning("Item set is not assigned");
                return;
            }
            
            _itemSet.TextAssets.Clear();
            
            GUID[] assetGUIDS = AssetDatabase.FindAssetGUIDs("t:TextAsset", new[] { _pathToRead });

            foreach (GUID guid in assetGUIDS)
            {
                TextAsset asset = AssetDatabase.LoadAssetByGUID<TextAsset>(guid);
                
                _itemSet.TextAssets.Add(asset);
            }
            
            EditorUtility.SetDirty(_itemSet);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }
        
    }
}