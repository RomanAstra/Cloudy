using System;
using System.Collections.Generic;
using Cloudy;
using Ui;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public sealed class WeaponsMenuViewModel : IWeaponsMenuViewModel
    {
        private const int WEAPONS_COUNT = 3;
        
        private readonly WeaponsDataProvider _dataWeaponsProvider;
        private readonly LocationsData _locationsData;

        public string Title => "Оружие";
        public IReadOnlyList<string> Weapons { get;  private set; }

        public event Action<IViewModel> OnClosed;

        public WeaponsMenuViewModel(WeaponsDataProvider dataWeaponsProvider, LocationsData locationsData)
        {
            _dataWeaponsProvider = dataWeaponsProvider;
            _locationsData = locationsData;
        }

        public void Close()
        {
            OnClosed?.Invoke(this);
        }
        public void RollWeapons()
        {
            Weapons = _dataWeaponsProvider.GetRandomWeapons(WEAPONS_COUNT);
            
        }
        public void LoadScene()
        {
            SceneManager.LoadScene(_locationsData.CurrentLocation);
        }
    }
}