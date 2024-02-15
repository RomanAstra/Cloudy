using System.Collections.Generic;

namespace Cloudy
{
    public sealed class DataWeaponsProvider
    {
        public readonly IReadOnlyCollection<string> Weapons;
            
        public DataWeaponsProvider(string[] weapons)
        {
            Weapons = weapons;
        }
    }
}