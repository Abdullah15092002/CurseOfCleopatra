using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.MainMenu.HighScoreMenu
{
    public  class HighScoreView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScore;
        [SerializeField] private TextMeshProUGUI lastScore;
        [SerializeField] private Button backButton;
        public event Action OnBackClicked;
        private void Start()
        {
            backButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }
        public void UpdateUIText(int highScore,int lastScore)
        {
            this.highScore.text = highScore.ToString();
            this.lastScore.text = lastScore.ToString(); 
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
