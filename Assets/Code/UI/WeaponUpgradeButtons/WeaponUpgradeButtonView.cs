using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class WeaponUpgradeButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
        
        public void Initialize(IWeaponUpgradeButtonModel model)
        {
            _text.text = model.Description;
            _button.onClick.AddListener(model.OnUpgradeButtonClick);
        }
    }
}