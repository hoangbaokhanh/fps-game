using Fps.Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Fps.UI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        
        public void SetMaxHealth(int health)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }

        public void SetHealth(int health)
        {
            healthSlider.value = health;
        }

        public void SetWeapon(WeaponClass weapon)
        {
            
        }
    }
}