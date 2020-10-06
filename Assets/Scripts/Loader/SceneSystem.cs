﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loader
{
    public enum Map
    {
        Sky = 1,
        Vulcan = 2,
        Ocean = 3
    }

    public enum Scene
    {
        Loader = 0,
        Menu = 4,
        Game = 5
    }

    public class SceneSystem : MonoBehaviour
    {
        public static SceneSystem instance;

        public Map map;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void LoadGame()
        {
            LoadScene(Scene.Game);
        }

        public void LoadMenu()
        {
            LoadScene(Scene.Menu);
        }

        private void LoadScene(Scene scene)
        {
            SceneManager.LoadScene((int) scene);
            StartCoroutine(LoadMap());
        }

        private IEnumerator LoadMap()
        {
            var mapLoading = SceneManager.LoadSceneAsync((int) map, LoadSceneMode.Additive);
            mapLoading.allowSceneActivation = false;
            mapLoading.completed += operation =>
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int) map));
            while (!mapLoading.isDone)
            {
                if (mapLoading.progress >= 0.9f)
                    mapLoading.allowSceneActivation = true;

                yield return null;
            }
        }
    }
}