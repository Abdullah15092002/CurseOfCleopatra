using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.PauseMen
{
    public class PauseMenuView:MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button settingsButton;

        public event Action onMainMenuClicked;
        public event Action onResumeClicked;
        public event Action onRestartClicked;
        public event Action onSettingsClicked;


        private void Start()
        {
            mainMenuButton.onClick.AddListener(() => onMainMenuClicked?.Invoke());
            resumeButton.onClick.AddListener(() => onResumeClicked?.Invoke());
            restartButton.onClick.AddListener(() => onRestartClicked?.Invoke());
            settingsButton.onClick.AddListener(() => onSettingsClicked?.Invoke());
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
