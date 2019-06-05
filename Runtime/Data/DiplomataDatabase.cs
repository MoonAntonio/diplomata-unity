using System.IO;
using LavaLeak.Diplomata.Helpers;

namespace LavaLeak.Diplomata
{
    [Resources("Diplomata/DiplomataData")]
    public class DiplomataDatabase : Resource<DiplomataDatabase>
    {
        [ReadOnly]
        public DiplomataData data;

        public static void CreateFile()
        {
#if UNITY_EDITOR
            var content = CreateInstance<DiplomataDatabase>();
            Directory.CreateDirectory(PathHelper.DIPLOMATA);
            Directory.CreateDirectory(PathHelper.LOCALE);
            UnityEditor.AssetDatabase.CreateAsset(content, Path.Combine(PathHelper.DIPLOMATA, "DiplomataData.asset"));
#endif
        }
    }
}
