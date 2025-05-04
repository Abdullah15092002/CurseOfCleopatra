using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MVP.InGameLayout
{
    public class InGamePresenter
    {
       readonly InGameView _view;
       public event Action OnPauseMenuShow;
        public InGamePresenter(InGameView view)
        {
            _view = view;
            _view.OnPauseMenuButtonClicked += OpenPauseMenu;
            EventManager.OnCoinCollected += UpdateCoinUI;//Coin toplandıkça
            EventManager.OnGameRestarted += ResetUI;//Score ve Coin sıfırlar
            EventManager.OnScoreChanged += UpdateScoreUI;//Score değiştikçe(GameManagerUpdate)
        }      

        private void UpdateCoinUI(int coinValue)
        {
            _view.UpdateCoinText(ScoreManager.Instance.coinCount);
        }
        private void UpdateScoreUI(float score)
        {
            _view.UpdateScoreText(score);
        }
        private void ResetUI()
        {
            _view.ResetUI();
        }
        private void OpenPauseMenu()
        {
            _view.Hide();
            OnPauseMenuShow?.Invoke();
            GameManager.Instance.PauseGame();  
        }
       
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
