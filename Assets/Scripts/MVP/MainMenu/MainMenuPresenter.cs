

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MVP.MainMenu
{
    public class MainMenuPresenter
    {
        private MainMenuView _view;

        public event Action OnSettingsMenuShow;
        public event Action OnInGamePanelShow;
        public event Action OnHighScoreShow;
        public MainMenuPresenter(MainMenuView view)
        {
            _view = view;
            _view.OnStartClicked += HandleStart;
            _view.OnSettingsClicked += HandleSettings;
            _view.OnHighScoreClicked += HandleHighScore;
            _view.OnQuitClicked += HandleQuit;
        }

        private void HandleStart() 
        {
            _view.Hide();
            SceneManager.LoadScene("SampleScene");
            OnInGamePanelShow?.Invoke();
            
        }
        private void HandleSettings() 
        {
            _view.Hide();
            OnSettingsMenuShow?.Invoke();
        }
        private void HandleHighScore() 
        {
            _view.Hide();
            OnHighScoreShow?.Invoke();

        }
        private void HandleQuit() 
        {
            Application.Quit();
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
