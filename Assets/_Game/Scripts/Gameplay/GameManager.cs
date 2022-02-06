using System;
using Fps.Character.Player;
using UniRx;
using Zenject;

namespace Fps.Gameplay
{
    public class GameManager: IInitializable
    {
        private Spawner spawner;
        private CompositeDisposable spawnZombieDisposable = new CompositeDisposable();

        private const float zombieSpawnRate = 5f;
        
        private PlayerController playerController;
        public void Initialize()
        {
            
        }

        public void SetSpawner(Spawner spawner)
        {
            this.spawner = spawner;
            playerController = spawner.SpawnPlayer();
            Observable.Interval(TimeSpan.FromSeconds(zombieSpawnRate)).Subscribe(_ =>
            {
                spawner.SpawnZombie();
            }).AddTo(spawnZombieDisposable);
        }

        public PlayerController GetPlayer()
        {
            return playerController;
        }
    }
}