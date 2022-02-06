using System;
using Cysharp.Threading.Tasks;
using Fps.Character.Player;
using UniRx;
using UnityEngine;

namespace Fps.Character.Weapon
{
    public class Gun : MonoBehaviour, IWeapon
    {
        [SerializeField] private PlayerVisual visual;
        
        [SerializeField] private ParticleSystem muzzle;
        [SerializeField] private ParticleSystem spark;
       
        public int MaxAmmo;
        public int MaxDistance;
        public int Damage;
        public float ReloadTimeSec;
        
        private IntReactiveProperty Ammo = new IntReactiveProperty();

        private bool isReloading = false;

        private void Start()
        {
            Ammo.Value = MaxAmmo;
            Ammo.Where(ammo => ammo <= 0).Subscribe(OnOutOfAmmo).AddTo(this);
        }

        private void OnOutOfAmmo(int ammoLeft)
        {
            ReloadWeapon(true).Forget();
        }

        public bool CanAttack()
        {
            return Ammo.Value > 0 && !isReloading;
        }

        public void Attack(Vector3 from, Vector3 to)
        {
            if (CanAttack())
            {
                Ammo.Value -= 1;
                visual.Fire();
                
                //muzzle.Play();
                spark.Play();
                if (Physics.Raycast(from, to, out var hit, MaxDistance))
                {
                    Debug.Log(hit.transform.name);
                }
            }
        }
        
        public async UniTask ReloadWeapon(bool isOutOfAmmo)
        {
            isReloading = true;
            visual.Reload(isOutOfAmmo);
            await UniTask.Delay(TimeSpan.FromSeconds(ReloadTimeSec));
            Ammo.Value = MaxAmmo;
            isReloading = false;
        }
    }
}