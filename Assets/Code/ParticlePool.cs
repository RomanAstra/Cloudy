using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Code
{
    public sealed class ParticlePool : IGamePause, IGameResume
    {
        private readonly Pool<ParticleSystem> _pool;
        private readonly HashSet<ParticleSystem> _particles = new();
        private readonly Vector3 _scale;

        public ParticlePool(ParticleSystem particleSystem)
        {
            _pool = new Pool<ParticleSystem>(particleSystem);
            _scale = particleSystem.transform.localScale;
        }
        
        
        public async UniTaskVoid Spawn(Vector3 position, float  scaleRatio)
        {
            var particle = _pool.Get(position, Quaternion.identity);
            particle.transform.localScale = _scale * scaleRatio;
            _particles.Add(particle);
            particle.Play(true);
            
            await UniTask.WaitUntil(() => particle.isStopped, PlayerLoopTiming.Update,
                particle.GetCancellationTokenOnDestroy());
            
            Release(particle);
        }

        private void Release(ParticleSystem particle)
        {
            _pool.Release(particle);
            _particles.Remove(particle);
        }
        public void OnPause()
        {
            foreach (var particle in _particles)
            {
                particle.Pause(true);
            }
        }
        public void OnResume()
        {
            foreach (var particle in _particles)
            {
                particle.Play(true);
            }
        }
    }
}