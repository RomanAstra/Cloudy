using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class BulletsInstaller
    {
        public void Binding(DiContainer container, Transform bulletsParent)
        {
            container.Bind<BulletPoolFactory>().AsSingle().WithArguments(bulletsParent);
        }
    }
}