using Fps.Item;
using Fps.Message;
using TMPro;
using UniRx;
using UnityEngine;

namespace Fps.Gameplay
{
    public class CollectItem : QuestBehavior
    {
        [SerializeField] private CollectItemQuest quest;
        [SerializeField] private TMP_Text result;
        private EItem targetItem;
        private int target;
        private IntReactiveProperty progress = new IntReactiveProperty();
        
        private void Start()
        {
            targetItem = quest.GetItemTarget();
            target = Random.Range(5, 15);
            progress.Where(p => p >= target).Subscribe(_ => OnWin()).AddTo(this);
            progress.Subscribe(OnProgressChanged).AddTo(this);
            MessageBroker.Default.Receive<PickupItem>().Where(evt => evt.Item == targetItem).Subscribe(OnItemCollected).AddTo(this);
        }

        private void OnProgressChanged(int progress)
        {
            result.text = $"Collect: {progress} / {target} {targetItem}";
        }

        private void OnItemCollected(PickupItem evt)
        {
            progress.Value += 1;
        }
        
        private void OnWin()
        {
            // handle win logic
        }

        public override void OnLoose()
        {
            // handle loose logic
        }
    }
}