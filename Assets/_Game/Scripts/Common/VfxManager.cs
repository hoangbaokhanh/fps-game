using System;
using Fps.Common.Pool;
using UnityEngine;

namespace Fps.Common
{
    public class VfxManager : MonoBehaviour
    {
        [SerializeField] private GameObject bloodPrefab;

        private void Start()
        {
            PoolManager.instance.CreatePool("blood", bloodPrefab, 10);
        }

        public void PlayBloodVfx(Vector3 position)
        {
            var psObject = PoolManager.instance.GetObject("blood", position, Quaternion.identity);
            var particle = psObject.GetComponent<ParticleSystem>();
            if (!particle.isPlaying)
            {
                particle.Play();
            }
            else
            {
                particle.Emit(1);
            }
        }
    }
}