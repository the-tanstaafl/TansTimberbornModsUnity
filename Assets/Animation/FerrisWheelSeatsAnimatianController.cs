using Timberborn.ConstructibleSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Animation
{
    public class FerrisWheelSeatsAnimatianController : TickableComponent, IFinishedStateListener
    {
        private static readonly int SeatSpeed = Animator.StringToHash(nameof (SeatSpeed));
        private Animator _animator;
        
        public void Awake()
        {
            this._animator = this.GetComponentInChildren<Animator>(true);
            this.enabled = false;
            
            _animator.SetFloat(SeatSpeed, 0.75f);
        }

        public void OnEnterFinishedState() => this.enabled = true;

        public void OnExitFinishedState() => this.enabled = false;

        public override void Tick() => this.UpdateAnimation();

        private float _number = 0.3f;
        private void UpdateAnimation()
        {
            
            
        }
    }
}
