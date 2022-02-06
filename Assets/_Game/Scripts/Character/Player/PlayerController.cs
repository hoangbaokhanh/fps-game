using System;
using Cysharp.Threading.Tasks;
using Fps.Character.Weapon;
using Fps.Common;
using Fps.Input;
using Fps.UI;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Fps.Character.Player
{
    public enum WeaponClass
    {
        None,
        AssaultRifle,
        Handgun,
    }

    public partial class PlayerController : MonoBehaviour
    {
        [SerializeField, Required] private Camera playerCamera;
        
 
        [SerializeField, Required, BoxGroup("Visual Prefab")]
        private GameObject assault;

        [SerializeField, Required, BoxGroup("Visual Prefab")]
        private GameObject handgun;

        [SerializeField] private int maxHealth;
        
        [SerializeField] private PlayerHud hud;
        
        [Inject] private GameInput gameInput;

        private ReactiveProperty<WeaponClass> wpClass = new ReactiveProperty<WeaponClass>(WeaponClass.AssaultRifle);
        private IntReactiveProperty health = new IntReactiveProperty();
        public IObservable<int> Health => health.AsObservable();
        
        private PlayerVisual playerVisual;
        private Gun gun;

        private void Start()
        {
            wpClass.Where(wp => wp != WeaponClass.None).Subscribe(OnWeaponChanged).AddTo(this);
            health.Value = maxHealth;
            hud.SetMaxHealth(maxHealth);
            gameInput.InputStream.Subscribe(OnInput).AddTo(this);
            gameInput.SetActive(true);
            
            health.PairWithPrevious()
                .Where(snapshot => snapshot.Previous > snapshot.Current)
                .Subscribe(_ => OnHurt())
                .AddTo(this);

            health.Subscribe(OnHealthChanged).AddTo(this);
        }
        
        private void OnInput(Input.Input input)
        {
            Rotation(input.LookVector);
            Move(input.MoveVector, input.Sprint);
            
            if (input.Fire)
            {
                Fire();
            }

            if (input.HandGun)
            {
                SetWeaponClass(WeaponClass.Handgun);
            }

            if (input.Assult)
            {
                SetWeaponClass(WeaponClass.AssaultRifle);
            }

            if (input.Reload)
            {
                gun.ReloadWeapon().Forget();
            }
        }

        private void OnHurt()
        {
            playerVisual.Hurt();
        }

        private void OnHealthChanged(int health)
        {
            hud.SetHealth(health);
        }

        public void TakeDamage(int damage)
        {
            health.Value -= damage;
        }

        public void Heal(int healAmount)
        {
            health.Value = Mathf.Min(maxHealth, health.Value + healAmount);
        }
        
        private void OnWeaponChanged(WeaponClass weaponClass)
        {
            LoadVisual(weaponClass);
        }

        public void SetWeaponClass(WeaponClass weaponClass)
        {
            wpClass.Value = weaponClass;
        }

        private void LoadVisual(WeaponClass weaponClass)
        {
            var weapon = weaponClass switch
            {
                WeaponClass.AssaultRifle => Instantiate(assault, playerCamera.transform),
                WeaponClass.Handgun => Instantiate(handgun, playerCamera.transform),
                _ => null
            };

            if (weapon)
            {
                if (playerVisual)
                {
                    Destroy(playerVisual.gameObject);
                }

                playerVisual = weapon.GetComponent<PlayerVisual>();
                gun = weapon.GetComponent<Gun>();

                if (playerVisual == null)
                {
                    Debug.LogError("You forgot to attach PlayerVisual script to visual prefab");
                }
            }
            else
            {
                Debug.LogError("Cannot load visual of weapon class " + weaponClass);
            }
        }

        private void Fire()
        {
            gun.Attack(playerCamera.transform.position, playerCamera.transform.forward);
        }

        
        [Button]
        private void SwitchToAssault()
        {
            wpClass.Value = WeaponClass.AssaultRifle;
        }

        [Button]
        private void SwitchToHandGun()
        {
            wpClass.Value = WeaponClass.Handgun;
        }
    }
}