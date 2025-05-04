using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MVP.MainMenu.HighScoreMenu
{
    public class HighScorePresenter
    {
        private HighScoreView _view;

        public event Action OnMenuShow;
        public HighScorePresenter(HighScoreView view)
        {
            _view = view;
            _view.OnBackClicked += BackToMenu;
            EventManager.OnUpdateHighScore += UpdateUI;
        }
        private void UpdateUI(int highScore,int lastscore)
        {
            _view.UpdateUIText(highScore, lastscore);   
        }
        private void BackToMenu()
        {
            _view.Hide();
            OnMenuShow?.Invoke();
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
