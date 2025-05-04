using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MVP.HighScorepPopUp
{
    public class HighScorePopUpPresenter
    {
        private HighScorePopUpView _view;
        public HighScorePopUpPresenter(HighScorePopUpView view)
        {
            _view = view;
            
        }
        public void Show() => _view.Show();
        public void Hide() => _view.Hide();
    }
}
