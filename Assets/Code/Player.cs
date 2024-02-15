using UnityEngine;
using Utils;
using Zenject;

namespace Cloudy
{
    public sealed class Player : MonoBehaviour, IGameStart, IGameFinish, 
        IGamePause, IGameResume
    {
        [SerializeField] private Transform _weaponContainer;
        
        private Camera _camera;
        private IMousePositionsInput _mousePositionsInput;
        
        public Transform WeaponContainer => _weaponContainer;

        [Inject]
        private void Construct(IMousePositionsInput mousePositionsInput)
        {
            _camera = Camera.main;
            _mousePositionsInput = mousePositionsInput;
        }

        private void LookAt(Vector2 point)
        {
            var difference = _camera.ScreenToWorldPoint(point) - transform.position;
            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            WeaponContainer.rotation = Quaternion.Euler(0f, 0f, rotateZ);
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
    }
}
