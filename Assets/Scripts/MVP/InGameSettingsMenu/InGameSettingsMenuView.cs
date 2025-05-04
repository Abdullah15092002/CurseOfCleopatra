using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.InGameSettingsMenu
{
    public class InGameSettingsMenuView:MonoBehaviour
    {
        [SerializeField] private Button BackButton;
        public event Action OnBackClicked;


        private void Start()
        {
            BackButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
