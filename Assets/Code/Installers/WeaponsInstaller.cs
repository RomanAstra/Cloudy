using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class WeaponsInstaller
    {
        public void Binding(DiContainer container, Transform parent,  string[] weapons)
        {
            container.Bind<WeaponFactory>().AsCached().WithArguments(parent);

            container.Bind<DataWeaponsProvider>().AsSingle().WithArguments(weapons);
            container.BindInterfacesAndSelfTo<WeaponsProvider>().AsSingle();
            container.BindInterfacesAndSelfTo<WeaponController>().AsSingle();
        }
    }
}