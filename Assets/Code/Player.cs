using UnityEngine;
using Zenject;

namespace Cloudy
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private Transform _weaponContainer;

        private Camera _camera;
        private IMousePositionsInput _mousePositionsInput;
        
        [Inject]
        private void Construct(IMousePositionsInput mousePositionsInput)
        {
            _camera = Camera.main;
            _mousePositionsInput = mousePositionsInput;
        }
        public void OnEnable()
        {
            _mousePositionsInput.OnRotated += LookAt;
            gameObject.SetActive(true);
        }
        public void OnDisable()
        {
            _mousePositionsInput.OnRotated -= LookAt;
            gameObject.SetActive(false);
        }

        private void LookAt(Vector2 point)
        {
            var difference = _camera.ScreenToWorldPoint(point) - transform.position;
            var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            _weaponContainer.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }
    }
}
