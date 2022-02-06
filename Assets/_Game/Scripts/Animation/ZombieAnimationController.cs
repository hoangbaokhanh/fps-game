using System.Threading;

namespace Fps.Animation
{
    public class ZombieAnimationController : BaseAnimationController
    {
        private CancellationTokenSource speedCts = new CancellationTokenSource();
        private const float IDLE_SPEED = 0.0f;
        private const float WALK_SPEED = 1f;
        private const float SMOOTH_STEP = 0.1f;
        
        public void Walk()
        {
            speedCts.Cancel();
            speedCts = new CancellationTokenSource();
            StopAttack();
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, WALK_SPEED, SMOOTH_STEP, speedCts.Token)
                .Forget();
        }

        public void Idle()
        {
            speedCts.Cancel();
            speedCts = new CancellationTokenSource();
            var currentSpeed = GetFloat(AnimationParameter.Speed);
            TransitionParam(AnimationParameter.Speed, currentSpeed, IDLE_SPEED, SMOOTH_STEP, speedCts.Token)
                .Forget();
        }

        public void Attack()
        {
            SetTrigger(AnimationParameter.Attack);
        }

        public void StopAttack()
        {
            SetTrigger(AnimationParameter.StopAttack);
        }
    }
}