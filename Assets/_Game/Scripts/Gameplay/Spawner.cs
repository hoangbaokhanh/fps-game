using System;
using System.Collections.Generic;
using Fps.Character.Player;
using Fps.Character.Zombie;
using Fps.Common;
using UnityEngine;
using Zenject;

namespace Fps.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<GameObject> zombiePrefabs;

        [Inject] private GameManager gameManager;
        [Inject] private DiContainer container;

        private void Start()
        {
            gameManager.SetSpawner(this);
        }

        public PlayerController SpawnPlayer()
        {
            var spawnPoint = spawnPoints.Random();
            var playerObj = container.InstantiatePrefab(playerPrefab, spawnPoint.position, Quaternion.identity, transform);
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
            var zombieObj = container.InstantiatePrefab(zombiePrefab, spawnPoint.position, Quaternion.identity, transform);
            if (zombieObj)
            {
                return zombieObj.GetComponent<ZombieController>();
            }
            else
            {
                throw new Exception("Cannot Instantiate player");
            }
        }
    }
}