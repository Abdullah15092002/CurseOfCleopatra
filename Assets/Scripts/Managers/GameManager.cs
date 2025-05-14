using Assets.Scripts.MVP;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager:MonoBehaviour
    {
        public static GameManager Instance;
        public bool isDead=false;
        public bool currentlyMove=false;
        public bool isGroundMove = false;
        public bool isGameStart = false;


        private void OnEnable()
        {
            EventManager.OnReset += StartGame;
            
        }
        private void OnDisable()
        {
            EventManager.OnReset -= StartGame;
        }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            ScoreManager.Instance.coinCount = 0;

        }
        private void Update()
        {
            if (!isGameStart) return;
            EventManager.OnScoreChanged?.Invoke(ScoreManager.Instance.scoreCount);
        }
        public void GameOver()
        {
            
            Time.timeScale = 0f;
            isDead = true;
            isGroundMove =false;
            currentlyMove = false;
            ScoreManager.Instance.SaveScore();//
            EventManager.GetPoints?.Invoke(ScoreManager.Instance.coinCount, ScoreManager.Instance.scoreCount);
            UIManager.Instance.OpenGameOverMenu();
        }
        public void ResetGame()
        {
            PowerUpManager.Instance.GameOverRegulations();
            ResetSpeed();
            ScoreManager.Instance.coinCount = 0;
            ScoreManager.Instance.scoreCount = 0;
            ScoreManager.Instance.isHighScoreShow = false;
            isDead = false;
            isGameStart = false;
            isGroundMove=false;
            currentlyMove = false;
            Time.timeScale = 1f;
            EventManager.OnGameRestarted?.Invoke();
        }
        public void StartGame()
        {
            ResetSpeed();
            isDead = false;
            isGameStart = true;
            isGroundMove = true;
            currentlyMove = true;
            Time.timeScale = 1f;
        }       
        public void PauseGame()
        {
            if (isGameStart)
            {
                Time.timeScale = 0f;
                isGroundMove = false;
                currentlyMove = false;
            }
         
        }
        public void ResumeGame()
        {
            if (isGameStart)
            {
                Time.timeScale = 1f;
                isGroundMove = true;
                currentlyMove = true;
            }
          
        }
        
        public void ResetSpeed()
        {
            LevelGenerator.baseSpeed = 12f;
        }
    }
}
