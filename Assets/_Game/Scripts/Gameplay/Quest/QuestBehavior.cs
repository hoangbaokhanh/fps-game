using System;
using Fps.Message;
using UniRx;
using UnityEngine;

namespace Fps.Gameplay
{
    public class QuestBehavior : MonoBehaviour
    {
        private void Start()
        {
            MessageBroker.Default.Receive<PlayerDeath>().Subscribe(OnPlayerDeath).AddTo(this);
        }

        private void OnPlayerDeath(PlayerDeath evt)
        {
            OnLoose();
        }

        public virtual void OnLoose(){}
    }
}