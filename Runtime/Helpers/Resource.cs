using System;
using System.IO;
using UnityEngine;

namespace LavaLeak.Diplomata
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourcesAttribute : Attribute
    {
        public readonly string Path;

        public ResourcesAttribute(string path)
        {
            Path = path;
        }
    }

    public class Resource<T> : ScriptableObject where T : Resource<T>
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var type = typeof(T);
                    var attributes = Attribute.GetCustomAttributes(type);

                    foreach (var attribute in attributes)
                    {
                        if (attribute.GetType() == typeof(ResourcesAttribute))
                        {
                            var resourcesAttribute = (ResourcesAttribute) attribute;
                            instance = Resources.Load<T>(resourcesAttribute.Path);
                            break;
                        }
                    }
                }

                if (instance == null)
                {
                    Debug.LogError($"Can't load the resource \"{typeof(T)}\".");
                }

                return instance;
            }
        }
    }
}