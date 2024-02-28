using UnityEngine;

namespace Cloudy
{
    public class OpenWeaponsPrefsController
    {
        private const string OPEN_WEAPONS = "open_weapon";

        public void SaveCount(int value)
        {
            PlayerPrefs.SetInt(OPEN_WEAPONS, value);
        }

        public int GetCount()
        {
            return PlayerPrefs.GetInt(OPEN_WEAPONS, 1);
        }
    }
}