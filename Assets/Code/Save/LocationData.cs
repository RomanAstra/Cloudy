using System;
using System.Collections.Generic;

namespace Cloudy.SaveData
{
    [Serializable]
    public sealed class LocationData
    {
        public string Id;
        public int Stars;
        public List<string> Weapons = new();

        public LocationData(string id)
        {
            Id = id;
        }
    }
}