using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;


namespace Assets.Scripts.Managers
{
    public class BootStrapper:MonoBehaviour
    {
            [SerializeField] private GameObject UIManagerPrefab;
            [SerializeField] private GameObject GameManagerPrefab;
            [SerializeField] private GameObject ScoreManagerPrefab;
            [SerializeField] private GameObject AudioManagerPrefab;

            void Awake()
            {
                if (UIManager.Instance == null)
                {
                    Instantiate(UIManagerPrefab);
                }
                if (GameManager.Instance==null)
                {
                Instantiate(GameManagerPrefab);
                }
               if (ScoreManager.Instance == null)
               {
                Instantiate(ScoreManagerPrefab);
               }
               if (AudioManager.Instance == null)
               {
                  Instantiate(AudioManagerPrefab);
               }

        }
            private void Start()
            {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
