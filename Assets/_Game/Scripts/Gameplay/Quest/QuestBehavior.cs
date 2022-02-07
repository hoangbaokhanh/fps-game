using Fps.Input;
using Fps.Message;
using Fps.UI;
using Fps.UI.Menu;
using Rewired;
using UniRx;
using UnityEngine;
using Zenject;

namespace Fps.Gameplay
{
    public class QuestBehavior : MonoBehaviour
    {
        [Inject] private UIRoot root;
        [Inject] private GameManager gameManager;
        [Inject] private GameInput gameInput;
        [Inject] private Spawner spawner;
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
            OnEnd();
            var result = root.PushMenu("result");
        
            if (result is Result resultMenu)
            {
                resultMenu.SetWinStatus(false);
            }
          
        }

        private void OnEnd()
        {
            gameInput.SetActive(false);
            spawner.ClearZombie();
        }

        public void OnWin()
        {
            OnEnd();
            var result = root.PushMenu("result");
            if (result is Result resultMenu)
            {
                resultMenu.SetWinStatus(true);
            }
        }
    }
}