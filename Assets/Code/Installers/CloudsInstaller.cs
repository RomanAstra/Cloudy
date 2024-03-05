using Cloudy.Pools;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class CloudsInstaller
    {
        public void Binding(DiContainer container, AudioSource audioSource, ParticleSystem particleSystem)
        {
            container.Bind<CloudPoolFactory>().AsSingle().WithArguments(audioSource, particleSystem);
        }
    }
}