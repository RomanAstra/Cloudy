using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Cloudy
{
    public class PreloadAssets : MonoBehaviour
    {
        [SerializeField] private SceneContext _context;

        public LevelHierarchy Level;
        public string[] Weapons;
        
        private async void Awake()
        {
            Level = await new LevelProvider().Load(App.CurrentLevel.ToString());
            var config = await Addressables.LoadAssetAsync<WeaponsConfig>(nameof(WeaponsConfig));
            Weapons = config.Weapons;
            
            _context.gameObject.SetActive(true);
        }
    }
}