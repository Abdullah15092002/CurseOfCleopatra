using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MVP.PowerUpLayout
{
    public class PowerUpPresenter
    {
        private PowerUpView _view;
        public PowerUpPresenter(PowerUpView view)
        {
            _view = view;
        }
        public void Show(Sprite powerUpImage) =>  _view.Show(powerUpImage);
        
        public void Hide() => _view.Hide();
    }
}
