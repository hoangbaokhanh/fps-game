using Fps.Animation;
using UnityEngine;

namespace Fps.Character.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private Transform grenadeSpawnPoint;
        [SerializeField] private Transform bulletSpawnPoint;

        public Transform GrenadeSpawnPoint => grenadeSpawnPoint;
        public Transform BulletSpawnPoint => bulletSpawnPoint;

        public void Walk()
        {
            animationController.Walk();
        }
        
        public void Run()
        {
            animationController.Run();
        }

        public void Idle()
        {
            animationController.Idle();
        }

        public void Fire()
        {
            animationController.Fire();
        }

        public void Aim()
        {
            animationController.Aim();
        }

        public void EndAim()
        {
            animationController.Aim();
        }
    }
}