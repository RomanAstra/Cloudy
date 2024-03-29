using System.Collections.Generic;
using Cloudy.Adapter;
using UnityEngine;

namespace Cloudy.Pools
{
    public interface IConcreteCloudPool
    {
        IReadOnlyList<CloudHierarchy> Clouds { get; }
        
        void Spawn(Vector3 position);
    }
}