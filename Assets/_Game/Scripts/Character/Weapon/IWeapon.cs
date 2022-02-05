namespace Fps.Character.Weapon
{
    public interface IWeapon
    {
        public bool CanAttack();
        public void Attack();
    }
}