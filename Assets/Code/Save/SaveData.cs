using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Cloudy.SaveData
{
    [Serializable]
    public sealed class SaveData
    {
        public List<LocationData> CompletedLocations = new ();
        
        public bool TryGetLocationData(string id, out LocationData data)
        {
            for (var i = 0; i < CompletedLocations.Count; i++)
            {
                if (CompletedLocations[i].Id == id)
                {
                    data = CompletedLocations[i];
                    return true;
                }
            }

            data = null;
            return false;
        }
        public LocationData GetLocationData(string id)
        {
            for (var i = 0; i < CompletedLocations.Count; i++)
            {
                if (CompletedLocations[i].Id == id)
                    return CompletedLocations[i];
            }
            
            var data = new LocationData(id);
            CompletedLocations.Add(data);
            return data;
        }
        public int GetStarsCount()
        {
            var sum = 0;
            
            foreach (var value in CompletedLocations)
            {
                sum += value.Stars;
            }
            
            return sum;
        }
    }
}