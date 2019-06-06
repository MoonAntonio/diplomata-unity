using System.IO;

namespace LavaLeak.Diplomata.Helpers
{
    public static class PathHelper
    {
        public static readonly string ASSETS = "Assets";
        public static readonly string RESOURCES = Path.Combine(ASSETS, "Resources");
        public static readonly string DIPLOMATA = Path.Combine(RESOURCES, "Diplomata");
        public static readonly string LOCALE = Path.Combine(DIPLOMATA, "Locale");
    }
}