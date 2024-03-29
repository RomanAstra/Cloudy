using Cloudy;
using Cloudy.SaveData;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Helper : MonoBehaviour
    {
        private LocationsData _locationsData;
        private SaveSystem _saveSystem;

        [Inject]
        public void Construct(LocationsData locationsData, SaveSystem saveSystem)
        {
            _locationsData = locationsData;
            _saveSystem = saveSystem;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Keypad1))
                _locationsData.CurrentLevel = 1;
            else if(Input.GetKeyDown(KeyCode.Keypad2))
                _locationsData.CurrentLevel = 2;
            else if(Input.GetKeyDown(KeyCode.Keypad3))
                _locationsData.CurrentLevel = 3;
            else if(Input.GetKeyDown(KeyCode.Keypad4))
                _locationsData.CurrentLevel = 4;
            else if(Input.GetKeyDown(KeyCode.Keypad5))
                _locationsData.CurrentLevel = 5;

            if (Input.GetKeyDown(KeyCode.Y))
            {
                _saveSystem.SaveData = new SaveData();
                _saveSystem.Save();
            }
        }
    }
}