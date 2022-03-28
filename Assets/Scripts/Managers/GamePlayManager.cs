using System;
using System.Collections.Generic;
using Bullets;
using Player_Logic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private Button exit;

        public Vector3 spawnPosition;

        [SerializeField] private GameObject listLevels;

        [SerializeField] private GameData gameData;
        public static GamePlayManager Instance { get; set; }
        public int countTargets;

        public bool isPaused;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button nextLevelButton, backLevelButton;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            GameManager.Instance.LoadDataAnimation();
            FirstLevel();
            exit.onClick.AddListener(Exit);
            nextLevelButton.onClick.AddListener(NextLevel);
            backLevelButton.onClick.AddListener(BackLevel);
        }

        private void Update()
        {
            if (countTargets > gameData.targetsCount[gameData.level])
            {
                LevelComplete();
            }

            if (!isPaused) Cursor.lockState = CursorLockMode.Locked;

            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            Pause(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private void FirstLevel()
        {
            var level = Instantiate(gameData.scenes[gameData.level = PlayerPrefs.GetInt("Level")], spawnPosition,
                Quaternion.identity);
            level.transform.parent = listLevels.transform;
            PlayerPrefs.SetInt("Level", gameData.level);
        }

        private void NextLevel()
        {
            gameData.level = Mathf.Clamp(gameData.level, 0, gameData.scenes.Length);
            if (gameData.level < gameData.scenes.Length - 1) gameData.level++;
            var levels = Instantiate(gameData.scenes[gameData.level], spawnPosition, Quaternion.identity);
            levels.transform.parent = listLevels.transform;
            if (listLevels.transform.childCount > 0)
                DestroyImmediate(listLevels.transform.GetChild(0).gameObject);
            PlayerPrefs.SetInt("Level", gameData.level);
            Pause(false);
        }

        private void BackLevel()
        {
            gameData.level = Mathf.Clamp(gameData.level, 0, gameData.scenes.Length);
            if (gameData.level > 0) gameData.level--;
            var levels = Instantiate(gameData.scenes[gameData.level], spawnPosition, Quaternion.identity);
            levels.transform.parent = listLevels.transform;
            if (listLevels.transform.childCount > 0)
                DestroyImmediate(listLevels.transform.GetChild(0).gameObject);
            PlayerPrefs.SetInt("Level", gameData.level);
            Pause(false);
        }

        private void LevelComplete()
        {
            NextLevel();
        }

        private void Pause(bool active)
        {
            if (!active) return;
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }

        private void Exit()
        {
            SceneManager.LoadScene(0);
            Pause(false);
        }
    }
}