using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts.MVP.MainMenu.SettingsMenu
{
    public class SettingsMenuPresenter
    {
        private SettingsMenuView _view;

        public event Action OnMainMenuShow;
        public SettingsMenuPresenter(SettingsMenuView view)
        {
            _view = view;
            _view.OnMainMenuClicked += HandleMainMenu;
            _view.OnGameMusicSwitchClicked += HandleGameMusicSwitch;
            _view.OnMainMusicSwitchClicked += HandlMainMusicSwitch;

        }
        private void HandleMainMenu() 
        {
            _view.Hide();
            OnMainMenuShow?.Invoke();
        }
        private void HandleGameMusicSwitch()
        {
            Debug.Log("GameMusicSwitched");
            var buttonTransform = _view.GameMusicSwitchButtonTransform.transform;
            var switchState = 1;
              buttonTransform.DOLocalMoveX(-buttonTransform.localPosition.x, 0.2f);
                switchState = Math.Sign(-buttonTransform.localPosition.x);
            
        }
        private void HandlMainMusicSwitch() 
        {
            Debug.Log("MainMusicSwitched");
            var buttonTransform = _view.MainMusicSwitchButtonTransform.transform;
            var switchState = 1;
            buttonTransform.DOLocalMoveX(-buttonTransform.localPosition.x, 0.2f);
            switchState = Math.Sign(-buttonTransform.localPosition.x);

        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
