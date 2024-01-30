using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace Cloudy
{
    [UsedImplicitly]
    public sealed class InputSystemManager : IFireInput, IMousePositionsInput, 
        IChangeWeaponInput, IInitializer, IUpdate
    {
        public event Action OnFired = delegate { };
        public event Action OnChanged = delegate { };
        public event Action<Vector2> OnRotated = delegate { };

        private Controls _controls;

        public void OnStart()
        {
            _controls = new Controls();

            _controls.Enable();
        }
        public void OnUpdate(float deltaTime)
        {
            if (_controls.Main.Fire.inProgress)
                OnFired();

            if (_controls.Main.NextWeapon.triggered)
                OnChanged();

            var point = _controls.Main.LookAt.ReadValue<Vector2>();
            OnRotated(point);
        }
    }
}