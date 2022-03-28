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
        #region Variables

        [SerializeField] private Button exit;
        [SerializeField] private GameObject listLevels;
        [SerializeField] private GameData gameData;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button nextLevelButton, backLevelButton;


        public int countTargets;
        public bool isPaused;

        public Vector3 spawnPosition;

        #endregion

        #region Singleton

        public static GamePlayManager Instance { get; set; }

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

        #endregion

        #region Methods

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            GameManager.Instance.LoadDataAnimation(); //Load menu animation
            FirstLevel();
            exit.onClick.AddListener(Exit); //Back to menu button 
            nextLevelButton.onClick.AddListener(NextLevel); //Next level button
            backLevelButton.onClick.AddListener(BackLevel); //Back level button
        }

        private void Update()
        {
            if (countTargets >
                gameData.targetsCount
                    [gameData.level]) //If the cube counter is greater than the preset number of cubes to be beaten per level is higher
            {
                LevelComplete();
            }

            if (!isPaused) Cursor.lockState = CursorLockMode.Locked;

            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            Pause(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private void FirstLevel() //First level the game will go to depending on the last level played. 
        {
            var level = Instantiate(gameData.scenes[gameData.level = PlayerPrefs.GetInt("Level")], spawnPosition,
                Quaternion.identity);
            level.transform.parent = listLevels.transform;
            PlayerPrefs.SetInt("Level", gameData.level);
        }

        private void NextLevel()
        {
            Pause(false);
            gameData.level = Mathf.Clamp(gameData.level, 0, gameData.scenes.Length);
            if (gameData.level < gameData.scenes.Length - 1) gameData.level++;
            var levels = Instantiate(gameData.scenes[gameData.level], spawnPosition, Quaternion.identity);
            levels.transform.parent = listLevels.transform;
            if (listLevels.transform.childCount > 0)
                DestroyImmediate(listLevels.transform.GetChild(0).gameObject);
            PlayerPrefs.SetInt("Level", gameData.level);
        }

        private void BackLevel()
        {
            Pause(false);
            gameData.level = Mathf.Clamp(gameData.level, 0, gameData.scenes.Length);
            if (gameData.level > 0) gameData.level--;
            var levels = Instantiate(gameData.scenes[gameData.level], spawnPosition, Quaternion.identity);
            levels.transform.parent = listLevels.transform;
            if (listLevels.transform.childCount > 0)
                DestroyImmediate(listLevels.transform.GetChild(0).gameObject);
            PlayerPrefs.SetInt("Level", gameData.level);
        }

        private void LevelComplete() //When you finish all the cubes in the scene, it will go to the next level.
        {
            NextLevel();
        }

        private void Pause(bool active)
        {
            if (active)
            {
                isPaused = !isPaused;
                Time.timeScale = isPaused ? 0 : 1;
                pauseMenu.SetActive(isPaused);
            }
        }

        private void Exit() //Go to the home menu
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0); // Hardcore mode
        }

        #endregion
    }
}