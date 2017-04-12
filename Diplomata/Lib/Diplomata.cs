﻿using System.Collections.Generic;
using UnityEngine;

namespace DiplomataLib {

    [ExecuteInEditMode]
    public class Diplomata : MonoBehaviour {
        public static Diplomata instance = null;
        public static Preferences preferences;
        public static GameProgress gameProgress;
        public static List<Character> characters;

        public void Awake() {
            if (instance == null) {
                instance = this;

                if (Application.isPlaying) {
                    DontDestroyOnLoad(gameObject);
                    instance.hideFlags = HideFlags.None;
                }
            }

            else {
                DestroyImmediate(gameObject);
            }

            Restart();
        }

        public static void Restart() {
            preferences = new Preferences();
            preferences.Start();
            
            characters = new List<Character>();
            Character.UpdateList();
            
            gameProgress = new GameProgress();
            gameProgress.Start();
        }

        public static void Instantiate() {
            if (instance == null && FindObjectsOfType<Diplomata>().Length < 1) {
                GameObject obj = new GameObject("[ Diplomata ]");
                obj.hideFlags = HideFlags.HideInHierarchy;
                obj.AddComponent<Diplomata>();
            }

            Restart();
        }

        private void CheckRepeated() {
            var repeated = FindObjectsOfType<Diplomata>();

            foreach (Diplomata item in repeated) {
                if (!item.Equals(instance)) {
                    DestroyImmediate(item.gameObject);
                }
            }
        }

        public static T[] ListToArray<T>(List<T> list) {
            T[] array = new T[list.Count];

            for (int i = 0; i < list.Count; i++) {
                array[i] = list[i];
            }

            return array;
        }
    }

}