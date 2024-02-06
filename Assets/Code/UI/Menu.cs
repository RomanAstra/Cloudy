using Cloudy.UI.Screens;
using Extensions;
using UnityEngine;

namespace Cloudy.UI
{
    public class Menu : MonoBehaviour
    {
        private void Start()
        {
            ScreenManager.Show<MainScreen>();
            WeaponUpgradeSystem.Reset();
        }
    }
}