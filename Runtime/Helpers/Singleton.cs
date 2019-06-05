using System;
using System.Collections.Generic;
using UnityEngine;

namespace LavaLeak.Diplomata
{
    /// <summary>
    /// Attribute to set singleton basic properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
        public readonly string Name;
        public readonly bool DontDestroyOnLoad;

        public SingletonAttribute(bool dontDestroyOnLoad = false, string name = "GameObject")
        {
            Name = name;
            DontDestroyOnLoad = dontDestroyOnLoad;
        }
    }

    /// <summary>
    /// Base class to define any singleton MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">The type of the Singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// The unique item in the singleton Dictionary.
        /// </summary>
        private class SingletonItem
        {
            public T Instance = null;
            public string Name;
            public bool DontDestroyOnLoad;
        }

        /// <summary>
        /// Where the singletons references are in.
        /// </summary>
        private static Dictionary<Type, SingletonItem> items = new Dictionary<Type, SingletonItem>();

        /// <summary>
        /// Public property with the singleton reference.
        /// </summary>
        public static T Instance => GetInstance();

        private void Awake()
        {
            GetInstance();
        }

        /// <summary>
        /// Method to get the singleton reference.
        /// </summary>
        /// <returns>The mono behaviour.</returns>
        /// <exception cref="Exception">Throw a error if can't find the singleton type.</exception>
        private static T GetInstance()
        {
            if (!items.ContainsKey(typeof(T)))
            {
                items.Add(typeof(T), new SingletonItem());
            }

            var item = items[typeof(T)];

            // Get existent singleton.
            if (item.Instance == null)
            {
                var type = typeof(T);
                var attributes = Attribute.GetCustomAttributes(type);

                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == typeof(SingletonAttribute))
                    {
                        var singletonAttribute = (SingletonAttribute) attribute;
                        item.Name = singletonAttribute.Name;
                        item.DontDestroyOnLoad = singletonAttribute.DontDestroyOnLoad;
                        break;
                    }
                }

                item.Instance = (T) FindObjectOfType(typeof(T));

                if (item.DontDestroyOnLoad && item.Instance != null)
                {
                    DontDestroyOnLoad(item.Instance.gameObject);
                }
            }

            // Create a singleton.
            if (item.Instance == null)
            {
                var go = new GameObject(item.Name);
                item.Instance = go.AddComponent<T>();

                if (item.DontDestroyOnLoad && item.Instance != null)
                {
                    DontDestroyOnLoad(item.Instance.gameObject);
                }
            }

            // Throw a error.
            if (item.Instance == null)
            {
                throw new Exception($"Can't add or load the component \"{typeof(T)}\".");
            }

            return item.Instance;
        }

        /// <summary>
        /// Destroy the Game Object and set instance to null.
        /// </summary>
        public void Destroy()
        {
            DestroyImmediate(Instance.gameObject);

            if (items.ContainsKey(typeof(T)))
            {
                items[typeof(T)] = null;
            }
        }
    }
}