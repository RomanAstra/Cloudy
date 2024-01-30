using System;

namespace Cloudy
{
    public interface IFireInput
    {
        public event Action OnFired;
    }
}