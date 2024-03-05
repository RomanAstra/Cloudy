using Cloudy.SaveData;
using YG;

namespace Cloudy
{
    public class YandexGameInstaller
    {
#if YG_PLUGIN_YANDEX_GAME
        private SaveSystem _saveSystem;

        ~YandexGameInstaller()
        {
            _saveSystem.OnSaveData -= OnSaveData;
        }
        
        public void Binding(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            
            YandexGame.GetDataEvent += OnGetData;
            _saveSystem.OnSaveData += OnSaveData;
        }
        
        private void OnSaveData(SaveData.SaveData data)
        {
            YandexGame.savesData.saveData = data;
            YandexGame.SaveProgress();
        }
        private void OnGetData()
        {
            _saveSystem.Load(YandexGame.savesData.saveData);
            YandexGame.GetDataEvent -= OnGetData;
        }
#endif
    }
}