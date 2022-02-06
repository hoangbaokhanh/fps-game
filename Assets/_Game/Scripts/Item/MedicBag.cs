using Fps.Character.Player;
using Fps.Common;
using UnityEngine;

namespace Fps.Item
{
    public class MedicBag : MonoBehaviour
    {
        [SerializeField] private int healthAmount;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayer())
            {
                var player = other.GetComponent<PlayerController>();
                if (player)
                {
                    player.Heal(healthAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}