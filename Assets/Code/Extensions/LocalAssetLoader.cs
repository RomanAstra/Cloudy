using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Extensions
{
    public abstract class LocalAssetLoader
    {
        private readonly HashSet<GameObject> _cachedObjects = new ();
        private readonly HashSet<ScriptableObject> _cachedScriptableObjects = new ();

        protected async UniTask<T> InstantiateAsync<T>(string assetId, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetId, parent);
            var obj = await handle.Task;

            return GetComponent<T>(obj);
        }
        protected async UniTask<T> LoadGameObjectAsync<T>(string assetId)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            var obj = await handle.Task;

            return GetComponent<T>(obj);
        }
        protected async UniTask<ScriptableObject> LoadScriptableObjectAsync(string assetId)
        {
            var handle = Addressables.LoadAssetAsync<ScriptableObject>(assetId);
            var obj = await handle.Task;

            _cachedScriptableObjects.Add(obj);
            return obj;
        }
        
        protected async UniTask<Disposable<T>> InstantiateAsyncDisposable<T>(string assetId, Transform parent = null)
        {
            var component = await InstantiateAsync<T>(assetId, parent);
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

            foreach (var scriptableObject in _cachedScriptableObjects)
            {
                Addressables.Release(scriptableObject);
            }
            _cachedScriptableObjects.Clear();
        }

        private T GetComponent<T>(GameObject obj)
        {
            _cachedObjects.Add(obj);
            
            if(!obj.TryGetComponent(out T component))
                throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                                 "attempt to load it from addressables");
            
            return component;
        }
    }
}