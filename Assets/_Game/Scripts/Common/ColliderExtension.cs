using UnityEngine;

namespace Fps.Common
{
    public static class ColliderExtension
    {
        public const string PLAYER = "Player";
        public const string ZOMBIE = "Zombie";
        
        public static bool IsPlayer(this Collider other)
        {
            return other.CompareTag(PLAYER);
        }

        public static bool IsZombie(this Collider other)
        {
            return other.CompareTag(ZOMBIE);
        }
    }
}