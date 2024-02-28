using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public sealed class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progressBarImage;

        public void Initialize(ILevelProgressModel progressLevelModel)
        {
            _progressBarImage.fillAmount = progressLevelModel.Percent / 100f;
        }
    }
}