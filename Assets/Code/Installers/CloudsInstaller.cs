using Cloudy.Pools;
using Zenject;

namespace Cloudy
{
    public sealed class CloudsInstaller
    {
        public void Binding(DiContainer container)
        {
            container.Bind<CloudPoolFactory>().AsSingle();
        }
    }
}