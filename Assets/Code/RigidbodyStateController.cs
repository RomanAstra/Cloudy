using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class RigidbodyStateController : IGameFinish, 
        IGamePause, IGameResume
    {
        private readonly Rigidbody _rigidbody;
        private Vector2 _cacheVelocity;

        public RigidbodyStateController(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void OnFinish()
        {
            Sleep();
        }
        public void OnPause()
        {
            Sleep();
        }
        public void OnResume()
        { 
            WakeUp();
        }
    
        private void Sleep()
        {
            _cacheVelocity = _rigidbody.velocity;
            _rigidbody.Sleep();
        }
        private void WakeUp()
        {
            _rigidbody.WakeUp();
            _rigidbody.velocity = _cacheVelocity;
        }
    }
}