using UnityEngine;
using Utils;

namespace Cloudy
{
    public sealed class PlayerAdapter : IGameStart, IGameFinish, 
        IGamePause, IGameResume
    {
        private readonly PlayerHierarchy _hierarchy;
        private readonly Camera _camera;
        private readonly IMousePositionsInput _mousePositionsInput;
        private readonly WeaponController _weaponController;
        private readonly Transform _weaponContainer;
        private float _velocity;

        private PlayerAdapter(PlayerHierarchy hierarchy, IMousePositionsInput mousePositionsInput, WeaponController weaponController)
        {
            _hierarchy = hierarchy;
            _camera = Camera.main;
            _mousePositionsInput = mousePositionsInput;
            _weaponController = weaponController;
            _weaponContainer = _hierarchy.WeaponContainer;
        }

        private void LookAt(Vector2 point)
        {
            _weaponContainer.eulerAngles = Rotate(point);
        }

        void IGameStart.OnStart()
        {
            _mousePositionsInput.OnRotated += LookAt;
        }
        void IGameFinish.OnFinish()
        {
            _mousePositionsInput.OnRotated -= LookAt;
        }
        void IGamePause.OnPause()
        {
            _mousePositionsInput.OnRotated -= LookAt;
        }
        void IGameResume.OnResume()
        {
            _mousePositionsInput.OnRotated += LookAt;
        }

        private Vector3 Rotate(Vector2 point)
        {
            var difference = _camera.ScreenToWorldPoint(point) - _weaponContainer.position;
            var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var euler = _weaponContainer.eulerAngles;
            var speed = _weaponController.CurrentWeaponAdapter.RotationSpeed;

            euler.z = Mathf.SmoothDampAngle(euler.z, angle, ref _velocity, Time.deltaTime, speed);
            return euler;
        }
    }
}