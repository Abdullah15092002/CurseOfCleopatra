using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MVP.InGameSettingsMenu
{
    public class InGameSettingsMenuPresenter
    {
        InGameSettingsMenuView _view;
        public event Action OnPauseMenuShow;
        public InGameSettingsMenuPresenter(InGameSettingsMenuView view)
        {
            _view = view;
            _view.OnBackClicked += HandleBackClick;
        }

        private void HandleBackClick()
        {
            _view.Hide();
            OnPauseMenuShow?.Invoke();          
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
