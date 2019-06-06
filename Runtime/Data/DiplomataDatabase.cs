using System.IO;
using LavaLeak.Diplomata.Helpers;
using UnityEngine;

namespace LavaLeak.Diplomata
{
    public class DiplomataDatabase : ScriptableObject
    {
        private static readonly string PATH = Path.Combine(PathHelper.DIPLOMATA, "DiplomataData");
        public DiplomataData Data { get; } = new DiplomataData();

        public static DiplomataDatabase Instance
        {
            get
            {
                var instance = Resources.Load<DiplomataDatabase>(PATH);
                return instance == null ? CreateFile() : instance;
            }
        }

        private static DiplomataDatabase CreateFile()
        {
#if UNITY_EDITOR
            var content = CreateInstance<DiplomataDatabase>();
            Directory.CreateDirectory(PathHelper.DIPLOMATA);
            Directory.CreateDirectory(PathHelper.LOCALE);
            UnityEditor.AssetDatabase.CreateAsset(content, $"{PATH}.asset");
#endif
            return Resources.Load<DiplomataDatabase>(PATH);
        }
    }
}
