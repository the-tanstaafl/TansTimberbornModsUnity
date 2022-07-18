using System;
using Bindito.Core;
using Timberborn.ConstructibleSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Animation
{
    public class AnimationController : TickableComponent, IFinishedStateListener
    {
        
        private static readonly int CubeSpeed = Animator.StringToHash(nameof (CubeSpeed));
        private Animator _animator;
        
        public void Awake()
        {
            this._animator = this.GetComponentInChildren<Animator>(true);
            this.enabled = false;
        }

        public void OnEnterFinishedState() => this.enabled = true;

        public void OnExitFinishedState() => this.enabled = false;

        public override void Tick() => this.UpdateAnimation();

        private float _number;
        private void UpdateAnimation()
        {
            
            _number += 0.01f;
            
            Plugin.Log.LogInfo(_number);
            
            _animator.SetFloat(CubeSpeed, _number);
        }
        
        
        
        
        
        
        // private Animator _animator;
        //
        // // Start is called before the first frame update
        // void Start()
        // {
        //     // Plugin.Log.LogFatal("ASDHAKHDSKLAJHDKLAHDHSLAD");
        //     // _animator = GetComponentInChildren<Animator>();
        //     // Plugin.Log.LogFatal(_animator);
        //     //
        //     //
        //     // _animator.Play("Rotate");
        // }
        //
        // private void Awake()
        // {
        //     Plugin.Log.LogFatal("ASDHAKHDSKLAJHDKLAHDHSLAD");
        //     _animator = GetComponentInChildren<Animator>();
        //     Plugin.Log.LogFatal(_animator);
        //
        //     _animator.speed = 0.5f;
        //     // _animator.Play("Rotate");
        // }
        //
        // // Update is called once per frame
        // void Update()
        // {
        //     
        // }
        
        
        // private static readonly int Rotate = Animator.StringToHash(nameof (Rotate));
        // [SerializeField]
        // private Animator _animator;
        //
        // public void Awake()
        // {
        //     this._animator = this.GetComponent<Animator>();
        //     Plugin.Log.LogFatal(_animator);
        //     this.enabled = false;
        // }
        //
        // public void OnEnterFinishedState() => this.enabled = true;
        //
        // public void OnExitFinishedState() => this.enabled = false;
        //
        // public override void Tick() => this.UpdateAnimation();
        //
        // private void UpdateAnimation()
        // {
        //
        //     this._animator.SetFloat(Rotate, 0.5f);
        //    
        // }
    }
}
