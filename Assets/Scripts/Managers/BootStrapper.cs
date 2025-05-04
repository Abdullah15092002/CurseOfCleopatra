using UnityEngine.SceneManagement;
using UnityEngine;


namespace Assets.Scripts.Managers
{
    public class BootStrapper:MonoBehaviour
    {
            [SerializeField] private GameObject UIManagerPrefab;
            [SerializeField] private GameObject GameManagerPrefab;
            [SerializeField] private GameObject ScoreManagerPrefab;

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

        }
            private void Start()
            {
                SceneManager.LoadScene("MenuScene");
            }
    }
}
