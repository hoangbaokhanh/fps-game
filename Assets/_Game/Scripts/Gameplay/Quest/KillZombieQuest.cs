using System.Collections.Generic;
using Fps.Common;
using UnityEngine;

namespace Fps.Gameplay
{
    [CreateAssetMenu(fileName = "KillZombie", menuName = "Fps/Kill-Zombie", order = 0)]
    public class KillZombieQuest : ScriptableObject
    {
        [SerializeField] private List<int> targets;

        public int GetKillTarget()
        {
            return targets.Random();
        }
    }
}