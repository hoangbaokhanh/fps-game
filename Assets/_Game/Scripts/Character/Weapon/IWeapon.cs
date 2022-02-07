using UnityEngine;

namespace Fps.Character.Weapon
{
    public interface IWeapon
    {
        public bool CanAttack();
        public void Attack(Vector3 from, Vector3 to);
    }
}