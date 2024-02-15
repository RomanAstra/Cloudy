using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class WeaponsInstaller
    {
        public void Binding(DiContainer container, Transform parent,  string[] weapons)
        {
            container.Bind<WeaponFactory>().AsCached().WithArguments(parent);

            container.Bind<DataWeaponsProvider>().AsSingle().WithArguments(weapons).NonLazy();
            container.BindInterfacesAndSelfTo<WeaponsProvider>().AsSingle();
            container.BindInterfacesTo<WeaponController>().AsSingle();
        }
    }
}