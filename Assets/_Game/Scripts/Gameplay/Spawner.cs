using System;
using System.Collections.Generic;
using Fps.Character.Player;
using Fps.Character.Zombie;
using Fps.Common;
using Fps.Item;
using UnityEngine;
using Zenject;

namespace Fps.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> zombiePrefabs;

        [SerializeField] private GameObject battery;
        [SerializeField] private GameObject fish;
        [SerializeField] private GameObject medic;
        [SerializeField] private GameObject water;

        [SerializeField] private Transform spawnTransform;
        
        [Inject] private DiContainer container;

        private List<ZombieController> zombies = new List<ZombieController>();


        public PlayerController SpawnPlayer()
        {
            var spawnPoint = spawnPoints.Random();
            var playerObj = container.InstantiatePrefab(playerPrefab, spawnPoint.position, Quaternion.identity, spawnTransform);
            if (playerObj)
            {
                return playerObj.GetComponent<PlayerController>();
            }
            else
            {
                throw new Exception("Cannot Instantiate player");
            }
        }

        public ZombieController SpawnZombie()
        {
            var spawnPoint = spawnPoints.Random();
            var zombiePrefab = zombiePrefabs.Random();
            var zombieObj = container.InstantiatePrefab(zombiePrefab, spawnPoint.position, Quaternion.identity, spawnTransform);
            if (zombieObj)
            {
                var zombieControl =  zombieObj.GetComponent<ZombieController>();
                zombies.Add(zombieControl);
                return zombieControl;
            }
            else
            {
                throw new Exception("Cannot Instantiate player");
            }
        }

        public void ClearZombie()
        {
            foreach (var zombie in zombies)
            {
                Destroy(zombie.gameObject);
            }
            
            zombies.Clear();
        }

        public void SpawnItem(EItem item, Vector3 position)
        {
            switch (item)
            {
                case EItem.MedicBag:
                    container.InstantiatePrefab(medic, new Vector3(position.x, medic.transform.position.y, position.z), Quaternion.identity, spawnTransform);
                    break;
                case EItem.Battery:
                    container.InstantiatePrefab(battery, new Vector3(position.x, battery.transform.position.y, position.z), Quaternion.identity, spawnTransform);
                    break;
                case EItem.Fish:
                    container.InstantiatePrefab(fish, new Vector3(position.x, fish.transform.position.y, position.z), Quaternion.identity, spawnTransform);
                    break;
                case EItem.WaterBottle:
                    container.InstantiatePrefab(water, new Vector3(position.x, water.transform.position.y, position.z), Quaternion.identity, spawnTransform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item), item, null);
            }
        }
    }
}