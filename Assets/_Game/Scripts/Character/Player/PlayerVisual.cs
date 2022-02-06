using Fps.Animation;
using NaughtyAttributes;
using UnityEngine;

namespace Fps.Character.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerAudioController audioController;
        
        [SerializeField] private Transform grenadeSpawnPoint;
        [SerializeField] private Transform bulletSpawnPoint;
        
        public Transform GrenadeSpawnPoint => grenadeSpawnPoint;
        public Transform BulletSpawnPoint => bulletSpawnPoint;

        public void Walk()
        {
            audioController.Walk();
            animationController.Walk();
        }
        
        public void Run()
        {
            audioController.Run();
            animationController.Run();
        }

        public void Idle()
        {
            audioController.Idle();
            animationController.Idle();
        }

        public void Fire()
        {
            audioController.Shoot();
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

        public void Reload(bool isOutOfAmmo)
        {
            audioController.Reload(isOutOfAmmo);
            animationController.ReloadAmmo(isOutOfAmmo);
        }
    }
}