namespace Fps.Character.Weapon
{
    public class Gun : IWeapon
    {
        public int Ammo;
        public int MaxAmmo;
        
        public bool CanAttack()
        {
            throw new System.NotImplementedException();
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}