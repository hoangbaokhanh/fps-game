using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Fps.Animation
{
    public enum AnimationParameter
    {
        Speed,
        Throw,
        KnifeIndex,
        KnifeAttack,
        Fire,
        AimIn,
        AimOut,
        Pose,
        Reload,
        ReloadOutOfAmmo
    }

    public class BaseAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly Dictionary<AnimationParameter, int> animationParameterHash =
            new Dictionary<AnimationParameter, int>();


        private void Awake()
        {
            foreach (var param in (AnimationParameter[]) Enum.GetValues(typeof(AnimationParameter)))
            {
                animationParameterHash[param] = Animator.StringToHash(param.ToString());
            }
        }

        protected void SetTrigger(AnimationParameter parameter)
        {
            if (animator != null)
            {
                animator.SetTrigger(animationParameterHash[parameter]);
            }
        }

        protected void ResetTrigger(AnimationParameter parameter)
        {
            if (animator != null)
            {
                animator.ResetTrigger(animationParameterHash[parameter]);
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        protected void SetFloat(AnimationParameter parameter, float value)
        {
            animator.SetFloat(animationParameterHash[parameter], value);
        }

        protected float GetFloat(AnimationParameter parameter)
        {
            return animator.GetFloat(animationParameterHash[parameter]);
        }

        //////////////////////////////////////////////////////////////////////////////

        protected void SetBool(AnimationParameter parameter, bool value)
        {
            animator.SetBool(animationParameterHash[parameter], value);
        }

        protected bool GetBool(AnimationParameter parameter)
        {
            return animator.GetBool(animationParameterHash[parameter]);
        }

        //////////////////////////////////////////////////////////////////////////////

        protected void SetInteger(AnimationParameter parameter, int value)
        {
            animator.SetInteger(animationParameterHash[parameter], value);
        }

        protected int GetInteger(AnimationParameter parameter)
        {
            return animator.GetInteger(animationParameterHash[parameter]);
        }

        protected async UniTaskVoid TransitionParam(AnimationParameter param, float start, float end, float step,
            CancellationToken ct)
        {
            void UpdateParam(float value, CancellationToken cancellationToken)
            {
                ct.ThrowIfCancellationRequested();
                SetFloat(param, Mathf.Lerp(start, end, value));
            }

            await SmoothStep(step, UpdateParam, ct);
        }

        private async UniTask SmoothStep(float duration, Action<float, CancellationToken> update,
            CancellationToken token, bool smooth = true)
        {
            var step = 1f / duration;
            var time = 0f;

            while (time <= 1)
            {
                if (smooth)
                {
                    update(Mathf.SmoothStep(0f, 1f, time), token);
                }
                else
                {
                    update(time, token);
                }

                time += Time.deltaTime * step;
                await UniTask.WaitForFixedUpdate(token);
            }

            update(1f, token);
        }
    }
}