namespace Cloudy
{
    public class LocationsData
    {
        public const int MAX_LEVEL = 5;

        public int CurrentLocation = 1;
        public int CurrentLevel = 1;
        
        public string[] Locations { get; private set; } = new [] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public bool IsMaxLevel => CurrentLevel >= MAX_LEVEL;

        public string GetLevelName()
        {
            return $"Location{CurrentLocation}Level{CurrentLevel}";
        }
    }
}