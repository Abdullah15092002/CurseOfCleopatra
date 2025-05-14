using Assets.Scripts.Enum;
using Assets.Scripts.MVP.GameOverMen;
using Assets.Scripts.MVP.HighScorepPopUp;
using Assets.Scripts.MVP.InGameLayout;
using Assets.Scripts.MVP.InGameSettingsMenu;
using Assets.Scripts.MVP.MainMenu;
using Assets.Scripts.MVP.MainMenu.HighScoreMenu;
using Assets.Scripts.MVP.MainMenu.SettingsMenu;
using Assets.Scripts.MVP.PauseMen;
using Assets.Scripts.MVP.PowerUpLayout;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MainMenuView MainMenuView;
        [SerializeField] private SettingsMenuView SettingsMenuView;
        [SerializeField] private PauseMenuView PauseMenuView;
        [SerializeField] private GameOverMenuView GameOverMenuView;
        [SerializeField] private InGameView InGameView;
        [SerializeField] private InGameSettingsMenuView InGameSettingsMenuView;
        [SerializeField] private HighScoreView HighScoreView;
        [SerializeField] private HighScorePopUpView HighScorePopUpView;
        [SerializeField] private PowerUpView PowerUpView;
        private UIFlowManager UIFlowManager;
        public static UIManager Instance { get; private set; }
        public event Action OnOpenGameOverMenu;
        public event Action OnShowPopUp;
        public event Action OnHidePopUp;
        public event Action<PowerUpType,Sprite> OnShowPowerUp;
        public event Action<PowerUpType> OnHidePowerUp;
        private bool hasInitializedUI = false;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {        
            if (scene.name == "MenuScene"&& hasInitializedUI==false)
            {
                hasInitializedUI = true;
                MainMenuView = FindObjectOfType<MainMenuView>();
                SettingsMenuView = FindObjectOfType<SettingsMenuView>();
                PauseMenuView = FindObjectOfType<PauseMenuView>();
                GameOverMenuView = FindObjectOfType<GameOverMenuView>();
                InGameView = FindObjectOfType<InGameView>();
                InGameSettingsMenuView = FindObjectOfType<InGameSettingsMenuView>();
                HighScoreView = FindObjectOfType<HighScoreView>();
                HighScorePopUpView = FindObjectOfType<HighScorePopUpView>();
                PowerUpView = FindObjectOfType<PowerUpView>();

                DontDestroyOnLoad(MainMenuView);
                DontDestroyOnLoad(SettingsMenuView);
                DontDestroyOnLoad(PauseMenuView);
                DontDestroyOnLoad(GameOverMenuView);
                DontDestroyOnLoad(InGameView);
                DontDestroyOnLoad(InGameSettingsMenuView);
                DontDestroyOnLoad(HighScoreView);
                DontDestroyOnLoad(HighScorePopUpView);
                DontDestroyOnLoad(PowerUpView);

                UIFlowManager = new UIFlowManager(
                    MainMenuView, SettingsMenuView,PauseMenuView,
                    GameOverMenuView,InGameView,InGameSettingsMenuView,
                    HighScoreView,HighScorePopUpView,PowerUpView);
            }
            
        }
        
        public void OpenGameOverMenu()
        {
            OnOpenGameOverMenu?.Invoke();
        } 
        public void PopUpRoutinePlay()
        {
            StartCoroutine(PopUpRoutine());
        }
        public IEnumerator PopUpRoutine()
        {
            OnShowPopUp?.Invoke();
            yield return new WaitForSeconds(2f);
            OnHidePopUp?.Invoke();
        }

        public void OnShowPowerUpLayout(PowerUpType type,Sprite powerUpImage)
        {
            OnShowPowerUp?.Invoke(type, powerUpImage);
        }
        public void OnHidePowerUpLayout(PowerUpType type)
        {
            OnHidePowerUp?.Invoke(type);
        }
    }
}
