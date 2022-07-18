using Bindito.Core;
using Timberborn.ConstructibleSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WindSystem;
using UnityEngine;

namespace Animation
{
    public class WindAnimationController : TickableComponent, IFinishedStateListener
    {
        private NonlinearAnimationManager _nonlinearAnimationManager;
        private Animator _animator;
        private bool _animationSuspended;

        [Inject]
        public void InjectDependencies(
            NonlinearAnimationManager nonlinearAnimationManager)
        {
            this._nonlinearAnimationManager = nonlinearAnimationManager;
        }

        public void Awake()
        {
            this._animator = this.GetComponentInChildren<Animator>(true);
            this._animator.speed = 0.0f;
            this.enabled = false;
        }

        public void OnEnterFinishedState() => this.enabled = true;

        public void OnExitFinishedState() => this.enabled = false;

        public override void Tick() => this.UpdateAnimation();

        public void SuspendAnimation() => this._animationSuspended = true;

        public void UnsuspendAnimation() => this._animationSuspended = false;

        private float _number;

        private void UpdateAnimation()
        {
            if (_number == 1.0f)
            {
                _number = 0.0f;
            }
            else
            {
                _number += 0.01f;
            }

            _number = 0.5f;
        
            if (this._animationSuspended)
                this._animator.speed = 0.0f;
            else
                this._animator.speed = _number * this._nonlinearAnimationManager.SpeedMultiplier;
        }
    }
}
