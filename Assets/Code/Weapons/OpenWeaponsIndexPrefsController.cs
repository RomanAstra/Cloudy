using UnityEngine;

namespace Cloudy
{
    public class OpenWeaponsIndexPrefsController
    {
        private const string OPEN_WEAPONS_INDEX = "open_weapon_index";

        public void SaveIndex(int value)
        {
            PlayerPrefs.SetInt(OPEN_WEAPONS_INDEX, value);
        }

        public int GetIndex()
        {
            return PlayerPrefs.GetInt(OPEN_WEAPONS_INDEX, 0);
        }
    }
}