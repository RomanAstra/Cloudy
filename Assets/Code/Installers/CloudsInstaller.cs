using Cloudy.Pools;
using Code;
using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class CloudsInstaller
    {
        public void Binding(DiContainer container, AudioSource audioSource, ParticleSystem particleSystem)
        {
            container.BindInterfacesAndSelfTo<ParticlePool>().AsSingle().WithArguments(particleSystem);;
            container.Bind<CloudPoolFactory>().AsSingle().WithArguments(audioSource);
        }
    }
}