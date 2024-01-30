using System;
using UnityEngine;

namespace Cloudy
{
    public interface IMousePositionsInput
    {
        public event Action<Vector2> OnRotated;
    }
}