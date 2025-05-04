

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.MainMenu.SettingsMenu
{
    public class SettingsMenuView:MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button gameMusicSwitchButton;
        [SerializeField] private Button mainMusicSwitchButton;

        public Transform GameMusicSwitchButtonTransform => gameMusicSwitchButton.transform;
        public Transform MainMusicSwitchButtonTransform => mainMusicSwitchButton.transform;

        public event Action OnMainMenuClicked;
        public event Action OnGameMusicSwitchClicked;
        public event Action OnMainMusicSwitchClicked;

        private void Start()
        {
            mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
            gameMusicSwitchButton.onClick.AddListener(() => OnGameMusicSwitchClicked?.Invoke());
            mainMusicSwitchButton.onClick.AddListener(() => OnMainMusicSwitchClicked?.Invoke());
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
