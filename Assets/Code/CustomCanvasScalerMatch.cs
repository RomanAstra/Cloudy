using UnityEngine;
using UnityEngine.UI;

namespace VG.Utilites
{
    [RequireComponent(typeof(Canvas), typeof(CanvasScaler))]
    public class CustomCanvasScalerMatch : MonoBehaviour
    {
        [SerializeField]
        private int _standartWidth = 1920;
        [SerializeField]
        private int _standartHeight = 1080;
        [SerializeField]
        [Range(0, 1)]
        private float _lessStandartMatch = 0;
        [SerializeField]
        [Range(0, 1)]
        private float _overStandartMatch = 1;

        // Awake is called when the script instance is being loaded
        void Awake()
        {
            var canvasCamera = GetComponent<Canvas>().worldCamera;
            var aspect = canvasCamera != null ? canvasCamera.aspect : Camera.main.aspect;
            var diff = aspect/((float)_standartWidth/_standartHeight);
            if (diff > 1.01f)
                GetComponent<CanvasScaler>().matchWidthOrHeight = _overStandartMatch;
            else if (diff < 0.99f)
                GetComponent<CanvasScaler>().matchWidthOrHeight = _lessStandartMatch;
        }
    }
}