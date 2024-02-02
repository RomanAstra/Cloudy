using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cloudy.UI
{
    public class EndGamePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultGameText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continurButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _continurButton.onClick.AddListener(ContinueGame);
        }
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _continurButton.onClick.RemoveAllListeners();
        }

        public void Show(bool isWin)
        {
            gameObject.SetActive(true);
            _resultGameText.text = isWin ? "Победа" : "Поражение";
            
            if(!App.IsMaxLevel)
                _continurButton.gameObject.SetActive(isWin);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
        private void ExitGame()
        {
            SceneManager.LoadScene("Menu");
        }
        private void ContinueGame()
        {
            App.CurrentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}