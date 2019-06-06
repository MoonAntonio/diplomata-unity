using System.Collections.Generic;
using System.IO;
using LavaLeak.Diplomata.Helpers;
using LavaLeak.Diplomata.Models;
using UnityEngine;

namespace LavaLeak.Diplomata.New
{
    public class DiplomataData : ScriptableObject
    {
        private const string FILE_NAME = "DiplomataData";
        private static readonly string RESOURCE_PATH = Path.Combine("Diplomata", FILE_NAME);
        private static readonly string FILE_PATH = Path.Combine(PathHelper.DIPLOMATA, $"{FILE_NAME}.asset");

        public List<Context> contexts;

        public static DiplomataData Instance
        {
            get
            {
                var instance = Resources.Load<DiplomataData>(RESOURCE_PATH);
                return instance == null ? CreateFile() : instance;
            }
        }

        private static DiplomataData CreateFile()
        {
#if UNITY_EDITOR
            var content = CreateInstance<DiplomataData>();
            Directory.CreateDirectory(PathHelper.DIPLOMATA);
            Directory.CreateDirectory(PathHelper.LOCALE);
            UnityEditor.AssetDatabase.CreateAsset(content, FILE_PATH);
#endif
            return Resources.Load<DiplomataData>(RESOURCE_PATH);
        }
    }
}
