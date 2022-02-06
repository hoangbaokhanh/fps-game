using Fps.Message;
using Fps.UI;
using Fps.UI.Menu;
using UniRx;
using UnityEngine;
using Zenject;

namespace Fps.Gameplay
{
    public class QuestBehavior : MonoBehaviour
    {
        [Inject] private UIRoot root;
        private void Start()
        {
            MessageBroker.Default.Receive<PlayerDeath>().Subscribe(OnPlayerDeath).AddTo(this);
        }

        private void OnPlayerDeath(PlayerDeath evt)
        {
            OnLoose();
        }

        public void OnLoose()
        {
            var result = root.PushMenu("result");
            if (result is Result resultMenu)
            {
                resultMenu.SetWinStatus(false);
            }
            
        }

        public void OnWin()
        {
            var result = root.PushMenu("result");
            if (result is Result resultMenu)
            {
                resultMenu.SetWinStatus(true);
            }
        }
    }
}