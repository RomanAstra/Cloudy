using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class WeaponsInstaller
    {
        public void Binding(DiContainer container, Transform parent)
        {
            container.Bind<WeaponFactory>().AsCached().WithArguments(parent);
            
            container.BindInterfacesAndSelfTo<WeaponsProvider>().AsSingle();
            container.BindInterfacesAndSelfTo<WeaponController>().AsSingle();
        }
    }
}