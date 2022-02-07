using Fps.Message;
using TMPro;
using UniRx;
using UnityEngine;

namespace Fps.Gameplay
{
    public class KillZombie : QuestBehavior
    {
        [SerializeField] private KillZombieQuest quest;
        [SerializeField] private TMP_Text result;

        private int target;
        private IntReactiveProperty progress = new IntReactiveProperty(0);

        private void Start()
        {
            target = quest.GetKillTarget();
            progress.Where(p => p >= target).Subscribe(_ => OnWin()).AddTo(this);
            progress.Subscribe(OnProgressChanged).AddTo(this);
            MessageBroker.Default.Receive<ZombieDie>().Subscribe(OnZombieKilled).AddTo(this);
        }

        private void OnProgressChanged(int progress)
        {
            result.text = $"Kill: {progress} / {target}";
        }

        private void OnZombieKilled(ZombieDie evt)
        {
            progress.Value += 1;
        }
    }
}