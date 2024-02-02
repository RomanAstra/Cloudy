using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Extensions
{
    public class LocalAssetLoader
    {
        private readonly HashSet<GameObject> _cachedObjects = new ();
        
        public async UniTask<T> Load<T>(string assetId, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetId, parent);
            var obj = await handle.Task;

            _cachedObjects.Add(obj);
            if(obj.TryGetComponent(out T component) == false)
                throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                                 "attempt to load it from addressables");
            
            return component;
        }
        
        public async UniTask<Disposable<T>> LoadDisposable<T>(string assetId, Transform parent = null)
        {
            var component = await Load<T>(assetId, parent);
            return Disposable<T>.Borrow(component, _ => Unload());
        }

        public void Unload()
        {
            foreach (var gameObject in _cachedObjects)
            {
                gameObject.SetActive(false);
                Addressables.ReleaseInstance(gameObject);
            }
            
            _cachedObjects.Clear();
        }
    }
}