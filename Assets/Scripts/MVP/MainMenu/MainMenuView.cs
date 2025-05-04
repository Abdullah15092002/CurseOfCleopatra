using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.MainMenu
{
    public class MainMenuView:MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button highScoreButton;
        [SerializeField] private Button quitButton;

        public event Action OnStartClicked;
        public event Action OnSettingsClicked;
        public event Action OnHighScoreClicked;
        public event Action OnQuitClicked;

        private void Start()
        {
            startButton.onClick.AddListener(() => OnStartClicked?.Invoke());
            settingsButton.onClick.AddListener(() => OnSettingsClicked?.Invoke());
            highScoreButton.onClick.AddListener(() => OnHighScoreClicked.Invoke());
            quitButton.onClick.AddListener(() => OnQuitClicked.Invoke());
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
