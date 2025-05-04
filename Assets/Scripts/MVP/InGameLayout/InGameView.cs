using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.InGameLayout
{
    public class InGameView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CoinText;
        [SerializeField] private TextMeshProUGUI ScoreText;
        [SerializeField] private Button openPauseMenuButton;

        public event Action OnPauseMenuButtonClicked;
        public string CoinTextt{get => CoinText.text;set => CoinText.text = value;}

        private void Start()
        {
            openPauseMenuButton.onClick.AddListener(() => OnPauseMenuButtonClicked?.Invoke());
        }
        public void UpdateCoinText(int coinAmount)
        {
            CoinText.text = coinAmount.ToString();
        }
        public void UpdateScoreText(float score) 
        {
            ScoreText.text = score.ToString();
        }
        public void ResetUI()
        {
            ScoreText.text = "0";
            CoinText.text = "0";
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
