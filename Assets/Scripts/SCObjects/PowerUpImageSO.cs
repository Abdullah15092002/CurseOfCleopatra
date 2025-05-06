using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SCObjects
{
    [CreateAssetMenu(fileName = "PowerUpImageSO", menuName = "SCObjects/PowerUpImageData", order = 1)]
    public class PowerUpImageSO:ScriptableObject
    {
         public Sprite magnetSprite;
    }
}
