using System;
using System.Collections.Generic;

namespace Cloudy.SaveData
{
    [Serializable]
    public sealed class SaveData
    {
        private Dictionary<string, LocationData> _completedLocations = new();

        public bool TryGetLocationData(string id, out LocationData data)
        {
            return _completedLocations.TryGetValue(id, out data);
        }
        public LocationData GetLocationData(string id)
        {
            if(_completedLocations.TryGetValue(id, out var locationData))
                return locationData;
            
            locationData = new LocationData(id);
            _completedLocations.Add(id, locationData);

            return locationData;
        }
        public int GetStarsCount()
        {
            var sum = 0;
            
            foreach (var value in _completedLocations.Values)
            {
                sum += value.Stars;
            }
            
            return sum;
        }
    }
}