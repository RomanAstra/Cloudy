using System;

namespace Cloudy
{
    public interface IChangeWeaponInput
    {
        public event Action OnChanged;
    }
}