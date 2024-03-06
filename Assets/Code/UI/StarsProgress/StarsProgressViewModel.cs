using System;
using System.Collections.Generic;
using Cloudy;
using Cloudy.SaveData;
using Ui;
using LocationsData = Cloudy.LocationsData;

namespace Code.UI
{
    public sealed class StarsProgressViewModel : IStarsProgressViewModel
    {
        public float Progress { get; }
        public string CurrentStarsCount { get; }
        public int AllStarsCount { get; }
        public IReadOnlyList<OpenObjectStarsData> Weapons { get; }

        public event Action<IViewModel> OnClosed;

        public StarsProgressViewModel(LocationsData locationsData, OpenObjectStarsConfig openObjectStarsConfig, 
            WeaponsDataProvider weaponsDataProvider, SaveSystem saveSystem)
        {
            var currentStarsCount = saveSystem.SaveData.GetStarsCount();
            CurrentStarsCount = currentStarsCount.ToString();
            
            var allStarsCount = locationsData.MaxLevelCount * locationsData.MaxStarsCountPerLevel * 
                                openObjectStarsConfig.Objects.Count;
            
            AllStarsCount = allStarsCount;
            
            Progress = currentStarsCount / (float)allStarsCount;
            
            Weapons = weaponsDataProvider.GetWeapons();
        }
        
        public void Close()
        {
            OnClosed?.Invoke(this);
        }
    }
}