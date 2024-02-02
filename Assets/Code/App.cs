using System.Collections.Generic;

namespace Cloudy
{
    public static class App
    {
        public static void Init()
        {
            if (IsInited)
                return;

            Locations = new HashSet<string> { "1", "2" };
            Weapons = new HashSet<string> { "Pistol", "Shotgun", "SniperRifle" };
        }
        
        public static bool IsInited { get; private set; }
        public static HashSet<string> Locations { get; private set; }
        public static string CurrentLocation { get; set; } = "1";
        public static int CurrentLevel { get; set; } = 1;
        public static int MaxLevel { get; set; } = 6;
        public static bool IsMaxLevel => CurrentLevel >= MaxLevel;
        public static HashSet<string> Weapons { get; private set; }
        public static List<string> CurrentWeapons { get; private set; } = new() { "Pistol", "Shotgun", "SniperRifle" };
    }
}