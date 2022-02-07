using System;
using Cysharp.Threading.Tasks;
using Fps.Character.Player;
using Fps.Character.Zombie;
using Fps.Common;
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
        
        private IntReactiveProperty ammo = new IntReactiveProperty();
        public IObservable<int> Ammo => ammo.AsObservable();

        private bool isReloading = false;

        private void Start()
        {
            ammo.Value = MaxAmmo;
            ammo.Where(ammo => ammo <= 0).Subscribe(OnOutOfAmmo).AddTo(this);
        }

        private void OnOutOfAmmo(int ammoLeft)
        {
            ReloadWeapon(true).Forget();
        }

        public bool CanAttack()
        {
            return ammo.Value > 0 && !isReloading;
        }

        public void Attack(Vector3 from, Vector3 to)
        {
            if (CanAttack())
            {
                ammo.Value -= 1;
                visual.Fire();
                muzzle.Play();
                spark.Play();
                if (Physics.Raycast(from, to, out var hit, MaxDistance))
                {
                    if (hit.collider.IsZombie())
                    {
                        var zombie = hit.transform.GetComponent<ZombieController>();
                        zombie.TakeDamage(Damage);
                    }
                }
            }
        }

        public async UniTask ReloadWeapon()
        {
            await ReloadWeapon(ammo.Value == 0);
        }
        
        public async UniTask ReloadWeapon(bool isOutOfAmmo)
        {
            isReloading = true;
            visual.Reload(isOutOfAmmo);
            await UniTask.Delay(TimeSpan.FromSeconds(ReloadTimeSec));
            ammo.Value = MaxAmmo;
            isReloading = false;
        }
    }
}