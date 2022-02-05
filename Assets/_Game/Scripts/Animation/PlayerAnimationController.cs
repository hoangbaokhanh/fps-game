using System.Threading;
using NaughtyAttributes;
using Random = UnityEngine.Random;

namespace Fps.Animation
{
    public class PlayerAnimationController : BaseAnimationController
    {
        private const float IDLE_SPEED = 0.0f;
        private const float WALK_SPEED = 0.5f;
        private const float RUN_SPEED = 1f;
        private const float SMOOTH_STEP = 0.1f;

        [Button]
        public void Walk()
        {
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, WALK_SPEED, SMOOTH_STEP, new CancellationToken())
                .Forget();
        }

        [Button]
        public void Run()
        {
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, RUN_SPEED, SMOOTH_STEP, new CancellationToken())
                .Forget();
        }

        [Button]
        public void Idle()
        {
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, IDLE_SPEED, SMOOTH_STEP, new CancellationToken())
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

        [Button]
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