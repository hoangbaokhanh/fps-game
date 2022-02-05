using NaughtyAttributes;
using UniRx;
using UnityEngine;

namespace Fps.Character.Player
{
    public enum WeaponClass
    {
        None,
        AssaultRifle,
        Handgun,
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Required] private Camera playerCamera;

        // Todo: Should load them dynamically, but ok for reference atm;
        [SerializeField, Required, BoxGroup("Visual Prefab")]
        private GameObject assault;

        [SerializeField, Required, BoxGroup("Visual Prefab")]
        private GameObject handgun;

        private ReactiveProperty<WeaponClass> wpClass = new ReactiveProperty<WeaponClass>(WeaponClass.AssaultRifle);
        private PlayerVisual playerVisual;

        private void Start()
        {
            wpClass.Where(wp => wp != WeaponClass.None).Subscribe(OnWeaponChanged).AddTo(this);
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
                WeaponClass.AssaultRifle => Instantiate(assault, transform),
                WeaponClass.Handgun => Instantiate(handgun, transform),
                _ => null
            };

            if (weapon)
            {
                if (playerVisual)
                {
                    Destroy(playerVisual.gameObject);
                }

                playerVisual = weapon.GetComponent<PlayerVisual>();

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