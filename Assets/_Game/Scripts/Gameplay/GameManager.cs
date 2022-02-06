using System;
using System.Collections.Generic;
using System.Linq;
using Fps.Character.Player;
using Fps.Common;
using Fps.Item;
using Fps.Message;
using UniRx;
using Zenject;

namespace Fps.Gameplay
{
    public enum QuestFormat
    {
        None,
        KillZombie,
        CollectItem
    }
    
    public class GameManager: IInitializable
    {
        private Spawner spawner;
        private CompositeDisposable spawnZombieDisposable = new CompositeDisposable();
        private CompositeDisposable disposable = new CompositeDisposable();

        private const float zombieSpawnRate = 5f;
        
        private PlayerController playerController;
        private List<EItem> items;
        private QuestFormat quest = QuestFormat.None;
        
        public void Initialize()
        {
            items = Enum.GetValues(typeof(EItem)).Cast<EItem>().ToList();
            MessageBroker.Default.Receive<ZombieDie>().Subscribe(OnZombieDie).AddTo(disposable);
        }

        private void OnZombieDie(ZombieDie evt)
        {
            var randomItem = items.Random();
            spawner.SpawnItem(randomItem, evt.Position);
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