
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager:MonoBehaviour
    {
        
        public static ScoreManager Instance;
        public int coinCount;
        public float scoreCount;
        public bool isHighScoreShow=false;
        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
      
        private void OnEnable()
        {
            EventManager.OnScoreIncreased += UpdateScore;
            EventManager.OnCoinCollected += AddCoin;

        }
        private void OnDisable()
        {
            EventManager.OnCoinCollected -= AddCoin;
            EventManager.OnScoreIncreased -= UpdateScore;
        }
        public void SaveScore()
        {
            PlayerPrefs.SetInt("HighScore", 50);
            int highScoree = PlayerPrefs.GetInt("HighScore");
            int scoreCountInt = Mathf.RoundToInt(scoreCount);
            PlayerPrefs.SetInt("LastScore", scoreCountInt);
            if (scoreCountInt > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", scoreCountInt);
            }

            PlayerPrefs.Save();
            int highScore = PlayerPrefs.GetInt("HighScore");
            int lastScore = PlayerPrefs.GetInt("LastScore");
            EventManager.OnUpdateHighScore.Invoke(highScore,lastScore);
        }
         public void AddCoin(int amount)
        {
            coinCount += amount;
        }
        public void UpdateScore(float delta)
        {
            scoreCount += delta;
            if (!isHighScoreShow && scoreCount>=PlayerPrefs.GetInt("HighScore"))
            {
                isHighScoreShow = true;
                UIManager.Instance.PopUpRoutinePlay();
            }
        }
    }
}
