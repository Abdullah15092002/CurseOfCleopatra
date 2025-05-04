using Assets.Scripts.MVP.GameOverMen;
using Assets.Scripts.MVP.HighScorepPopUp;
using Assets.Scripts.MVP.InGameLayout;
using Assets.Scripts.MVP.InGameSettingsMenu;
using Assets.Scripts.MVP.MainMenu;
using Assets.Scripts.MVP.MainMenu.HighScoreMenu;
using Assets.Scripts.MVP.MainMenu.SettingsMenu;
using Assets.Scripts.MVP.PauseMen;

namespace Assets.Scripts.Managers 
{
    public class UIFlowManager
    {
        private MainMenuPresenter _mainMenuPresenter;
        private SettingsMenuPresenter _settingsMenuPresenter;
        private PauseMenuPresenter _pauseMenuPresenter;
        private GameOverMenuPresenter _gameOverMenuPresenter;
        private InGamePresenter _inGamePresenter;
        private InGameSettingsMenuPresenter _inGameSettingsMenuPresenter;
        private HighScorePresenter _highScorePresenter;
        private HighScorePopUpPresenter _highScorePopUpPresenter;
        public UIFlowManager(
            MainMenuView mainMenuView,
            SettingsMenuView settingsMenuView,
            PauseMenuView pauseMenuView,
            GameOverMenuView gameOverMenuView,
            InGameView inGameView,
            InGameSettingsMenuView inGameSettingsMenuView,
            HighScoreView highScoreView,
            HighScorePopUpView highScorePopUpView)
        {
            _mainMenuPresenter = new MainMenuPresenter(mainMenuView);
            _settingsMenuPresenter = new SettingsMenuPresenter(settingsMenuView);
            _pauseMenuPresenter = new PauseMenuPresenter(pauseMenuView);
            _gameOverMenuPresenter= new GameOverMenuPresenter(gameOverMenuView);
            _inGamePresenter= new InGamePresenter(inGameView);
            _inGameSettingsMenuPresenter = new InGameSettingsMenuPresenter(inGameSettingsMenuView);
            _highScorePresenter= new HighScorePresenter(highScoreView);
            _highScorePopUpPresenter= new HighScorePopUpPresenter(highScorePopUpView);

            _pauseMenuPresenter.OnMainMenuShow += ShowMainMenu;
            _gameOverMenuPresenter.OnMainMenuShow += ShowMainMenu;
            _settingsMenuPresenter.OnMainMenuShow += ShowMainMenu;       
            _mainMenuPresenter.OnSettingsMenuShow += ShowSettingsMenu;
            _inGamePresenter.OnPauseMenuShow += ShowPauseMenu;
            _inGameSettingsMenuPresenter.OnPauseMenuShow += ShowPauseMenu;
            _pauseMenuPresenter.OnInGameSettingsMenuShow += ShowInGameSettingsMenu;
            _mainMenuPresenter.OnHighScoreShow += ShowHighScore;
            _highScorePresenter.OnMenuShow += ShowMainMenu;
            _mainMenuPresenter.OnInGamePanelShow += ShowInGamePanel;
            _pauseMenuPresenter.OnInGamePanelShow += ShowInGamePanel;
            _gameOverMenuPresenter.OnInGameMenuShow += ShowInGamePanel;
            UIManager.Instance.OnOpenGameOverMenu += HandleOpenGameOverMenu;
            UIManager.Instance.OnShowPopUp += ShowPopUp;
            UIManager.Instance.OnHidePopUp += HidePopUp;
            StartGameMenuLayout();
        }

        public void ShowMainMenu() => _mainMenuPresenter.Show();
        public void ShowSettingsMenu() => _settingsMenuPresenter.Show();
        public void ShowPauseMenu() => _pauseMenuPresenter.Show();
        public void ShowInGamePanel()=>_inGamePresenter.Show();
        public void ShowInGameSettingsMenu()=>_inGameSettingsMenuPresenter.Show();
        public void ShowHighScore() => _highScorePresenter.Show();
        public void ShowPopUp()=>_highScorePopUpPresenter.Show();
        public void ShowGameOverMenu() => _gameOverMenuPresenter.Show();

        public void HidePopUp() => _highScorePopUpPresenter.Hide();

        public void HandleOpenGameOverMenu()
        {
            _inGamePresenter.Hide();
            _gameOverMenuPresenter.Show();
        }
        private void StartGameMenuLayout()
        {
            _mainMenuPresenter.Show();
            _settingsMenuPresenter.Hide();
            _gameOverMenuPresenter.Hide();
            _pauseMenuPresenter.Hide();
            _inGamePresenter.Hide();
            _inGameSettingsMenuPresenter.Hide(); 
            _highScorePresenter.Hide();
            _highScorePopUpPresenter.Hide();
            
        }

    }
}
