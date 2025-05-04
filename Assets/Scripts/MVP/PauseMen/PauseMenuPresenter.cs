using Assets.Scripts.Managers;
using System;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.MVP.PauseMen
{
    public class PauseMenuPresenter
    {
        private PauseMenuView _view;

        public event Action OnMainMenuShow;
        public event Action OnInGameSettingsMenuShow;
        public event Action OnInGamePanelShow;
        public PauseMenuPresenter(PauseMenuView view)
        {
            _view = view;
            _view.onMainMenuClicked += HandleMainMenu;
            _view.onResumeClicked += HandleResume;
            _view.onRestartClicked += HandleRestart;
            _view.onSettingsClicked += HandleSettings;
        }
        private void HandleMainMenu() 
        {
            EventManager.OnReset?.Invoke();
            _view.Hide();
            SceneManager.LoadScene("ClearScene");
            OnMainMenuShow?.Invoke();
        }
        private void HandleResume() 
        {
            _view.Hide();
            OnInGamePanelShow?.Invoke();
            GameManager.Instance.ResumeGame();
        }
        private void HandleRestart() 
        {
            EventManager.OnReset?.Invoke();
            _view.Hide();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            OnInGamePanelShow?.Invoke();

        }
        private void HandleSettings()
        {
            _view.Hide();
            OnInGameSettingsMenuShow?.Invoke();
            GameManager.Instance.PauseGame();
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
