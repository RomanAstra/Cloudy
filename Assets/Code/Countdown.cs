using System;
using UnityEngine;

namespace Cloudy
{
    [Serializable]
    public sealed class Countdown
    {
        public float Duration = -1;
        public float CurrentTime;

        public bool IsEnded => CurrentTime <= 0;
        public bool IsInfinitely => Duration < 0;

        public Countdown() { }
        public Countdown(float duration, float currentTime = 0)
        {
            Duration = duration;
            CurrentTime = currentTime;
        }

        public void Update()
        {
            CurrentTime -= Time.deltaTime;
        }
        public void Reset()
        {
            CurrentTime = Duration;
        }
    }
}