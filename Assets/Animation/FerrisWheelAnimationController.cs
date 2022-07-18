using Bindito.Core;
using Timberborn.ConstructibleSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WindSystem;
using UnityEngine;

namespace Animation
{
    public class FerrisWheelAnimationController : TickableComponent, IFinishedStateListener
    {
        private Animator _animator;
        private bool _animationSuspended;

        public void Awake()
        {
            _animator = GetComponentInChildren<Animator>(true);
            _animator.speed = 0.0f;
            enabled = false;
        }

        public void OnEnterFinishedState() => enabled = true;

        public void OnExitFinishedState() => enabled = false;

        public override void Tick() => this.UpdateAnimation();

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

            _number = 0.3f;


            _animator.speed = _number;
        }
    }
}
