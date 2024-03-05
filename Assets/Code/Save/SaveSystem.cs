﻿using System;

namespace Cloudy.SaveData
{
    public class SaveSystem
    {
        public SaveData SaveData = new();
        
        public event Action<SaveData> OnSaveData;

        public void Load(SaveData data)
        {
            SaveData = data;
        }
        public void Save()
        {
            OnSaveData?.Invoke(SaveData);
        }
    }
}