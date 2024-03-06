namespace Cloudy
{
    public class LocationsData
    {
        public readonly int MaxLevelCount = 5;
        public readonly int MaxStarsCountPerLevel = 3;

        public string CurrentLocation;
        public int CurrentLevel = 1;
        public int CurrentStars;
        
        private readonly (int oneStar, int twoStars, int threeStars) _starsPercent = (50, 70, 90);
        
        public bool IsMaxLevel => CurrentLevel >= MaxLevelCount;

        public string GetCurrentLevelName()
        {
            return $"Location{CurrentLocation}Level{CurrentLevel}";
        }
        public int GetStars(int percent)
        {
            return _starsPercent.threeStars <= percent ? 3 : _starsPercent.twoStars <= percent ? 2 : 1;
        }
        public bool IsWin(int percent)
        {
            return percent >= _starsPercent.oneStar;
        }
    }
}