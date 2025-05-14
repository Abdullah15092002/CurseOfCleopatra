
using Assets.Scripts.Enum;
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
        public void Show(PowerUpType type,Sprite sprite)
        {
            _view.Show(type,sprite);
        }
        public void Hide(PowerUpType type) 
        {
            _view.Hide(type); 
        }
    }
}
