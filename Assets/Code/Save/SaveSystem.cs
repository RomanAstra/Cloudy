using System;

namespace Cloudy.SaveData
{
    public class SaveSystem
    {
        public SaveData SaveData = new();
        
        public event Action<SaveData> OnSaveData;
        public event Action<SaveData> OnLoadData;

        public void Load(SaveData data)
        {
            SaveData = data;
            OnLoadData?.Invoke(SaveData);
        }
        public void Save()
        {
            OnSaveData?.Invoke(SaveData);
        }
    }
}