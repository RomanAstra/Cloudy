using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Cloudy
{
    public sealed class BeatOffComponent : MonoBehaviour
    {
        [SerializeField] private float _shieldDelay;

        private readonly CancellationTokenSource _cancellationToken = new ();
        
        private void OnCollisionEnter(Collision other)
        {
             HideShield().Forget();
        }

        private void ShowShield()
        {
            gameObject.SetActive(true);
        }
        private async UniTaskVoid HideShield()
        {
            gameObject.SetActive(false);
            await Observable.Timer(TimeSpan.FromSeconds(_shieldDelay));
            if(!_cancellationToken.IsCancellationRequested)
                ShowShield();
        }
        private void OnDestroy()
        {
            _cancellationToken.Cancel();
        }
    }
}