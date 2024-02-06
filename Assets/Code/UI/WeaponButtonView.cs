using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cloudy.UI
{
    public sealed class WeaponButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Set(string text, Action callBack)
        {
            _text.text = text;
            _button.onClick.AddListener(() => callBack?.Invoke());
        }
    }
}