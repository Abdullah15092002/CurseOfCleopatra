using Assets.Scripts.Managers;
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
            _view.OnGameMusicSwitchClicked += HandleInGameMusicSwitch;
            _view.OnMainMusicSwitchClicked += HandlMainMusicSwitch;

        }
        private void HandleMainMenu() 
        {
            _view.Hide();
            OnMainMenuShow?.Invoke();
        }
        private void HandlMainMusicSwitch()
        {
            var buttonTransform = _view.MainMusicSwitchButtonTransform;
            float targetX = -buttonTransform.localPosition.x;
            buttonTransform
                .DOLocalMoveX(targetX, 0.2f)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    int switchState = Math.Sign(buttonTransform.localPosition.x);

                    if (switchState == 1)
                        AudioManager.Instance.PlayBackgroundMusic();
                    else
                        AudioManager.Instance.StopBackgroundMusic();

                    Debug.Log($"Music Switch State: {switchState}");
                });
        }
        private void HandleInGameMusicSwitch()
            {
            Debug.Log("InGameMusicSwitched");
            }
        
        
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
