using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameMode.StorySession.GameBoard.View.Utils
{
    public class VFXHolderComponent : MonoBehaviour
    {
        private Dictionary<Type, Transform> _vfxRegistry = new();

        public void Register<T>(Transform vfx)
        {
            vfx.SetParent(transform, false);
            vfx.localPosition = Vector3.zero;
            
            _vfxRegistry.Add(typeof(T), vfx);
        }

        public Transform Get<T>()
        {
            return _vfxRegistry[typeof(T)];
        }
        
        public Transform Unregister(Type type)
        {
            _vfxRegistry.Remove(type, out Transform vfx);

            return vfx;
        }
        
        
    }
}