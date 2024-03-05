using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class WeaponsInstaller
    {
        public void Binding(DiContainer container, Transform parent, AudioSource audioSource)
        {
            container.Bind<WeaponFactory>().AsCached().WithArguments(parent);
            
            container.BindInterfacesAndSelfTo<WeaponsProvider>().AsSingle();
            container.BindInterfacesAndSelfTo<WeaponController>().AsSingle().WithArguments(audioSource);
        }
    }
}