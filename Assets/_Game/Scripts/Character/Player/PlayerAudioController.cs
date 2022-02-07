using UnityEngine;

namespace Fps.Character.Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource shootSrc;
        [SerializeField] private AudioSource reloadSrc;
        [SerializeField] private AudioSource walkSrc;
        [SerializeField] private AudioSource hurtSrc;

        [SerializeField] private AudioClip reload;
        [SerializeField] private AudioClip reloadOutOfAmmo;

        [SerializeField] private AudioClip walk;
        [SerializeField] private AudioClip run;


        public void Walk()
        {
            walkSrc.clip = walk;
            if (!walkSrc.isPlaying)
            {
                walkSrc.Play();
            }
            
        }

        public void Idle()
        {
            if (walkSrc.isPlaying)
            {
                walkSrc.Stop();
            }
        }

        public void Run()
        {
            walkSrc.clip = run;
            if (!walkSrc.isPlaying)
            {
                walkSrc.Play();
            }
        }

        public void Shoot()
        {
            shootSrc.Play();
        }

        public void Reload(bool isOutOfAmmo)
        {
            var clip = isOutOfAmmo ? reloadOutOfAmmo : reload;
            reloadSrc.clip = clip;
            reloadSrc.Play();
        }
        
        public void Hurt()
        {
            hurtSrc.Play();
        }
    }
}