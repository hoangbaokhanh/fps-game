using System;
using Rewired;
using UniRx;
using UnityEngine;
using Zenject;

namespace Fps.Input
{

    [System.Serializable]
    public struct Input
    {
        public Vector2 MoveVector;
        public Vector2 LookVector;
        public bool Fire;
        public bool Sprint;
    }
    
    public class GameInput: ITickable, IInitializable
    {
        private int playerId = 0;
        private Player rewiredPlayer;

        private bool activate = false;

        private Subject<Input> input = new Subject<Input>();
        public IObservable<Input> InputStream => input.AsObservable();

       
        public void Initialize()
        {
            rewiredPlayer = ReInput.players.GetPlayer(playerId);
            if (rewiredPlayer == null)
            {
                Debug.LogError("rewired player is null");
            }
        }

        public void SetActive(bool isActive)
        {
            activate = isActive;
            if (activate)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private Vector2 GetAxis(InputAction horizontal, InputAction vertical)
        {
            var x = rewiredPlayer.GetAxis((int) horizontal);
            var y = rewiredPlayer.GetAxis((int) vertical);

            return new Vector2(x, y);
        }
        
        private bool GetButtonDown(InputAction action)
        {
            return rewiredPlayer.GetButtonDown((int) action);
        }
        private bool GetButton(InputAction action)
        {
            return rewiredPlayer.GetButton((int) action);
        }

        
        public void Tick()
        {
            if (activate)
            {
                var move = GetAxis(InputAction.MoveHorizontal, InputAction.MoveVertical);
                var look = GetAxis(InputAction.FireHorizontal, InputAction.FireVertical);
                var isFire = GetButton(InputAction.Fire);
                var isSprint = GetButton(InputAction.Sprint);
                
                input.OnNext(new Input()
                {
                    MoveVector = move,
                    LookVector = look,
                    Fire = isFire,
                    Sprint = isSprint
                });
            }
        }

    }
}