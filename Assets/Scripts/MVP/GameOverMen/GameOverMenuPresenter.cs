using Assets.Scripts.Managers;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.MVP.GameOverMen
{
    public class GameOverMenuPresenter
    {
        private GameOverMenuView _view;
        public event Action OnMainMenuShow;
        public event Action OnInGameMenuShow;
        public GameOverMenuPresenter(GameOverMenuView view)
        {
            _view = view;
            _view.onRestartClicked += HandleRestart;
            _view.onMainMenuClicked += HandleMainMenu;
            EventManager.GetPoints += UpdateUI;
        }
        private void UpdateUI(int coinCount,float scoreCount)
        {
            _view.UpdateUI(coinCount,scoreCount);
        }
        private void HandleRestart() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _view.Hide();
            OnInGameMenuShow?.Invoke();
        }
        
        private void HandleMainMenu() 
        {
            EventManager.OnReset?.Invoke();
            _view.Hide();
            SceneManager.LoadScene("ClearScene");     
            OnMainMenuShow?.Invoke();
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
