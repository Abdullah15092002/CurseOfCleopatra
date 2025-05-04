using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.GameOverMen
{
    public class GameOverMenuView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinValueText;
        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;

        public event Action onRestartClicked;
        public event Action onMainMenuClicked;

        private void Start()
        {
            restartButton.onClick.AddListener(() => onRestartClicked.Invoke());
            mainMenuButton.onClick.AddListener(() => onMainMenuClicked?.Invoke());

        }
        public void UpdateUI(int coinCount,float scoreCount)
        {
            
            coinValueText.text = coinCount.ToString();
            scoreValueText.text = scoreCount.ToString();
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
