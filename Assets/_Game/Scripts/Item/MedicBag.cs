using System;
using Cysharp.Threading.Tasks;
using Fps.Character.Player;
using Fps.Common;
using UnityEngine;

namespace Fps.Item
{
    public class MedicBag : MonoBehaviour
    {
        [SerializeField] private int healthAmount;
        [SerializeField] private AudioSource audioSource;
        private async void OnTriggerEnter(Collider other)
        {
            if (other.IsPlayer())
            {
                var player = other.GetComponent<PlayerController>();
                if (player)
                {
                    audioSource.Play();
                    await UniTask.Delay(TimeSpan.FromSeconds(1));
                    player.Heal(healthAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}