using System.Threading;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fps.Animation
{
    public class PlayerAnimationController : BaseAnimationController
    {
        private const float IDLE_SPEED = 0.0f;
        private const float WALK_SPEED = 0.5f;
        private const float RUN_SPEED = 1f;
        private const float SMOOTH_STEP = 0.1f;

        private CancellationTokenSource speedCts = new CancellationTokenSource();

        [Button]
        public void Walk()
        {
            speedCts.Cancel();
            speedCts = new CancellationTokenSource();
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, WALK_SPEED, SMOOTH_STEP, speedCts.Token)
                .Forget();
        }

        [Button]
        public void Run()
        {
            speedCts.Cancel();
            speedCts = new CancellationTokenSource();
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            Debug.LogError("Curren Speed " + currentSpeed);
            Debug.LogError("run Speed " + RUN_SPEED);
            
            TransitionParam(AnimationParameter.Speed, currentSpeed, RUN_SPEED, SMOOTH_STEP, speedCts.Token)
                .Forget();
        }

        [Button]
        public void Idle()
        {
            speedCts.Cancel();
            speedCts = new CancellationTokenSource();
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, IDLE_SPEED, SMOOTH_STEP, speedCts.Token)
                .Forget();
        }

        [Button]
        public void Aim()
        {
            SetTrigger(AnimationParameter.AimIn);
        }
        
        [Button]
        public void EndAim()
        {
            SetTrigger(AnimationParameter.AimOut);
        }

        [Button]
        public void KnifeAttack()
        {
            var knifeIndex = Random.Range(0, 1);
            SetInteger(AnimationParameter.KnifeIndex, knifeIndex);
            SetTrigger(AnimationParameter.KnifeAttack);
        }

        [Button]
        public void ThrowGrenade()
        {
            SetTrigger(AnimationParameter.Throw);
        }

       
        public void ReloadAmmo(bool isOutOfAmmo)
        {
            SetTrigger(isOutOfAmmo ? AnimationParameter.ReloadOutOfAmmo : AnimationParameter.Reload);
        }

        public void Fire()
        {
            SetTrigger(AnimationParameter.Fire);
        }
    }
}