using System;
using System.Collections.Generic;
using System.Linq;
using Fps.Character.Player;
using Fps.Common;
using Fps.Item;
using Fps.Message;
using UniRx;
using UnityEngine;
using Zenject;

namespace Fps.Gameplay
{
    public enum QuestFormat
    {
        None,
        KillZombie,
        CollectItem
    }
    
    public class GameManager: MonoBehaviour
    {
        [Inject] private Spawner spawner;
        [Inject] private DiContainer diContainer;
        private CompositeDisposable spawnZombieDisposable = new CompositeDisposable();
        private CompositeDisposable disposable = new CompositeDisposable();

        [SerializeField] private float zombieSpawnRate = 5f;
        [SerializeField] private List<QuestBehavior> quests;
        private PlayerController playerController;
        private List<EItem> items;
        private QuestFormat quest = QuestFormat.None;
        
        private QuestBehavior currentQuest;
        void Start()
        {
            items = Enum.GetValues(typeof(EItem)).Cast<EItem>().ToList();
            MessageBroker.Default.Receive<ZombieDie>().Subscribe(OnZombieDie).AddTo(disposable);
            OnStartGame();
        }

        private void OnZombieDie(ZombieDie evt)
        {
            var randomItem = items.Random();
            spawner.SpawnItem(randomItem, evt.Position);
        }
        
        public PlayerController GetPlayer()
        {
            return playerController;
        }

        private QuestBehavior RandomQuest()
        {
            return quests.Random();
        }
        
        public void OnStartGame()
        {
            var q = RandomQuest();
            var qObject = diContainer.InstantiatePrefab(q.gameObject);
            currentQuest = qObject.GetComponent<QuestBehavior>();
            
            playerController = spawner.SpawnPlayer();
            
            Observable.Interval(TimeSpan.FromSeconds(zombieSpawnRate)).Subscribe(_ =>
            {
                spawner.SpawnZombie();
            }).AddTo(spawnZombieDisposable);
        }

        public void OnEndGame()
        {
            Destroy(currentQuest.gameObject);
        }
    }
}