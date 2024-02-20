using UnityEngine;
using Utils;
using Zenject;

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
        private readonly int _inaccuracy = 10;

        private PlayerAdapter(PlayerHierarchy hierarchy, IMousePositionsInput mousePositionsInput, WeaponController weaponController)
        {
            _hierarchy = hierarchy;
            _weaponContainer = _hierarchy.WeaponContainer;
            _camera = Camera.main;
            _mousePositionsInput = mousePositionsInput;
            _weaponController = weaponController;
        }

        private void LookAt(Vector2 point)
        {
            _weaponContainer.eulerAngles = Rotate(point);
        }
        
        public void OnStart()
        {
            _mousePositionsInput.OnRotated += LookAt;
        }
        public void OnFinish()
        {
            _mousePositionsInput.OnRotated -= LookAt;
        }
        public void OnPause()
        {
            _mousePositionsInput.OnRotated -= LookAt;
        }
        public void OnResume()
        {
            _mousePositionsInput.OnRotated += LookAt;
        }

        private Vector3 Rotate(Vector2 point)
        {
            var difference = _camera.ScreenToWorldPoint(point) - _weaponContainer.position;
            var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 180;
            var euler = _weaponContainer.eulerAngles;
            var diff = Mathf.DeltaAngle(angle, euler.z);

            var direction = Mathf.Clamp(Mathf.Sign(diff) / _inaccuracy, -1, 1);
            euler.z += direction * _weaponController.CurrentWeaponAdapter.RotationSpeed;
            return euler;
        }
    }
    public sealed class PlayerHierarchy : MonoBehaviour
    {
        public Transform WeaponContainer;
    }
}
