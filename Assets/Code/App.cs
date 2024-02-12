using System.Collections.Generic;

namespace Cloudy
{
    public static class App
    {
        public static bool IsInited { get; private set; }
        public static string[] Locations { get; private set; } = new [] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static int CurrentLocation { get; set; } = 1;
        public static int CurrentLevel { get; set; } = 1;
        public static int MaxLevel { get; } = 5;
        public static bool IsMaxLevel => CurrentLevel >= MaxLevel;
        public static string[] Weapons { get; private set; } = new [] { "Pistol", "Shotgun", "SniperRifle", "Through", 
            "Strengthening", "Freezing", "Ricochet" };
        public static List<string> CurrentWeapons { get; private set; } = new () { "Pistol", "Shotgun", "SniperRifle" };
        public static int OpenWeaponIndex { get; set; }
    }
}