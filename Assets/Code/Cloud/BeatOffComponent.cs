using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Cloudy
{
    public sealed class BeatOffComponent : MonoBehaviour
    {
        [Header("Shield")]
        [SerializeField] private float _shieldDelay;

        private CancellationToken _cancellationToken;
        
        private void OnCollisionEnter(Collision other)
        {
            HideShield();
        }

        private void ShowShield()
        {
            gameObject.SetActive(true);
        }
        private async UniTaskVoid HideShield()
        {
            gameObject.SetActive(false);
            await Observable.Timer(TimeSpan.FromSeconds(_shieldDelay));
            ShowShield();
        }
    }
}